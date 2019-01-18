using System.Web;

namespace Auditor
{
    public class ActiveUser : AppUser
    {
        public ActiveUser() : base(GetLogin())
        {
        }

        public static bool IsAuthenticated => HttpContext.Current.User.Identity.IsAuthenticated;

        public static bool IsInRole(string role) => (IsAuthenticated && HttpContext.Current.User.IsInRole(role));

        private static string GetLogin() => (IsAuthenticated) ? HttpContext.Current.User.Identity.Name.ToUpper().Trim() : null;
    }
}