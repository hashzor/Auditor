using DevExpress.Web;
using System;
using System.IO;
using System.Web;

namespace Auditor
{
    public partial class Camera : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int? auditId = Utils.ConvertToNullableInt(Session["audit_id"]);
            int? auditDetailId = Utils.ConvertToNullableInt(Session["audit_detail_id"]);
            if (auditId != null && auditDetailId != null && Audit.AuditExist((int)auditId) == true && Audit.AuditClosed((int)auditId) == false)
            {
                SetupFileManager((int)auditId, (int)auditDetailId);
                PhotoFiles.SavePhoto((int)auditId, (int)auditDetailId);
            }
            else
            {
                Response.Redirect(Pages.PerformAudit);
            }
        }

        protected void SetupFileManager(int auditId, int auditDetailId)
        {
            string questionFolder = Audit.GetAuditDetailFolder(auditDetailId);
            string rootFolder = $"{PhotoFiles.PhotosAppPath}{auditId}/{questionFolder}/";
            string serverPath = HttpContext.Current.Server.MapPath(rootFolder);
            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }
            dxFileManager.Settings.RootFolder = rootFolder;
            dxFileManager.JSProperties["cp_root"] = rootFolder;
            dxFileManager.Settings.ThumbnailFolder = PhotoFiles.ThumbnailsAppPath;
            dxFileManager.SettingsEditing.AllowCopy = false;
            dxFileManager.SettingsEditing.AllowCreate = false;
            dxFileManager.SettingsEditing.AllowDelete = true;
            dxFileManager.SettingsEditing.AllowDownload = false;
            dxFileManager.SettingsEditing.AllowMove = false;
            dxFileManager.SettingsEditing.AllowRename = false;
            dxFileManager.SettingsFolders.Visible = false;
            dxFileManager.SettingsToolbar.ShowFilterBox = false;
            dxFileManager.SettingsToolbar.ShowPath = false;
            dxFileManager.SettingsUpload.Enabled = true;
            dxFileManager.SettingsUpload.ValidationSettings.MaxFileCount = 5;
            dxFileManager.SettingsUpload.ValidationSettings.MaxFileSize = 26214400;
            dxFileManager.SettingsUpload.AdvancedModeSettings.EnableMultiSelect = true;
            dxFileManager.Settings.AllowedFileExtensions = new String[] { ".jpg", ".jpeg", ".bmp", ".gif", ".png" };
            dxFileManager.SettingsUpload.ValidationSettings.MaxFileCountErrorText = "You can only upload 5 files at a time!";
            dxFileManager.SettingsUpload.ValidationSettings.MaxFileSizeErrorText = "File too big! The maximum size is 25 Mb!";
            dxFileManager.SettingsUpload.ValidationSettings.NotAllowedFileExtensionErrorText = "Illegal extension! You can upload [.jpg, .jpeg, .bmp, .gif, .png]!";
            dxFileManager.SettingsUpload.AutoStartUpload = true;
            dxFileManager.SettingsContextMenu.Enabled = false;
        }

        protected void picCallback_Callback(object source, CallbackEventArgs e)
        {
            string appPath = e.Parameter;
            PhotoFiles.PhotoCallback(source, e, appPath);
        }
    }
}