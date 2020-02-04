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
    public class SchemesController : Controller
    {
        private ISchemeRepo db;
        private string instanceId;

        public SchemesController(ISchemeRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;

        }


        // GET: BasicSetup/Schemes
        public ActionResult Index()
        {
            
            return View(db.Scheme(instanceId).ToList());
        }

        

        // GET: BasicSetup/Schemes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/Schemes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SchemeID,SchemeName")] Scheme Scheme)
        {
            
            if (ModelState.IsValid)
            {
                Scheme.InstanceID = instanceId;
                db.SaveScheme(Scheme);
                return RedirectToAction("Index");
            }

            return View(Scheme);
        }

        // GET: BasicSetup/Schemes/Edit/5
        //public ActionResult Edit(long id=0)
        //{
        //    

        //    Scheme Scheme = db.Single(instanceId, id);
        //    if (Scheme == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View(Scheme);
        //}

        // POST: BasicSetup/Schemes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "SchemeID,SchemeName")] Scheme Scheme)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        
        //        db.SaveScheme(Scheme);
        //        return RedirectToAction("Index");
        //    }
        //    return View(Scheme);
        //}

        // GET: BasicSetup/Schemes/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            Scheme Scheme = db.Single(instanceId, id);
            if (Scheme == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Scheme.SchemeID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Scheme);
        }

        // POST: BasicSetup/Schemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            
            Scheme Scheme = db.Single(instanceId,id);
            if (Scheme != null)
            {
                db.DeleteScheme(Scheme.SchemeID);
            }
            return RedirectToAction("Index");
        }

    }
}
