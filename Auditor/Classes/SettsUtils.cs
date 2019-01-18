using System.Collections.Generic;

namespace Auditor
{
    public abstract class SettsUtils
    {
        public static string AuditorInsertVerify(string newAuditType, string newAuditorLogin)
        {
            string query = @"SELECT COUNT(*) FROM [setts_auditors] WHERE [audit_type] = @audit_type AND [auditor_login] = @auditor_login;";
            var parameters = new Dictionary<string, object>() { { "audit_type", newAuditType }, { "auditor_login", newAuditorLogin } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null && result > 0) ? "For a given audit, the auditor can be assigned only once!" : null;
        }

        public static string AuditorUpdateVerify(string newAuditType, string newAuditorLogin, string id)
        {
            string query = @"SELECT COUNT(*) FROM [setts_auditors] WHERE [audit_type] = @audit_type AND [auditor_login] = @auditor_login AND [id] <> @id;";
            var parameters = new Dictionary<string, object>() { { "audit_type", newAuditType }, { "auditor_login", newAuditorLogin }, { "id", id } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null && result > 0) ? "For a given audit, the auditor can be assigned only once!" : null;
        }

        public static string ScheduleInsertVerify(string newScheduleName)
        {
            string query = @"SELECT COUNT(*) FROM [setts_schedules] WHERE [schedule_name] = @schedule_name;";
            var parameters = new Dictionary<string, object>() { { "schedule_name", newScheduleName } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null && result > 0) ? "The system name must be unique!" : null;
        }

        public static string ScheduleUpdateVerify(string newScheduleName, string id)
        {
            string query = @"SELECT COUNT(*) FROM [setts_schedules] WHERE [schedule_name] = @schedule_name AND [id] <> @id;";
            var parameters = new Dictionary<string, object>() { { "schedule_name", newScheduleName }, { "id", id } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null && result > 0) ? "The system name must be unique!" : null;
        }

        public static string AuditTypeInsertVerify(string newAuditType)
        {
            string query = @"SELECT COUNT(*) FROM [setts_audit_types] WHERE [audit_type] = @audit_type;";
            var parameters = new Dictionary<string, object>() { { "audit_type", newAuditType } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null && result > 0) ? "The system name must be unique!" : null;
        }

        public static string AuditTypeUpdateVerify(string newAuditType, string id)
        {
            string query = @"SELECT COUNT(*) FROM [setts_audit_types] WHERE [audit_type] = @audit_type AND [id] <> @id;";
            var parameters = new Dictionary<string, object>() { { "audit_type", newAuditType }, { "id", id } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null && result > 0) ? "The system name must be unique!" : null;
        }

        public static string AuditTargetInsertVerify(string newAuditType, string newAuditTarget)
        {
            string query = @"SELECT COUNT(*) FROM [setts_audit_targets] WHERE [audit_type] = @audit_type AND [audit_target] = @audit_target;";
            var parameters = new Dictionary<string, object>() { { "audit_type", newAuditType }, { "audit_target", newAuditTarget } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null && result > 0) ? "For a given audit the target can be assigned only once!" : null;
        }

        public static string AuditTargetUpdateVerify(string newAuditType, string newAuditTarget, string id)
        {
            string query = @"SELECT COUNT(*) FROM [setts_audit_targets] WHERE [audit_type] = @audit_type AND [audit_target] = @audit_target AND [id] <> @id;";
            var parameters = new Dictionary<string, object>() { { "audit_type", newAuditType }, { "audit_target", newAuditTarget } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null && result > 0) ? "For a given audit the target can be assigned only once!" : null;
        }

        public static string ShiftInsertVerify(string newShiftName)
        {
            string query = @"SELECT COUNT(*) FROM [setts_shifts] WHERE [shift_name] = @shift_name;";
            var parameters = new Dictionary<string, object>() { { "shift_name", newShiftName } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null && result > 0) ? "The name must be unique!" : null;
        }

        public static string ShiftUpdateVerify(string newShiftName, string id)
        {
            string query = @"SELECT COUNT(*) FROM [setts_shifts] WHERE [shift_name] = @shift_name AND [id] <> @id;";
            var parameters = new Dictionary<string, object>() { { "shift_name", newShiftName } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters));
            return (result != null && result > 0) ? "The name must be unique!" : null;
        }
    }
}