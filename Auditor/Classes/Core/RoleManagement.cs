using System;
using System.Collections.Generic;
using System.Web.Security;

namespace Auditor
{
    public static class RoleManagement
    {
        public static void RoleRegister(string roleName, string description)
        {
            if (!Roles.RoleExists(roleName))
            {
                Roles.CreateRole(roleName);
                RoleUpdate(roleName, description);
            }
            else
            {
                throw new Exception("Role already exists!");
            }
        }

        public static void RoleUpdate(string roleName, string description)
        {
            if (Roles.RoleExists(roleName))
            {
                string query = @"UPDATE [aspnet_Roles]
                                    SET [description] = @description
                                    WHERE [loweredrolename] = @rolename;";
                var parameters = new Dictionary<string, object>() { { "description", description },
                                                                    { "rolename", roleName }};
                DatabaseUtils.ExecuteNonQuery(query, parameters, DatabaseUtils.ConnectionTarget.Services);
            }
            else
            {
                throw new Exception("Role does not exist!");
            }
        }

        public static void RoleDelete(string roleName)
        {
            if (Roles.RoleExists(roleName))
            {
                string[] usersInRole = Roles.GetUsersInRole(roleName);
                if (usersInRole.Length > 0)
                {
                    Roles.RemoveUsersFromRole(usersInRole, roleName);
                }
                Roles.DeleteRole(roleName);
            }
            else
            {
                throw new Exception("Role does not exist!");
            }
        }
    }
}