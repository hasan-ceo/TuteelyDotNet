using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Globalization;


namespace Nyika.WebUI.Controllers
{
    public class LanguageController : Controller
    {

        public ActionResult ChangeLanguage(string selectedlanguage)
        {
            if (selectedlanguage != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(selectedlanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedlanguage);
                var cookie = new HttpCookie("Language");
                cookie.Value = selectedlanguage;
                Response.Cookies.Add(cookie);
            }
            return Redirect(System.Web.HttpContext.Current.Request.UrlReferrer.ToString());///"Index", "Shop");
        }
    }

  
}