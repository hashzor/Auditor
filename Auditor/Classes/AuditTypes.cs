using DevExpress.Web;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

namespace Auditor
{
    public abstract class AuditTypes
    {
        //TODO change audit types according to requirements

        public const string Audit5sLpaProduction = "5s_lpa_prod";
        public const string Audit5sAdministration = "5s_admin";
        public static List<string> AuditTypesList = new List<string>() { Audit5sLpaProduction, Audit5sAdministration };

        public static void AdjustTemplateToAuditType(string auditType, ASPxComboBox cbAuditTarget, ASPxComboBox cbAuditShiftName)
        {
            cbAuditTarget.Value = null;
            cbAuditShiftName.Value = null;

            cbAuditTarget.Visible = auditType != null;
            cbAuditShiftName.Visible = auditType == Audit5sLpaProduction;

            if (cbAuditTarget.Visible)
            {
                foreach (ListBoxColumn column in cbAuditTarget.Columns)
                {
                    column.Visible = false;
                }
                cbAuditTarget.Columns[0].Visible = true; //target
                if (auditType == Audit5sAdministration || auditType == Audit5sLpaProduction)
                {
                    cbAuditTarget.Columns[1].Visible = true; //area
                    cbAuditTarget.Columns[2].Visible = true; //subarea
                    cbAuditTarget.Columns[4].Visible = true; //supervisor
                }
            }
        }

        public static bool ShiftNameObligatory(string auditType) => auditType == Audit5sLpaProduction;

        public static decimal CalculateAuditScore(Audit audit)
        {
            decimal result = 0;
            var cntQuestion = audit.AuditQuestionCount;
            var cntOK = audit.GetAuditQuestionAnswersCount(Answers.OK);
            var cntNOK = audit.GetAuditQuestionAnswersCount(Answers.NOK);
            var cntNC = audit.GetAuditQuestionAnswersCount(Answers.NC);
            var cntNA = audit.GetAuditQuestionAnswersCount(Answers.NA);
            switch (audit.Type)
            {
                default:
                    result = (cntQuestion != cntNA) ? 100m * cntOK / (cntQuestion - cntNA) : 0;
                    break;
            }
            return result;
        }

        public static void GenerateAuditActions(Audit audit)
        {
            string query = @"SELECT 0";
            var parameters = new Dictionary<string, object>();

            if (audit.Type == Audit5sAdministration || audit.Type == Audit5sLpaProduction)
            {
                query = @"INSERT INTO [actions]  ([audit_id],[audit_detail_id],[responsible_login],[responsible_full_name])
                               SELECT
                                      [ad].[audit_id],
                                      [ad].[id] AS              [audit_detail_id],
                                      [t].[supervisor_login] AS [responsible_login],
                                      [t].[supervisor] AS       [responsible_full_name]
                               FROM   [audit_details] AS [ad]
                                      LEFT JOIN [audits] AS [a] ON [a].[id] = [ad].[audit_id]
                                      LEFT JOIN [setts_audit_targets] AS [t] ON [t].[audit_type] = [a].[audit_type]
                                                                                      AND [t].[audit_target] = [a].[audit_target]
                               WHERE  [ad].[answer] = @answer
                                      AND [ad].[audit_id] = @audit_id;";
                parameters.Add("answer", Answers.NOK);
                parameters.Add("audit_id", audit.Id);
            }
            DatabaseUtils.ExecuteNonQuery(query, parameters);
        }

        public static bool UserInAuditorList(string auditorLogin, string auditType, string auditTarget)
        {
            string query = @"SELECT 0";
            var parameters = new Dictionary<string, object>();
            switch (auditType)
            {
                case Audit5sLpaProduction:
                    query = @"SELECT COUNT(*)
                            FROM
                            (
                                SELECT [auditor_login]
                                FROM [setts_auditors]
                                WHERE [audit_type] = @audit_type

                                UNION

                                SELECT [supervisor_login]
                                FROM [setts_audit_targets]
                                WHERE [audit_type] = @audit_type AND [audit_target] = @audit_target
                            ) [data]
                            WHERE [auditor_login] = @auditor_login";
                    parameters.Add("audit_type", auditType);
                    parameters.Add("audit_target", auditTarget);
                    parameters.Add("auditor_login", auditorLogin);
                    break;

                default:
                    query = @"SELECT COUNT(*)
                            FROM   [setts_auditors]
                            WHERE  [audit_type] = @audit_type
                                    AND [auditor_login] = @auditor_login;";
                    parameters.Add("audit_type", auditType);
                    parameters.Add("auditor_login", auditorLogin);
                    break;
            }
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null && result > 0);
        }

        public static XtraReport GetXtraReport(Audit audit, string lang)
        {
            return new PdfReport(audit.Id, lang);
        }

        public static string GetTargetSupervisorLogin(string auditType, string auditTarget)
        {
            string query = "SELECT [supervisor_login] FROM [setts_audit_targets] WHERE [audit_type] = @audit_type AND audit_target = @audit_target";
            var parameters = new Dictionary<string, object>() { { "audit_type", auditType }, { "audit_target", auditTarget } };
            var result = Utils.ConvertToTrimmedString(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return result;
        }

        public static void NotifySupervisor(Audit audit)
        {
            if (audit.Type == Audit5sAdministration || audit.Type == Audit5sLpaProduction)
            {
                var supervisorLogin = GetTargetSupervisorLogin(audit.Type, audit.Target);
                var fileName = $"Audit_ID{audit.Id}_{DateTime.Now.ToString("yyyyMMdd")}.pdf";
                var subject = $"Ended audit {audit.TypeName} | {audit.Target}";
                var body = "";
                var email = AppUser.GetUserMail(supervisorLogin);
                if (email == null)
                {
                    throw new Exception("You can not send an e-mail with the report - no e-mail in the system / wrong format!");
                }
                var report = GetXtraReport(audit, Languages.Default);
                using (MemoryStream stream = new MemoryStream())
                {
                    report.ExportToPdf(stream);
                    stream.Position = 0;
                    var attach = new Attachment(stream, fileName, "application/pdf");
                    MailUtils.SendEmail(subject, body, new List<string> { email }, new List<Attachment> { attach });
                }
            }
        }

        public static string GetAuditGridTitle(Audit audit)
        {
            var result = $"{s}{ audit.TypeName }{e} | {s}{audit.Target}{e} |  Auditor: {s}{audit.AuditorFullName}{e} | Progress: {s}{audit.AuditProgress}%{e}";
            if (audit.Type == Audit5sAdministration || audit.Type == Audit5sLpaProduction)
            {
                result = $"{s}{ audit.TypeName }{e} | {s}{audit.Target}{e} |  Auditor: {s}{audit.AuditorFullName}{e} | Progress: {s}{audit.AuditProgress}%{e} | Max 45 min";
            }
            return result;
        }

        public static string GetAuditGridCaption(string groupPosition, string groupName)
        {
            return $"{s}{groupPosition}. {groupName}{e}";
        }

        private const string s = "<span style=\"color: #FFD500; font-weight: bold;\">"; //s - html span start
        private const string e = "</span>"; //e - html span end

        public static string VerifyAuditTargetEntry(string newAuditType, string newArea, string newSubarea, string newSection, string newSupervisorLogin)
        {
            var message = "Error!";
            if (newAuditType == Audit5sAdministration || newAuditType == Audit5sLpaProduction)
            {
                if (newArea == null
                    || newSubarea == null
                    || newSupervisorLogin == null)
                {
                    message = "Complete the AREA, SUBAREA and MANAGER fields!";
                }
                else
                {
                    message = null;
                }
            }
            return message;
        }
    }
}