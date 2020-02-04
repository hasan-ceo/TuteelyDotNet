using System.Web.Mvc;
using Nyika.WebUI.Areas.Invoices.Models;

namespace Nyika.WebUI.Infrastructure.Binders
{

    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "InvCart";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            // get the Cart from the session 

            InvCart invcart = null;
            if (controllerContext.HttpContext.Session != null)
            {
                invcart = (InvCart)controllerContext.HttpContext.Session[sessionKey];
            }
            // create the Cart if there wasn't one in the session data
            if (invcart == null)
            {
                invcart = new InvCart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = invcart;
                }
            }
            // return the cart
            return invcart;
        }
    }
}
