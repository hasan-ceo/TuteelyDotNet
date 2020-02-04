using System.Web.Mvc;

namespace Nyika.WebUI.Areas.HRnPayroll
{
    public class HRnPayrollAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HRnPayroll";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HRnPayroll_default",
                "HRnPayroll/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}