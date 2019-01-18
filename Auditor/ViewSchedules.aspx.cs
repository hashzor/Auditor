using DevExpress.Web;
using System;

namespace Auditor
{
    public partial class ViewSchedules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void gvSchedules_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;

            gridview.SettingsBehavior.AllowSort = false;
            gridview.SettingsText.Title = $"AUDITS SCHEDULES";
            gridview.SettingsExport.FileName = $"AuditSchedules_{DateTime.Now.ToString("yyyyMMdd")}";
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

            var check = new GridViewToolbarItem();
            check.Command = GridViewToolbarCommand.Custom;
            check.Name = ToolbarButtons.CheckAudit;
            check.Text = "See Audit";
            check.Image.Url = "Images/research.png";
            check.Image.AlternateText = print.Text;
            check.Image.ToolTip = print.Text;
            check.Image.Height = GridViewUtils.ImageToolbarSize;
            check.Image.Width = GridViewUtils.ImageToolbarSize;

            var toolbarGrid = gridview.Toolbars.FindByName(GridViewUtils.ToolbarGrid);
            if (toolbarGrid != null)
            {
                toolbarGrid.Items.Add(print);
                toolbarGrid.Items.Add(check);
            }
        }
    }
}