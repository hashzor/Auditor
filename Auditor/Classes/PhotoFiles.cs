using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Auditor
{
    public abstract class PhotoFiles
    {
        public const string ThumbnailsAppPath = "~/Files/AuditPhotos/Thumbnails/";
        public const string PhotosAppPath = "~/Files/AuditPhotos/";

        public static void PhotoCallback(object source, CallbackEventArgs e, string appPath)
        {
            List<string> ImageExtensions = new List<string> { ".jpg", ".jpeg", ".bmp", ".gif", ".png" };
            int widthMax = 1200;
            int heightMax = 600;
            int width = 100;
            int height = 100;
            string url = appPath.Replace(@"~/", string.Empty);
            string filePath = HttpContext.Current.Server.MapPath(appPath);
            string fileExtension = Path.GetExtension(filePath).ToLower();
            if (ImageExtensions.Contains(fileExtension))
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(filePath);
                int widthOrigin = img.Width;
                int heightOrigin = img.Height;
                if (widthOrigin <= widthMax && heightOrigin <= heightMax)
                {
                    width = widthOrigin;
                    height = heightOrigin;
                }
                else
                {
                    double ratio = Math.Min(1.0 * widthMax / widthOrigin, 1.0 * heightMax / heightOrigin);
                    width = (int)(widthOrigin * ratio);
                    height = (int)(heightOrigin * ratio);
                }
                e.Result = width + "|" + height + "|" + url;
            }
            else
            {
                e.Result = "nok";
            }
        }

        public static void SavePhoto(int auditId, int auditDetailId)
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
                if (postedFile.FileName.ToLower().EndsWith(".png"))
                {
                    try
                    {
                        string imageName = DateTime.Now.ToString("yyyyMMddhhmmss");
                        string questionFolder = Audit.GetAuditDetailFolder(auditDetailId);
                        string imagePath = $"{PhotoFiles.PhotosAppPath}{auditId}/{questionFolder}/{imageName}.png";
                        using (Stream inStream = postedFile.InputStream)
                        {
                            byte[] fileData = new byte[postedFile.ContentLength];
                            inStream.Read(fileData, 0, postedFile.ContentLength);
                            postedFile.SaveAs(HttpContext.Current.Server.MapPath(imagePath));
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        public static void RemoveEmptyDirectories(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                RemoveEmptyDirectories(directory);
                if (Directory.GetFiles(directory).Length == 0 &&
                    Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }
        }

        public static void EndAuditRemove(Audit audit)
        {
            var path = $"{PhotosAppPath}/{audit.Id}/";
            var serverPath = HttpContext.Current.Server.MapPath(path);
            if (Directory.Exists(serverPath))
            {
                RemoveEmptyDirectories(serverPath);
            }
        }

        public static void DeleteAuditRemove(Audit audit)
        {
            var path = $"{PhotosAppPath}/{audit.Id}/";
            var serverPath = HttpContext.Current.Server.MapPath(path);
            if (Directory.Exists(serverPath))
            {
                Directory.Delete(serverPath, true);
            }
        }
    }
}