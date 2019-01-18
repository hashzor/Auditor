using DevExpress.XtraReports.UI;
using System;
using System.IO;
using System.Web;

namespace Auditor
{
    public static class Utils
    {
        public static bool? ConvertToNullableBool(object value)
        {
            bool parsedValue;
            if (value != null && bool.TryParse(value.ToString(), out parsedValue))
            {
                return parsedValue;
            }
            else
            {
                return null;
            }
        }

        public static int? ConvertToNullableInt(object value)
        {
            int parsedValue;
            if (value != null && int.TryParse(value.ToString(), out parsedValue))
            {
                return parsedValue;
            }
            else
            {
                return null;
            }
        }

        public static decimal? ConvertToNullableDecimal(object value)
        {
            decimal parsedValue;
            if (value != null && decimal.TryParse(value.ToString().Replace(".", ","), out parsedValue))
            {
                return parsedValue;
            }
            else
            {
                return null;
            }
        }

        public static DateTime? ConvertToNullableDateTime(object value)
        {
            DateTime parsedValue;
            if (value != null && DateTime.TryParse(value.ToString(), out parsedValue))
            {
                return parsedValue;
            }
            else
            {
                return null;
            }
        }

        public static string ConvertToTrimmedString(object value)
        {
            string parsedValue = Convert.ToString(value);
            if (!string.IsNullOrWhiteSpace(parsedValue))
            {
                return parsedValue.Trim();
            }
            else
            {
                return null;
            }
        }

        public static void PrintPDF(XtraReport report, string fileName)
        {
            fileName = Utils.ConvertToTrimmedString(fileName);
            if (fileName == null)
            {
                throw new Exception("File name can not be empty string!");
            }
            fileName = (fileName.ToLower().EndsWith(".pdf")) ? fileName : $"{fileName}.pdf";
            using (MemoryStream stream = new MemoryStream())
            {
                report.ExportToPdf(stream);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("Accept-Header", stream.Length.ToString());
                HttpContext.Current.Response.AddHeader("Content-Disposition", $"Inline; filename={fileName}");
                HttpContext.Current.Response.AddHeader("Content-Length", stream.Length.ToString());
                HttpContext.Current.Response.BinaryWrite(stream.ToArray());
                HttpContext.Current.Response.End();
            }
        }
    }
}