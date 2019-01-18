using System;

namespace Auditor
{
    public partial class AccessDenied : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ActiveUser.IsAuthenticated)
            {
                Response.Redirect(Pages.Login);
            }
        }
    }
}