using CaptchaMvc.HtmlHelpers;
using Nyika.Domain.Abstract.AVL;
using Nyika.Domain.Abstract.Setup;
using Nyika.Domain.Abstract.Stock;
using Nyika.Domain.Entities.AVL;
using Nyika.Domain.Entities.Setup;
using Nyika.Domain.Entities.Stock;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Controllers
{
    public class ShopBagController : Controller
    {
        private IShopPageRepo db;
        private IFeedbackRepo fdb;
        private ICategoryRepo cdb;
        private ICategorySubRepo csdb;
        public IItemRepo idb;

        public ShopBagController(IShopPageRepo DB, IFeedbackRepo FDB, ICategoryRepo CDB, ICategorySubRepo CSDB, IItemRepo IDB)
        {
            this.db = DB;
            this.fdb = FDB;
            this.cdb = CDB;
            this.csdb = CSDB;
            this.idb = IDB;
        }

        public ViewResult Index(ShopBag ShopBag)
        {
            ViewBag.selectedlanguage = Request.Cookies["Language"].Value;
            return View(ShopBag);
        }

        [HttpPost]
        public ActionResult AddToBag(ShopBag ShopBag, long itemId, string returnUrl)
        {
            var a = System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
            Item item = idb.Item.Where(p => p.ItemID == itemId).FirstOrDefault();

            if (item != null)
            {
                ShopBag.AddItem(item, "", "", "", 1);
            }
            return Redirect(System.Web.HttpContext.Current.Request.UrlReferrer.ToString());
            //return RedirectToAction("Index", new { returnUrl });
        }

        public ActionResult RemoveFromBag(ShopBag ShopBag, long itemId)
        {
            Item item = idb.Item.Where(p => p.ItemID == itemId).FirstOrDefault();
            if (item != null)
            {
                ShopBag.RemoveLine(item);
            }
            return Redirect(System.Web.HttpContext.Current.Request.UrlReferrer.ToString());
        }

        public ActionResult IncreaseItem(ShopBag ShopBag, long itemId)
        {
            Item item = idb.Item.Where(p => p.ItemID == itemId).FirstOrDefault();
            if (item != null)
            {
                ShopBag.IncreaseItem(item);
            }
            return Redirect(System.Web.HttpContext.Current.Request.UrlReferrer.ToString());
        }

        public ActionResult DecreaseItem(ShopBag ShopBag, long itemId)
        {
            Item item = idb.Item.Where(p => p.ItemID == itemId).FirstOrDefault();
            if (item != null)
            {
                ShopBag.DecreaseItem(item);
            }
            return Redirect(System.Web.HttpContext.Current.Request.UrlReferrer.ToString());
        }

        [Authorize]
        public ActionResult Checkout(ShopBag ShopBag)
        {
            return View();
        }
    }


}