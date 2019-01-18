using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;

namespace Auditor
{
    public static class DatabaseUtils
    {
        public enum ConnectionTarget
        {
            Services,
            App
        }

        public static string ConnectionString(ConnectionTarget target)
        {
            string connectionString;
            switch (target)
            {
                case ConnectionTarget.Services:
                    connectionString = WebConfigurationManager.ConnectionStrings["csServices"].ConnectionString;
                    break;

                case ConnectionTarget.App:
                    connectionString = WebConfigurationManager.ConnectionStrings["csApp"].ConnectionString;
                    break;

                default:
                    connectionString = null;
                    break;
            }
            return connectionString;
        }

        /// <summary>
        /// Execute SQL query. Returns value from SCOPE_IDENTITY()
        /// </summary>
        public static int? ExecuteIdentityQuery(string query) => ExecuteIdentityQuery(query, null, ConnectionTarget.App);

        public static int? ExecuteIdentityQuery(string query, Dictionary<string, object> parameters) => ExecuteIdentityQuery(query, parameters, ConnectionTarget.App);

        public static int? ExecuteIdentityQuery(string query, ConnectionTarget target) => ExecuteIdentityQuery(query, null, target);

        public static int? ExecuteIdentityQuery(string query, Dictionary<string, object> parameters, ConnectionTarget target)
        {
            int? scopeIdentity = null;
            QueryValidation(query);
            var parametersList = CreateParametersList(parameters);
            ParametersValidation(parametersList);
            query = query + Environment.NewLine + "SELECT SCOPE_IDENTITY();";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString(target)))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parametersList != null)
                    {
                        command.Parameters.AddRange(parametersList.ToArray());
                    }
                    connection.Open();
                    scopeIdentity = Utils.ConvertToNullableInt(command.ExecuteScalar());
                    command.Parameters.Clear();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return scopeIdentity;
        }

        /// <summary>
        /// Execute  SQL query. Returns value from ROWS_AFFECTED
        /// </summary>

        public static int ExecuteNonQuery(string query) => ExecuteNonQuery(query, null, ConnectionTarget.App);

        public static int ExecuteNonQuery(string query, Dictionary<string, object> parameters) => ExecuteNonQuery(query, parameters, ConnectionTarget.App);

        public static int ExecuteNonQuery(string query, ConnectionTarget target) => ExecuteNonQuery(query, null, target);

        public static int ExecuteNonQuery(string query, Dictionary<string, object> parameters, ConnectionTarget target)
        {
            int rowsAffected;
            QueryValidation(query);
            var parametersList = CreateParametersList(parameters);
            ParametersValidation(parametersList);
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString(target)))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parametersList != null)
                    {
                        command.Parameters.AddRange(parametersList.ToArray());
                    }
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rowsAffected;
        }

        /// <summary>
        /// Execute SELECT SQL query. Returns DataTable
        /// </summary>

        public static DataTable ExecuteSelectQuery(string query) => ExecuteSelectQuery(query, null, ConnectionTarget.App);

        public static DataTable ExecuteSelectQuery(string query, Dictionary<string, object> parameters) => ExecuteSelectQuery(query, parameters, ConnectionTarget.App);

        public static DataTable ExecuteSelectQuery(string query, ConnectionTarget target) => ExecuteSelectQuery(query, null, target);

        public static DataTable ExecuteSelectQuery(string query, Dictionary<string, object> parameters, ConnectionTarget target)
        {
            DataTable result = new DataTable();
            QueryValidation(query);
            var parametersList = CreateParametersList(parameters);
            ParametersValidation(parametersList);
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString(target)))
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    if (parametersList != null)
                    {
                        command.Parameters.AddRange(parametersList.ToArray());
                    }
                    connection.Open();
                    adapter.Fill(result);
                    command.Parameters.Clear();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Execute SELECT SQL query. Returns object
        /// </summary>
        public static object ExecuteScalarSelectQuery(string query) => ExecuteScalarSelectQuery(query, null, ConnectionTarget.App);

        public static object ExecuteScalarSelectQuery(string query, Dictionary<string, object> parameters) => ExecuteScalarSelectQuery(query, parameters, ConnectionTarget.App);

        public static object ExecuteScalarSelectQuery(string query, ConnectionTarget target) => ExecuteScalarSelectQuery(query, null, target);

        public static object ExecuteScalarSelectQuery(string query, Dictionary<string, object> parameters, ConnectionTarget target)
        {
            object result;
            QueryValidation(query);
            var parametersList = CreateParametersList(parameters);
            ParametersValidation(parametersList);
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString(target)))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parametersList != null)
                    {
                        command.Parameters.AddRange(parametersList.ToArray());
                    }
                    connection.Open();
                    result = command.ExecuteScalar();
                    command.Parameters.Clear();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        private static void QueryValidation(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new Exception("Empty SQL query!");
            }
        }

        private static List<SqlParameter> CreateParametersList(Dictionary<string, object> parameters)
        {
            var parametersList = new List<SqlParameter>();
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    parametersList.Add(new SqlParameter(parameter.Key, parameter.Value ?? DBNull.Value));
                }
            }
            return parametersList.Count > 0 ? parametersList : null;
        }

        private static void ParametersValidation(List<SqlParameter> parameters)
        {
            if (parameters != null)
            {
                int distinctParameterNames = (from x in parameters
                                              select x.ParameterName).Distinct().Count();
                if (distinctParameterNames != parameters.Count())
                {
                    throw new Exception("Parameter names are not unique!");
                }
            }
        }
    }
}