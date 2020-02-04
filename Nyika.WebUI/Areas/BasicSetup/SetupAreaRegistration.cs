using System.Web.Mvc;

namespace Nyika.WebUI.Areas.BasicSetup
{
    public class SetupAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BasicSetup";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BasicSetup_default",
                "BasicSetup/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}