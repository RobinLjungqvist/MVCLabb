using System.Web.Mvc;

namespace MVCLabb.Areas.Account
{
    public class AccountAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Account";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Account_default",
                "Account/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Update",
                "Account/{controller}/{action}/{id}",
                new { action = "Update", id = UrlParameter.Optional }
            );
        }
    }
}