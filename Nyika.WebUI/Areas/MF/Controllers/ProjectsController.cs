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
    public class ProjectsController : Controller
    {
        private IProjectRepo db;
        private string instanceId;

        public ProjectsController(IProjectRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;
        }

        public ActionResult GetProject()
        {
            //var products = lstProd.Where(p => p.CategoryID == CategorySubID);
            var Project = db.Project(instanceId).Where(p => p.Inactive == false);
            return Json(Project.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: BasicSetup/Projects
        public ActionResult Index()
        {
            return View(db.Project(instanceId).ToList());
        }



        // GET: BasicSetup/Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,ProjectName,StartDate,EndDate,FirstDisbursementDate")] Project Project)
        {
            if (ModelState.IsValid)
            {
                if (Project.StartDate < Project.EndDate)
                {
                    if (Project.StartDate <= Project.FirstDisbursementDate && Project.FirstDisbursementDate < Project.EndDate)
                    {
                        Project.Inactive = false;
                        Project.InstanceID = instanceId;
                        db.SaveProject(Project);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("FirstDisbursementDate", "First Disbursement Date must be between project start and end date");
                    }
                }
                else
                {
                    ModelState.AddModelError("EndDate", "Project end date must be after project start date");
                }
            }

            return View(Project);
        }

        // GET: BasicSetup/Projects/Edit/5
        public ActionResult Edit(long id = 0)
        {
            Project Project = db.Single(instanceId, id);
            if (Project == null)
            {
                return RedirectToAction("Index");
            }
            return View(Project);
        }

        // POST: BasicSetup/Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,ProjectName,StartDate,EndDate,FirstDisbursementDate,Inactive")] Project Project)
        {
            if (ModelState.IsValid)
            {

                    if (Project.StartDate < Project.EndDate)
                    {
                        if (Project.StartDate <= Project.FirstDisbursementDate && Project.FirstDisbursementDate < Project.EndDate)
                        {
                            db.SaveProject(Project);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("FirstDisbursementDate", "First Disbursement Date must be between project start and end date");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("EndDate", "Project end date must be after project start date");
                    }

            }
            return View(Project);
        }

        // GET: BasicSetup/Projects/Delete/5
        public ActionResult Delete(long id = 0)
        {
            Project Project = db.Single(instanceId, id);
            if (Project == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Project.ProjectID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Project);
        }

        // POST: BasicSetup/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Project Project = db.Single(instanceId, id);
            if (Project != null)
            {
                db.DeleteProject(Project.ProjectID);
            }
            return RedirectToAction("Index");
        }

    }
}
