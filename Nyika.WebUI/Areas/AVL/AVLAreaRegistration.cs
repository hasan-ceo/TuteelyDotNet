using System.Web.Mvc;

namespace Nyika.WebUI.Areas.AVL
{
    public class AVLAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AVL";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AVL_default",
                "AVL/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}