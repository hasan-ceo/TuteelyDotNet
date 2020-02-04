using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nyika.Domain.Concrete.MF;
using Nyika.Domain.Entities.MF;
using Nyika.Domain.Abstract.MF;
using Microsoft.AspNet.Identity;
using Nyika.WebUI.Models;

namespace Nyika.WebUI.Areas.MF.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class ProductsController : Controller
    {
        private IProductRepo db;
        private IProjectRepo pdb;
        private string instanceId;

        public ProductsController(IProductRepo DB, IProjectRepo PDB)
        {
            this.db = DB;
            this.pdb = PDB;
            instanceId = new InstanceVM().InstanceID;
        }

        public ActionResult GetProduct(long ProjectID)
        {
            //var products = lstProd.Where(p => p.CategoryID == CategorySubID);
            var Product = db.Product(instanceId).Where(g => g.ProjectID == ProjectID && g.Inactive == false);
            return Json(Product.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: BasicSetup/Products
        public ActionResult Index()
        {
            return View(db.Product(instanceId).ToList());
        }



        // GET: BasicSetup/Products/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName");
            ViewBag.InterestRateType = "Flat";
            return View();
        }

        // POST: BasicSetup/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProjectID,ProductName,InterestRate,InterestRateType,IntFactor,Duration,NoOfInstallment")] Product Product)
        {
            if (ModelState.IsValid)
            {
                Product.InstanceID = instanceId;
                db.SaveProduct(Product);
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName", Product.ProjectID);
            return View(Product);
        }

        // GET: BasicSetup/Products/Edit/5
        //public ActionResult Edit(long id=0)
        //{
        //    

        //    Product Product = db.Single(instanceId, id);
        //    if (Product == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ProjectID = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName", Product.ProjectID);
        //    return View(Product);
        //}

        // POST: BasicSetup/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductID,ProjectID,ProductName,InterestRate,InterestRateType,IntFactor,Duration,NoOfInstallment,Status")] Product Product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SaveProduct(Product);
        //        return RedirectToAction("Index");
        //    }
        //    
        //    ViewBag.ProjectID = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName", Product.ProjectID);
        //    return View(Product);
        //}

        // GET: BasicSetup/Products/Delete/5
        public ActionResult Delete(long id = 0)
        {
            Product Product = db.Single(instanceId, id);
            if (Product == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Product.ProductID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Product);
        }

        // POST: BasicSetup/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Product Product = db.Single(instanceId, id);
            if (Product != null)
            {
                db.DeleteProduct(Product.ProductID);
            }
            return RedirectToAction("Index");
        }

    }
}
