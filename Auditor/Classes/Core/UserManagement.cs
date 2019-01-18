using System;
using System.Collections.Generic;
using System.Web.Security;

namespace Auditor
{
    public static class UserManagement
    {
        public static void UserRegister(string username, string name, string lastName, string email)
        {
            if (Membership.GetUser(username) == null)
            {
                var password = GeneratePassword();
                Membership.CreateUser(username, password);
                UserUpdate(username, name, lastName, email);
                SendRegisterNotification(username, password);
            }
            else
            {
                throw new Exception("Username is already in use!");
            }
        }

        public static void UserDelete(string username)
        {
            if (Membership.GetUser(username) != null)
            {
                Membership.DeleteUser(username);
            }
            else
            {
                throw new Exception("User does not exist!");
            }
        }

        public static void UserUpdate(string username, string name, string lastName, string email)
        {
            if (Membership.GetUser(username) != null)
            {
                string query = @"UPDATE [aspnet_Users]
                                    SET
                                        [name] = @name,
                                        [lastname] = @lastname,
                                        [email] = @email
                                    WHERE
                                        [loweredusername] = @username;";
                var parameters = new Dictionary<string, object>() { { "name", name },
                                                                    { "lastname", lastName },
                                                                    { "email", email },
                                                                    { "username", username }};
                DatabaseUtils.ExecuteNonQuery(query, parameters, DatabaseUtils.ConnectionTarget.Services);
            }
            else
            {
                throw new Exception("User does not exist!");
            }
        }

        public static void UserUnlock(string username)
        {
            var member = Membership.GetUser(username);
            if (member != null)
            {
                member.UnlockUser();
                SendUnlockNotification(username);
            }
            else
            {
                throw new Exception("User does not exist!");
            }
        }

        public static void UserResetPassword(string username)
        {
            var member = Membership.GetUser(username);
            if (member != null)
            {
                var password = GeneratePassword();
                member.UnlockUser();
                member.ChangePassword(member.ResetPassword(), password);
                SendChangePasswordNotification(username, password);
            }
            else
            {
                throw new Exception("User does not exist!");
            }
        }

        public static void UserRemoveRole(string username, string roleName)
        {
            if (Membership.GetUser(username) != null && Roles.RoleExists(roleName) && Roles.IsUserInRole(username, roleName))
            {
                Roles.RemoveUserFromRole(username, roleName);
            }
            else
            {
                throw new Exception("User is not in this role! OR Role/user may not exist!");
            }
        }

        public static void UserAddRole(string username, string roleName)
        {
            if (Membership.GetUser(username) != null && Roles.RoleExists(roleName) && !Roles.IsUserInRole(username, roleName))
            {
                Roles.AddUserToRole(username, roleName);
            }
            else
            {
                throw new Exception("User is already in this role! OR Role/user may not exist!");
            }
        }

        private static string GeneratePassword() => (new Random()).Next(0, 999999).ToString("D6");

        private static void SendRegisterNotification(string username, string password)
        {
            var subject = $"Account {username.ToUpper()} creation!";
            var body = $"Your password: {password}";
            var email = AppUser.GetUserMail(username);
            MailUtils.SendEmail(subject, body, new List<string> { email });
        }

        private static void SendUnlockNotification(string username)
        {
            var subject = $"Account {username.ToUpper()} unlocked!";
            var body = string.Empty;
            var email = AppUser.GetUserMail(username);
            MailUtils.SendEmail(subject, body, new List<string> { email });
        }

        private static void SendChangePasswordNotification(string username, string password)
        {
            var subject = $"Account {username.ToUpper()} password reseted!";
            var body = $"Your new password: {password}";
            var email = AppUser.GetUserMail(username);
            MailUtils.SendEmail(subject, body, new List<string> { email });
        }
    }
}