using System;
using System.Collections.Generic;

namespace Auditor
{
    public class AppUser
    {
        public string UserName { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName { get; }
        public string Email { get; }
        public bool Exist { get; }

        public AppUser(string userName)
        {
            this.UserName = (userName != null) ? userName.ToUpper().Trim() : null;
            this.Exist = false;
            if (this.UserName != null && UserExist(userName))
            {
                this.Exist = true;
                string query = @"SELECT
                                    [Name],
                                    [LastName],
                                    [Email]
                                FROM [aspnet_Users]
                                WHERE [LoweredUserName] = @username;";
                var parameters = new Dictionary<string, object>() { { "username", userName.ToLower().Trim() } };
                var result = DatabaseUtils.ExecuteSelectQuery(query, parameters, DatabaseUtils.ConnectionTarget.Services);
                if (result != null)
                {
                    this.Email = Convert.ToString(result.Rows[0]["Email"]);
                    this.Email = (MailUtils.EmailAddressValid(this.Email)) ? this.Email.ToLower().Trim() : null;

                    this.FirstName = Convert.ToString(result.Rows[0]["Name"]);
                    this.FirstName = (this.FirstName != null) ? this.FirstName.Trim() : null;
                    this.LastName = Convert.ToString(result.Rows[0]["LastName"]);
                    this.LastName = (this.LastName != null) ? this.LastName.Trim() : null;

                    this.FullName = this.FirstName + " " + this.LastName;
                    this.FullName = (!string.IsNullOrWhiteSpace(this.FullName)) ? this.FullName.Trim() : null;
                }
            }
        }

        public static bool UserExist(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) return false;
            string query = @"SELECT
                                    COUNT(*) AS [result]
                            FROM   [aspnet_Users]
                            WHERE  [LoweredUserName] = @username;";
            var parameters = new Dictionary<string, object>() { { "username", userName.ToLower().Trim() } };
            var result = Utils.ConvertToNullableInt(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters, DatabaseUtils.ConnectionTarget.Services));
            return (result != null && result > 0);
        }

        public static string GetUserMail(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) return null;
            string query = @"SELECT
                                    [Email]
                               FROM [aspnet_Users]
                              WHERE [LoweredUserName] = @username;";
            var parameters = new Dictionary<string, object>() { { "username", userName.ToLower().Trim() } };
            var result = Convert.ToString(DatabaseUtils.ExecuteScalarSelectQuery(query, parameters, DatabaseUtils.ConnectionTarget.Services));
            return (MailUtils.EmailAddressValid(result)) ? result.ToLower().Trim() : null;
        }
    }
}