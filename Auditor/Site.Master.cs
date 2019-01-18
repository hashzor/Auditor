using System;
using System.Web.UI.WebControls;

namespace Auditor
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = AppUtils.PageTitle;
            lblAppName.Text = AppUtils.AppName;
            submenu.FindItem("LOGIN").Text = ActiveUser.IsAuthenticated ? "LOG OUT" : "LOG IN";
            if (ActiveUser.IsAuthenticated)
            {
                var activeAudit = Audit.GetUserActiveAudit(new ActiveUser().UserName);
                if (activeAudit != null)
                {
                    submenu.FindItem("AUDIT").NavigateUrl = Pages.PerformAudit;
                }
            }
            var settingsButton = submenu.FindItem("SETTINGS");
            if (settingsButton != null && !(ActiveUser.IsInRole(AppRoles.AuditorAdmin) || ActiveUser.IsInRole(AppRoles.UserAdmin)))
            {
                submenu.Items.Remove(settingsButton);
            }
            MenuSelecting(submenu.Items);
        }

        protected bool MenuSelecting(MenuItemCollection items)
        {
            foreach (MenuItem item in items)
            {
                var url = Request.Url.AbsoluteUri.ToLower().Trim();
                var navLink = Page.ResolveUrl(item.NavigateUrl).ToLower().Trim();
                if (!string.IsNullOrEmpty(navLink) && url.Contains(navLink))
                {
                    item.Selected = true;
                    return true;
                }
                if (item.ChildItems.Count > 0 && MenuSelecting(item.ChildItems))
                {
                    item.Text = "<div style='border-bottom: 4px solid #e00025; margin-bottom: -4px;'>" + item.Text + "</div>";
                    return true;
                }
            }
            return false;
        }
    }
}