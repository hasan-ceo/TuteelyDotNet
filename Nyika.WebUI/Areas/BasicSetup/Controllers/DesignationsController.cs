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
using Nyika.WebUI.Models;

namespace Nyika.WebUI.Areas.BasicSetup.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class DesignationsController : Controller
    {
        private IDesignationRepo db;
        private string instanceId;

        public DesignationsController(IDesignationRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: BasicSetup/Designations
        public ActionResult Index()
        {
             
            return View(db.Designation(instanceId).ToList());
        }

        

        // GET: BasicSetup/Designations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/Designations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DesignationID,DesignationName")] Designation Designation)
        {
             
            if (ModelState.IsValid)
            {
                Designation.InstanceID = instanceId;
                db.SaveDesignation(Designation);
                return RedirectToAction("Index");
            }

            return View(Designation);
        }

        // GET: BasicSetup/Designations/Edit/5
        public ActionResult Edit(long id=0)
        {
             

            Designation Designation = db.Single(instanceId, id);
            if (Designation == null)
            {
                return RedirectToAction("Index");
            }
            return View(Designation);
        }

        // POST: BasicSetup/Designations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DesignationID,DesignationName")] Designation Designation)
        {
            if (ModelState.IsValid)
            {
                 
                db.SaveDesignation(Designation);
                return RedirectToAction("Index");
            }
            return View(Designation);
        }

        // GET: BasicSetup/Designations/Delete/5
        public ActionResult Delete(long id=0)
        {
             
            Designation Designation = db.Single(instanceId, id);
            if (Designation == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Designation.DesignationID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Designation);
        }

        // POST: BasicSetup/Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
             
            Designation Designation = db.Single(instanceId, id);
            if (Designation != null)
            {
                db.DeleteDesignation(Designation.DesignationID);
            }            
            return RedirectToAction("Index");
        }

    }
}
