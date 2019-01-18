using DevExpress.Web;
using System;
using System.IO;
using System.Web;
using System.Web.UI;

namespace Auditor
{
    public partial class BrowseAudits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["gvAuditsExpandedRowId"] = -1;
                var id = Utils.ConvertToNullableInt(Request.QueryString["id"]);
                if (id != null)
                {
                    if (Audit.AuditExist((int)id))
                    {
                        gvAudits.DataColumns["id"].Settings.AutoFilterCondition = AutoFilterCondition.Equals;
                        gvAudits.AutoFilterByColumn(gvAudits.Columns["id"], id.ToString());
                        gvAudits.DetailRows.ExpandRow(0);
                        Session["gvAuditsExpandedRowId"] = 0;
                    }
                }
            }
        }

        protected void gvAudits_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;

            gridview.SettingsText.Title = "AUDITS";
            gridview.SettingsDetail.AllowOnlyOneMasterRowExpanded = true;
            gridview.SettingsDetail.ShowDetailRow = true;
            gridview.SettingsExport.FileName = $"Audits_{DateTime.Now.ToString("yyyyMMdd")}";
            gridview.SettingsPager.PageSize = 50;

            GridViewUtils.GridViewToolbarInit(sender, e);

            var print = new GridViewToolbarItem();
            print.Command = GridViewToolbarCommand.Custom;
            print.Name = ToolbarButtons.PrintAudit;
            print.Text = "Print Audit";
            print.Image.Url = "Images/printer.png";
            print.Image.AlternateText = print.Text;
            print.Image.ToolTip = print.Text;
            print.Image.Height = GridViewUtils.ImageToolbarSize;
            print.Image.Width = GridViewUtils.ImageToolbarSize;

            var deleteAudit = new GridViewToolbarItem();
            deleteAudit.Command = GridViewToolbarCommand.Custom;
            deleteAudit.Name = ToolbarButtons.DeleteAudit;
            deleteAudit.Text = "Delete Audit";
            deleteAudit.Image.Url = "Images/trash.png";
            deleteAudit.Image.AlternateText = print.Text;
            deleteAudit.Image.ToolTip = print.Text;
            deleteAudit.Image.Height = GridViewUtils.ImageToolbarSize;
            deleteAudit.Image.Width = GridViewUtils.ImageToolbarSize;

            var toolbarGrid = gridview.Toolbars.FindByName(GridViewUtils.ToolbarGrid);
            if (toolbarGrid != null)
            {
                toolbarGrid.Items.Add(print);
                if (ActiveUser.IsInRole(AppRoles.AuditorAdmin))
                {
                    toolbarGrid.Items.Add(deleteAudit);
                }
            }
        }

        protected void gvAuditDetails_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;

            gridview.SettingsBehavior.AllowSort = false;
            gridview.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            gridview.SettingsText.Title = $"AUDIT ID: {gridview.GetMasterRowKeyValue()}";
            gridview.SettingsExport.FileName = $"Audit_{gridview.GetMasterRowKeyValue()}_{DateTime.Now.ToString("yyyyMMdd")}";

            GridViewUtils.GridViewToolbarInit(sender, e);
        }

        protected void gvAuditDetails_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView gridview = sender as ASPxGridView;
            sdsAuditDetails.SelectParameters["audit_id"].DefaultValue = gridview.GetMasterRowKeyValue().ToString();
        }

        protected void gvAudits_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            Session["gvAuditsExpandedRowId"] = (e.Expanded) ? e.VisibleIndex : -1;
            ASPxGridView gridView = sender as ASPxGridView;
            gridView.ScrollToVisibleIndexOnClient = e.VisibleIndex;
        }

        protected void dxFileManager_Init(object sender, EventArgs e)
        {
            ASPxFileManager dxFileManager = sender as ASPxFileManager;
            var gvAuditsExpandedRowId = Utils.ConvertToNullableInt(Session["gvAuditsExpandedRowId"]);
            gvAuditsExpandedRowId = (gvAuditsExpandedRowId == null) ? -1 : gvAuditsExpandedRowId;
            string auditId = (gvAuditsExpandedRowId != -1) ? gvAudits.GetRowValues((int)gvAuditsExpandedRowId, "id").ToString() : "0";
            string rootFolder = $"{PhotoFiles.PhotosAppPath}{auditId}/";
            string serverPath = HttpContext.Current.Server.MapPath(rootFolder);
            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }
            else
            {
                PhotoFiles.RemoveEmptyDirectories(serverPath);
            }
            dxFileManager.Settings.RootFolder = rootFolder;
            dxFileManager.Settings.ThumbnailFolder = PhotoFiles.ThumbnailsAppPath;
            dxFileManager.SettingsEditing.AllowCopy = false;
            dxFileManager.SettingsEditing.AllowCreate = false;
            dxFileManager.SettingsEditing.AllowDelete = false;
            dxFileManager.SettingsEditing.AllowDownload = true;
            dxFileManager.SettingsEditing.AllowMove = false;
            dxFileManager.SettingsEditing.AllowRename = false;
            dxFileManager.SettingsUpload.Enabled = false;
            dxFileManager.SettingsUpload.AdvancedModeSettings.EnableMultiSelect = true;
            dxFileManager.SettingsToolbar.ShowPath = false;
            dxFileManager.SettingsToolbar.ShowFilterBox = false;
            dxFileManager.SettingsContextMenu.Enabled = true;
            dxFileManager.Settings.EnableMultiSelect = true;
        }

        protected void picCallback_Callback(object source, CallbackEventArgs e)
        {
            string appPath = PhotoFiles.PhotosAppPath + e.Parameter.Replace("\\", "/");
            PhotoFiles.PhotoCallback(source, e, appPath);
        }

        protected void gvAudits_ToolbarItemClick(object source, DevExpress.Web.Data.ASPxGridViewToolbarItemClickEventArgs e)
        {
            ASPxGridView gridview = source as ASPxGridView;
            var auditId = Utils.ConvertToNullableInt(gridview.GetRowValues(gridview.FocusedRowIndex, "id"));
            var audit = new Audit((int)auditId);
            if (!audit.AuditExist())
            {
                throw new Exception("Audit does not exist!!");
            }
            bool refresh = false;
            switch (e.Item.Name)
            {
                case ToolbarButtons.DeleteAudit:
                    audit.Delete();
                    refresh = true;
                    break;
            }
            if (refresh)
            {
                gridview.JSProperties["cp_refresh"] = true;
            }
        }
    }
}