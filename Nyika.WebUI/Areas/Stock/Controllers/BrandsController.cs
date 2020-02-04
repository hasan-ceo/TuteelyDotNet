using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nyika.Domain.Concrete.Stock;
using Nyika.Domain.Entities.Stock;
using Nyika.Domain.Abstract.Stock;
using Microsoft.AspNet.Identity;
using Nyika.WebUI.Models;

namespace Nyika.WebUI.Areas.Stock.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class BrandsController : Controller
    {
        private IBrandRepo db;

        public BrandsController(IBrandRepo DB)
        {
            this.db = DB;
        }


        // GET: BasicSetup/Brands
        public ActionResult Index()
        {
            return View(db.Brand.ToList());
        }

        

        // GET: BasicSetup/Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrandID,BrandName,BrandNameLocal")] Brand Brand)
        {

            if (ModelState.IsValid)
            {
                db.SaveBrand(Brand);
                return RedirectToAction("Index");
            }

            return View(Brand);
        }

        // GET: BasicSetup/Brands/Edit/5
        public ActionResult Edit(long id=0)
        {
            Brand Brand = db.Single(id);
            if (Brand == null)
            {
                return RedirectToAction("Index");
            }
            return View(Brand);
        }

        // POST: BasicSetup/Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrandID,BrandName,BrandNameLocal")] Brand Brand)
        {
            if (ModelState.IsValid)
            {

                db.SaveBrand(Brand);
                return RedirectToAction("Index");
            }
            return View(Brand);
        }

        // GET: BasicSetup/Brands/Delete/5
        public ActionResult Delete(long id=0)
        {
            Brand Brand = db.Single(id);

            if (Brand == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Brand.BrandID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Brand);
        }

        // POST: BasicSetup/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand Brand = db.Single(id);
            if (Brand != null)
            {
                db.DeleteBrand(Brand.BrandID);
            }
            return RedirectToAction("Index");
        }

    }
}
