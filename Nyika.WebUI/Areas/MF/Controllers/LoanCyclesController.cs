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
    public class LoanCyclesController : Controller
    {
        private ILoanCycleRepo db;
        private IProjectRepo pdb;
        private string instanceId;

        public LoanCyclesController(ILoanCycleRepo DB, IProjectRepo PDB)
        {
            this.db = DB;
            this.pdb = PDB;
            instanceId = new InstanceVM().InstanceID;
        }

        public ActionResult GetLoanCycle(long ProjectID)
        {
            //var LoanCycles = lstProd.Where(p => p.CategoryID == CategorySubID);
            var LoanCycle = db.LoanCycle(instanceId).Where(g => g.ProjectID == ProjectID);
            return Json(LoanCycle.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: BasicSetup/LoanCycles
        public ActionResult Index()
        {
            return View(db.LoanCycle(instanceId).ToList());
        }

       

        // GET: BasicSetup/LoanCycles/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName");
            return View();
        }

        // POST: BasicSetup/LoanCycles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoanCycleID,ProjectID,LoanCycleNo,MinLimit,MaxLimit")] LoanCycle LoanCycle)
        {
            if (ModelState.IsValid)
            {
                if (db.CreateNew(instanceId, LoanCycle.ProjectID) == true)
                {
                    LoanCycle.InstanceID = instanceId;
                    db.SaveLoanCycle(LoanCycle);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("nyikamsg", "Already maximum loan cycle created for this project."); 
                }
            }
            ViewBag.ProjectID = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName",LoanCycle.ProjectID);
            return View(LoanCycle);
        }

        // GET: BasicSetup/LoanCycles/Edit/5
        public ActionResult Edit(long id=0)
        {
            LoanCycle LoanCycle = db.Single(instanceId, id);
            if (LoanCycle == null)
            {
                return RedirectToAction("Index");
            }
            return View(LoanCycle);
        }

        // POST: BasicSetup/LoanCycles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoanCycleID,LoanCycleNo,MinLimit,MaxLimit")] LoanCycle LoanCycle)
        {
            if (ModelState.IsValid)
            {
                db.SaveLoanCycle(LoanCycle);
                return RedirectToAction("Index");
            }
            return View(LoanCycle);
        }

        // GET: BasicSetup/LoanCycles/Delete/5
        public ActionResult Delete(long id=0)
        {
            LoanCycle LoanCycle = db.Single(instanceId, id);
            if (LoanCycle == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(LoanCycle.InstanceID, LoanCycle.ProjectID) <2)
            {
                TempData["Delete"] = "Minimum one loan cycle required, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(LoanCycle);
        }

        // POST: BasicSetup/LoanCycles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            LoanCycle LoanCycle = db.Single(instanceId,id);
            if (LoanCycle != null)
            {
                db.DeleteLoanCycle(LoanCycle.LoanCycleID);
            }
            return RedirectToAction("Index");
        }

    }
}
