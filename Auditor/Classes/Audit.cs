using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Mail;

namespace Auditor
{
    public class Audit
    {
        #region audit fields

        public int Id { get; }
        public string Type { get; }
        public string TypeName { get; }
        public string Target { get; }
        public string AuditorLogin { get; }
        public string AuditorFullName { get; }
        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }
        public decimal? Score { get; }
        public string Comment { get; }
        public string TargetArea { get; }
        public string TargetSubarea { get; }
        public string TargetSection { get; }
        public string ShiftName { get; }

        #endregion audit fields

        #region calculated fields

        public DataTable AuditQuestionGroups { get { return GetAuditQuestionGroups(); } }
        public int AuditQuestionCount { get { return GetAuditQuestionCount(); } }
        public int AuditQuestionAnsweredCount { get { return GetAuditQuestionAnsweredCount(); } }
        public decimal AuditProgress { get { return (AuditQuestionCount != 0) ? 100 * AuditQuestionAnsweredCount / AuditQuestionCount : 0; } }
        public decimal AuditScoreCalculated { get { return AuditTypes.CalculateAuditScore(this); } }

        #endregion calculated fields

        public Audit(int id)
        {
            if (AuditExist(id))
            {
                string query = @"SELECT
                                       [id],
                                       [audit_type],
                                       [audit_type_name],
                                       [audit_target],
                                       [auditor_login],
                                       [auditor_full_name],
                                       [start_date],
                                       [end_date],
                                       [score],
                                       [comment],
                                       [audit_target_area],
                                       [audit_target_subarea],
                                       [audit_target_section],
                                       [audit_shift_name]
                                FROM   [audits]
                                WHERE  [id] = @id;";
                var parameters = new Dictionary<string, object>() { { "id", id } };
                var result = DatabaseUtils.ExecuteSelectQuery(query, parameters);
                if (result != null)
                {
                    this.Id = id;
                    this.Type = Utils.ConvertToTrimmedString(result.Rows[0]["audit_type"]);
                    this.TypeName = Utils.ConvertToTrimmedString(result.Rows[0]["audit_type_name"]);
                    this.Target = Utils.ConvertToTrimmedString(result.Rows[0]["audit_target"]);
                    this.AuditorLogin = Utils.ConvertToTrimmedString(result.Rows[0]["auditor_login"]);
                    this.AuditorFullName = Utils.ConvertToTrimmedString(result.Rows[0]["auditor_full_name"]);
                    this.StartDate = Utils.ConvertToNullableDateTime(result.Rows[0]["start_date"]);
                    this.EndDate = Utils.ConvertToNullableDateTime(result.Rows[0]["end_date"]);
                    this.Score = Utils.ConvertToNullableDecimal(result.Rows[0]["score"]);
                    this.Comment = Utils.ConvertToTrimmedString(result.Rows[0]["comment"]);
                    this.TargetArea = Utils.ConvertToTrimmedString(result.Rows[0]["audit_target_area"]);
                    this.TargetSubarea = Utils.ConvertToTrimmedString(result.Rows[0]["audit_target_subarea"]);
                    this.TargetSection = Utils.ConvertToTrimmedString(result.Rows[0]["audit_target_section"]);
                    this.ShiftName = Utils.ConvertToTrimmedString(result.Rows[0]["audit_shift_name"]);
                }
            }
        }

        private int GetAuditQuestionCount()
        {
            string query = @"SELECT COUNT(*)
                                FROM [audit_details]
                                WHERE [audit_id]= @audit_id;";
            var parameters = new Dictionary<string, object>() { { "audit_id", this.Id } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null && result > 0) ? (int)result : 0;
        }

        private int GetAuditQuestionAnsweredCount()
        {
            string query = @"SELECT COUNT(*)
                                FROM [audit_details]
                                WHERE [audit_id]= @audit_id AND [answer] IS NOT NULL;";
            var parameters = new Dictionary<string, object>() { { "audit_id", this.Id } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null && result > 0) ? (int)result : 0;
        }

        public int GetAuditQuestionAnswersCount(string answer)
        {
            string query = @"SELECT COUNT(*)
                                FROM [audit_details]
                                WHERE [audit_id]= @audit_id AND [answer] = @answer;";
            var parameters = new Dictionary<string, object>() { { "audit_id", this.Id },
                                                                { "answer", answer} };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null && result > 0) ? (int)result : 0;
        }

        private DataTable GetAuditQuestionGroups()
        {
            string query = @"SELECT  DISTINCT
		                              [group_position],
                                        [group_name],
                                        [group_name_ENG]
                                FROM [audit_details]
                                WHERE [audit_id]= @audit_id
                                ORDER BY [group_position];";
            var parameters = new Dictionary<string, object>() { { "audit_id", this.Id } };
            var result = DatabaseUtils.ExecuteSelectQuery(query, parameters);
            return (result.Rows.Count > 0) ? result : null;
        }

        public bool AuditClosed() => AuditClosed(this.Id);

        public bool AuditExist() => AuditExist(this.Id);

        public void End()
        {
            string query = @"UPDATE [audits]
                                SET [score] = @score,
                                    [end_date] = GETDATE()
                                WHERE [id] = @audit_id;";
            var parameters = new Dictionary<string, object>() { { "audit_id", this.Id },
                                                                { "score", this.AuditScoreCalculated }};
            DatabaseUtils.ExecuteNonQuery(query, parameters);
            AuditTypes.GenerateAuditActions(this);
            AuditTypes.NotifySupervisor(this);
            PhotoFiles.EndAuditRemove(this);
        }

        public void Delete()
        {
            PhotoFiles.DeleteAuditRemove(this);
            string query = @"DELETE FROM [audits]
                            WHERE [id] = @audit_id;
                            DELETE FROM [audit_details]
                            WHERE [audit_id] = @audit_id;
                            DELETE FROM [actions]
                            WHERE [audit_id] = @audit_id;";
            var parameters = new Dictionary<string, object>() { { "audit_id", this.Id } };
            DatabaseUtils.ExecuteNonQuery(query, parameters);
        }

        public static Audit GetUserActiveAudit(string user)
        {
            string query = "SELECT [id] FROM [audits] WHERE [end_date] IS NULL AND [auditor_login] = @auditor_login";
            var parameters = new Dictionary<string, object>() { { "auditor_login", user } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null) ? new Audit((int)result) : null;
        }

        public static Audit CreateNew(string auditType, string auditTarget, string auditorLogin, string auditorFullName, string auditShiftName)
        {
            var auditId = InsertAuditHeader(auditType, auditTarget, auditorLogin, auditorFullName, auditShiftName);
            if (auditId != null)
            {
                if (InsertAuditDetails((int)auditId) > 0)
                {
                    return new Audit((int)auditId);
                }
                else
                {
                    new Audit((int)auditId).Delete();
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static void AnswerQuestion(int auditDetailsId, string answer)
        {
            string query = "UPDATE [audit_details] SET [answer] = @answer WHERE [id] = @id";
            var parameters = new Dictionary<string, object>() { { "id", auditDetailsId },
                                                                { "answer", answer } };
            DatabaseUtils.ExecuteNonQuery(query, parameters);
        }

        public static bool AuditClosed(int id)
        {
            string query = "SELECT [end_date] FROM [audits] WHERE [id] = @id";
            var parameters = new Dictionary<string, object>() { { "id", id } };
            var result = Utils.ConvertToNullableDateTime(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return result != null;
        }

        public static bool AuditExist(int id)
        {
            string query = "SELECT [id] FROM [audits] WHERE [id] = @id";
            var parameters = new Dictionary<string, object>() { { "id", id } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return result != null;
        }

        public static string GetAuditDetailFolder(int auditDetailId)
        {
            string query = "SELECT 'G'+CONVERT(NVARCHAR(10),[group_position])+'Q'+CONVERT(NVARCHAR(10),[question_position]) FROM [audit_details] WHERE [id] = @id";
            var parameters = new Dictionary<string, object>() { { "id", auditDetailId } };
            var result = Utils.ConvertToTrimmedString(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return result == null ? "error" : result;
        }

        public void SendEmailWithPdf(string lang)
        {
            var fileName = $"Audit_ID{this.Id}_{DateTime.Now.ToString("yyyyMMdd")}.pdf";
            var subject = $"Printout {this.TypeName} | {this.Target}";
            var body = "";
            var email = AppUser.GetUserMail(this.AuditorLogin);
            if (email == null)
            {
                throw new Exception("You can not send an e-mail with the report - no e-mail in the system / wrong format!");
            }
            var report = AuditTypes.GetXtraReport(this, lang);
            using (MemoryStream stream = new MemoryStream())
            {
                report.ExportToPdf(stream);
                stream.Position = 0;
                var attach = new Attachment(stream, fileName, "application/pdf");
                MailUtils.SendEmail(subject, body, new List<string> { email }, new List<Attachment> { attach });
            }
            throw new Exception("An email with a PDF printout has been sent! Check your inbox!");
        }

        private static int? InsertAuditHeader(string auditType, string auditTarget, string auditorLogin, string auditorFullName, string auditShiftName)
        {
            string query = @"INSERT INTO [audits] ([audit_type],[audit_type_name],[audit_target],[audit_target_area],[audit_target_subarea],[audit_target_section],[auditor_login],[auditor_full_name],[start_date], [audit_shift_name])
                                SELECT TOP 1
                                       [types].[audit_type],
                                       [types].[name] AS [audit_type_name],
                                       [targets].[audit_target],
                                       [targets].[area] AS [audit_target_area],
                                       [targets].[subarea] AS [audit_target_subarea],
                                       [targets].[section] AS [audit_target_section],
	                                   @auditor_login AS [auditor_login],
	                                   @auditor_full_name AS [auditor_full_name],
	                                   GETDATE() AS [start_date],
                                        @audit_shift_name AS [audit_shift_name]
                                FROM   [setts_audit_types] [types]
                                LEFT JOIN [setts_audit_targets] [targets] ON [types].[audit_type] =  [targets].[audit_type]
                                WHERE [types].[audit_type] = @audit_type AND [targets].[audit_target] = @audit_target";
            var parameters = new Dictionary<string, object>() { { "audit_type", auditType } ,
                                                                { "audit_target", auditTarget } ,
                                                                { "auditor_login", auditorLogin },
                                                                { "auditor_full_name", auditorFullName },
                                                                { "audit_shift_name", auditShiftName }};
            return DatabaseUtils.ExecuteIdentityQuery(query, parameters);
        }

        private static int InsertAuditDetails(int auditId)
        {
            string query = @"INSERT INTO [audit_details] ([audit_id],[group_position],[group_name],[group_name_ENG],[question_position],[question],[question_ENG],[answer_OK],[answer_NOK],[answer_NA],[answer_NC])
                               SELECT
                                      [audit].[id] AS [audit_id],
                                      [groups].[group_position],
                                      [groups].[group_name],
                                      [groups].[group_name_ENG],
                                      [questions].[question_position],
                                      [questions].[question],
                                      [questions].[question_ENG],
                                      [questions].[answer_OK],
                                      [questions].[answer_NOK],
                                      [questions].[answer_NA],
                                      [questions].[answer_NC]
                               FROM   [audits] AS [audit]
                                      LEFT JOIN [setts_audit_question_groups] AS [groups] ON [groups].[audit_type] = [audit].[audit_type]
                                      LEFT JOIN [setts_audit_questions] AS [questions] ON [questions].[group_id] = [groups].[id]
                               WHERE  [audit].[id] = @audit_id;";
            var parameters = new Dictionary<string, object>() { { "audit_id", auditId } };
            return DatabaseUtils.ExecuteNonQuery(query, parameters);
        }
    }
}