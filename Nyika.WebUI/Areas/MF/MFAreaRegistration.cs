using System.Web.Mvc;

namespace Nyika.WebUI.Areas.MF
{
    public class MFAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MF";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MF_default",
                "MF/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}