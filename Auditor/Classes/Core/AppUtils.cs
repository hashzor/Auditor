using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;

namespace Auditor
{
    public static class AppUtils
    {
        public static string AppName => Assembly.GetExecutingAssembly().GetName().Name.Trim();

        public static string PageTitle
        {
            get
            {
                string fileName = Path.GetFileNameWithoutExtension(HttpContext.Current.Request.FilePath).Trim();
                fileName = fileName.Replace("default", string.Empty).Replace("Default", string.Empty);

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return AppName;
                }
                else
                {
                    return $"{AppName}/{fileName}";
                }
            }
        }

        public static string CurrentUrl => HttpContext.Current.Request.Url.ToString();

        public static bool LocalhostMode => CurrentUrl.ToLower().Contains("localhost");

        public static void LogError(string errorMessage)
        {
            string logFilePath = HttpContext.Current.Server.MapPath("~/App_Data/errorLog.txt");

            StringBuilder message = new StringBuilder();
            message.AppendLine(DateTime.Now.ToString());
            message.AppendLine($"{CurrentUrl}\n");
            message.AppendLine(errorMessage);
            message.AppendLine("=========================================\n");

            File.AppendAllText(logFilePath, message.ToString());
        }
    }
}