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
    public class SectionsController : Controller
    {
        private ISectionRepo db;
        private string instanceId;

        public SectionsController(ISectionRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: BasicSetup/Sections
        public ActionResult Index()
        {
            
            return View(db.Section(instanceId).ToList());
        }

        

        // GET: BasicSetup/Sections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SectionID,SectionName")] Section Section)
        {
            
            if (ModelState.IsValid)
            {
                Section.InstanceID = instanceId;
                db.SaveSection(Section);
                return RedirectToAction("Index");
            }

            return View(Section);
        }

        // GET: BasicSetup/Sections/Edit/5
        public ActionResult Edit(long id=0)
        {
            

            Section Section = db.Single(instanceId, id);
            if (Section == null)
            {
                return RedirectToAction("Index");
            }
            return View(Section);
        }

        // POST: BasicSetup/Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SectionID,SectionName")] Section Section)
        {
            if (ModelState.IsValid)
            {
                
                db.SaveSection(Section);
                return RedirectToAction("Index");
            }
            return View(Section);
        }

        // GET: BasicSetup/Sections/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            Section Section = db.Single(instanceId, id);
            if (Section == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Section.SectionID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Section);
        }

        // POST: BasicSetup/Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            
            Section Section = db.Single(instanceId,id);
            if (Section != null)
            {
                db.DeleteSection(Section.SectionID);
            }
            return RedirectToAction("Index");
        }

    }
}
