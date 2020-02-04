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
    public class EducationsController : Controller
    {
        private IEducationRepo db;
        private string instanceId;

        public EducationsController(IEducationRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: BasicSetup/Educations
        public ActionResult Index()
        {
             
            return View(db.Education(instanceId).ToList());
        }

        

        // GET: BasicSetup/Educations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/Educations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EducationID,EducationName")] Education Education)
        {
             
            if (ModelState.IsValid)
            {
                Education.InstanceID = instanceId;
                db.SaveEducation(Education);
                return RedirectToAction("Index");
            }

            return View(Education);
        }

        // GET: BasicSetup/Educations/Edit/5
        public ActionResult Edit(long id=0)
        {
             
            Education Education = db.Single(instanceId, id);
            if (Education == null)
            {
                return RedirectToAction("Index");
            }
            return View(Education);
        }

        // POST: BasicSetup/Educations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EducationID,EducationName")] Education Education)
        {
            if (ModelState.IsValid)
            {
                 
                db.SaveEducation(Education);
                return RedirectToAction("Index");
            }
            return View(Education);
        }

        // GET: BasicSetup/Educations/Delete/5
        public ActionResult Delete(long id=0)
        {
             
            Education Education = db.Single(instanceId, id);
            if (Education == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Education.EducationID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Education);
        }

        // POST: BasicSetup/Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
             
            Education Education = db.Single(instanceId, id);
            if (Education != null)
            {
                db.DeleteEducation(Education.EducationID);
            }            
            return RedirectToAction("Index");
        }

    }
}
