using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nyika.Domain.Concrete;
using Nyika.Domain.Entities.Setup;
using Nyika.Domain.Abstract.Setup;
using Microsoft.AspNet.Identity;
using Nyika.WebUI.Models;

namespace Nyika.WebUI.Areas.BasicSetup.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class AllDedsController : Controller
    {
        private IAllDedRepo db;
        private string instanceId;

        public AllDedsController(IAllDedRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: BasicSetup/AllDeds
        public ActionResult Index()
        {
            
            return View(db.AllDed(instanceId).ToList());
        }

        

        // GET: BasicSetup/AllDeds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/AllDeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AllDedID,AllDedName,ADType")] AllDed allDed)
        {
            if (ModelState.IsValid)
            {
                 
                allDed.InstanceID = instanceId;
                db.SaveAllDed(allDed);
                return RedirectToAction("Index");
            }

            return View(allDed);
        }

        // GET: BasicSetup/AllDeds/Edit/5
        public ActionResult Edit(long id=0)
        {
             
            AllDed allDed = db.Single(instanceId, id);
            if (allDed == null)
            {
                return RedirectToAction("Index");
            }
            return View(allDed);
        }

        // POST: BasicSetup/AllDeds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AllDedID,AllDedName,ADType")] AllDed allDed)
        {
            if (ModelState.IsValid)
            {
                db.SaveAllDed(allDed);
                return RedirectToAction("Index");
            }
            return View(allDed);
        }

        // GET: BasicSetup/AllDeds/Delete/5
        public ActionResult Delete(long id=0)
        {

             
            AllDed allDed = db.Single(instanceId, id);
            if (allDed == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(allDed.AllDedID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(allDed);
        }

        // POST: BasicSetup/AllDeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
             
            AllDed allDed = db.Single(instanceId, id);

            db.DeleteAllDed(allDed.AllDedID);            
            return RedirectToAction("Index");
        }
        
    }
}
