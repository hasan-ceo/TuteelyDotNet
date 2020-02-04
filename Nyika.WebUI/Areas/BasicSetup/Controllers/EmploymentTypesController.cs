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
    public class EmploymentTypesController : Controller
    {
        private IEmploymentTypeRepo db;
        private string instanceId;

        public EmploymentTypesController(IEmploymentTypeRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: BasicSetup/EmploymentTypes
        public ActionResult Index()
        {
             
            return View(db.EmploymentType(instanceId).ToList());
        }

        

        // GET: BasicSetup/EmploymentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/EmploymentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmploymentTypeID,EmploymentTypeName")] EmploymentType EmploymentType)
        {
             
            if (ModelState.IsValid)
            {
                EmploymentType.InstanceID = instanceId;
                db.SaveEmploymentType(EmploymentType);
                return RedirectToAction("Index");
            }

            return View(EmploymentType);
        }

        // GET: BasicSetup/EmploymentTypes/Edit/5
        public ActionResult Edit(long id=0)
        {
             
            EmploymentType EmploymentType = db.Single(instanceId, id);
            if (EmploymentType == null)
            {
                return RedirectToAction("Index");
            }
            return View(EmploymentType);
        }

        // POST: BasicSetup/EmploymentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmploymentTypeID,EmploymentTypeName")] EmploymentType EmploymentType)
        {
            if (ModelState.IsValid)
            {
                 
                db.SaveEmploymentType(EmploymentType);
                return RedirectToAction("Index");
            }
            return View(EmploymentType);
        }

        // GET: BasicSetup/EmploymentTypes/Delete/5
        public ActionResult Delete(long id=0)
        {

             
            EmploymentType EmploymentType = db.Single(instanceId, id);
            if (EmploymentType == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(EmploymentType.EmploymentTypeID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(EmploymentType);
        }

        // POST: BasicSetup/EmploymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
             
            EmploymentType EmploymentType = db.Single(instanceId ,id);
            if (EmploymentType != null)
            {
                db.DeleteEmploymentType(EmploymentType.EmploymentTypeID);
            }            
            return RedirectToAction("Index");
        }

    }
}
