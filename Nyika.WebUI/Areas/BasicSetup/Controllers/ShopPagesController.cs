using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nyika.Domain.Concrete.Setup;
using Nyika.Domain.Entities.Setup;
using Nyika.Domain.Abstract.Setup;
using Microsoft.AspNet.Identity;

namespace Nyika.WebUI.Areas.BasicSetup.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class ShopPagesController : Controller
    {
        private IShopPageRepo db;

        public ShopPagesController(IShopPageRepo DB)
        {
            this.db = DB;
        }


        // GET: BasicSetup/ShopPages
        public ActionResult Index()
        {
            return View(db.ShopPage.ToList());
        }



        // GET: BasicSetup/ShopPages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/ShopPages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShopPageID,Slug,Description,DescriptionLocal")] ShopPage ShopPage)
        {
            if (ModelState.IsValid)
            {
                db.SaveShopPage(ShopPage);
                return RedirectToAction("Index");
            }

            return View(ShopPage);
        }

        // GET: BasicSetup/ShopPages/Edit/5
        public ActionResult Edit(long id=0)
        {
            ShopPage ShopPage = db.ShopPage.Where(p => p.ShopPageID == id).FirstOrDefault();
            if (ShopPage == null)
            {
                return RedirectToAction("Index");
            }
            return View(ShopPage);
        }

        // POST: BasicSetup/ShopPages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShopPageID,Slug,Description,DescriptionLocal")] ShopPage ShopPage)
        {
            if (ModelState.IsValid)
            {
                db.SaveShopPage(ShopPage);
                return RedirectToAction("Index");
            }
            return View(ShopPage);
        }

        // GET: BasicSetup/ShopPages/Delete/5
        public ActionResult Delete(long id=0)
        {
            ShopPage ShopPage = db.ShopPage.Where(p => p.ShopPageID == id).FirstOrDefault();
            if (ShopPage == null)
            {
                return RedirectToAction("Index");
            }
            return View(ShopPage);
        }

        // POST: BasicSetup/ShopPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ShopPage ShopPage = db.ShopPage.Where(p => p.ShopPageID == id).FirstOrDefault();
            if (ShopPage != null)
            {
                db.DeleteShopPage(ShopPage.ShopPageID);
            }
            return RedirectToAction("Index");
        }

    }
}
