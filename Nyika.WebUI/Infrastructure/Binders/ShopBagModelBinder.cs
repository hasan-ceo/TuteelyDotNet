using System.Web.Mvc;
using Nyika.WebUI.Models;

namespace Nyika.WebUI.Infrastructure.Binders
{

    public class ShopBagModelBinder : IModelBinder
    {
        private const string sessionKey = "ShopBag";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            // get the Cart from the session 

            ShopBag ShopBag = null;
            if (controllerContext.HttpContext.Session != null)
            {
                ShopBag = (ShopBag)controllerContext.HttpContext.Session[sessionKey];
            }
            // create the Cart if there wasn't one in the session data
            if (ShopBag == null)
            {
                ShopBag = new ShopBag();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = ShopBag;
                }
            }
            // return the cart
            return ShopBag;
        }
    }
}
