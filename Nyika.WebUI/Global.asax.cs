using Nyika.WebUI.Areas.Invoices.Models;
using Nyika.WebUI.Controllers;
using Nyika.WebUI.Infrastructure.Binders;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Nyika.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //if (!Context.Request.IsSecureConnection)
            //    Response.Redirect(Context.Request.Url.ToString().Replace("http:", "https:"));

            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            if (cookie != null && cookie.Value != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                HttpContext.Current.Response.SetCookie(new HttpCookie("Language", "en"));
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(ShopBag), new ShopBagModelBinder());
            ModelBinders.Binders.Add(typeof(InvCart), new CartModelBinder());
        }

        protected void Application_Error()
        {

            Exception exception = Server.GetLastError();


            Response.Clear();

            HttpException httpException = exception as HttpException;
            int httpCode = httpException.GetHttpCode();

            //Add controller name
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");

            //we will add controller's action name 
            routeData.Values.Add("action", "Index");

            // Pass exception details to the target error View.
            routeData.Values.Add("error", exception.Message);

            // Clear the error on server.
            Server.ClearError();

            // Call target Controller and pass the routeData.
            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));


            //and throw the exception from the Controller by simply writing
            //throw new Exception();

        }
    }

}

