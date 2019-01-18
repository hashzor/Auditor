using System;
using System.Collections.Generic;

namespace Auditor
{
    public class Action
    {
        #region audit fields

        public int Id { get; }
        public int? AuditId { get; }
        public int? AuditDetailId { get; }
        public string ResponsibleLogin { get; }
        public string ResponsibleFullName { get; }
        public string ActionText { get; }
        public DateTime? PlannedTerm { get; }
        public DateTime? Term { get; }
        public DateTime? LastEditDate { get; }

        #endregion audit fields

        public Action(int id)
        {
            if (ActionExist(id))
            {
                string query = @"SELECT
                                       [id],
                                       [audit_id],
                                       [audit_detail_id],
                                       [responsible_login],
                                       [responsible_full_name],
                                       [action],
                                       [planned_term],
                                       [term],
                                       [last_edit_date]
                                FROM   [actions]
                                WHERE  [id] = @id;";
                var parameters = new Dictionary<string, object>() { { "id", id } };
                var result = DatabaseUtils.ExecuteSelectQuery(query, parameters);
                if (result != null)
                {
                    this.Id = id;
                    this.AuditId = Utils.ConvertToNullableInt(result.Rows[0]["audit_id"]);
                    this.AuditDetailId = Utils.ConvertToNullableInt(result.Rows[0]["audit_detail_id"]);
                    this.ResponsibleLogin = Utils.ConvertToTrimmedString(result.Rows[0]["responsible_login"]);
                    this.ResponsibleFullName = Utils.ConvertToTrimmedString(result.Rows[0]["responsible_full_name"]);
                    this.ActionText = Utils.ConvertToTrimmedString(result.Rows[0]["action"]);
                    this.PlannedTerm = Utils.ConvertToNullableDateTime(result.Rows[0]["planned_term"]);
                    this.Term = Utils.ConvertToNullableDateTime(result.Rows[0]["term"]);
                    this.LastEditDate = Utils.ConvertToNullableDateTime(result.Rows[0]["last_edit_date"]);
                }
            }
        }

        public bool ActionExist() => ActionExist(this.Id);

        public void Confirm()
        {
            string query = @"UPDATE [actions]
                                SET
                                    [term] = GETDATE()
                              WHERE [id] = @id";
            var parameters = new Dictionary<string, object>() { { "id", this.Id } };
            DatabaseUtils.ExecuteNonQuery(query, parameters);
        }

        public void Clear()
        {
            string query = @"UPDATE [actions]
                                SET
                                    [action] = NULL,
                                    [planned_term] = NULL,
                                    [term] = NULL,
                                    [last_edit_date] = NULL
                              WHERE [id] = @id";
            var parameters = new Dictionary<string, object>() { { "id", this.Id } };
            DatabaseUtils.ExecuteNonQuery(query, parameters);
        }

        public static bool ActionExist(int id)
        {
            string query = "SELECT [id] FROM [actions] WHERE [id] = @id";
            var parameters = new Dictionary<string, object>() { { "id", id } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return result != null;
        }
    }
}