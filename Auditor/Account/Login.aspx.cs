using Auditor;
using System;
using System.Web.Security;

namespace RoleManager
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ActiveUser.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}