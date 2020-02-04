using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nyika.Domain.Concrete;
using Nyika.Domain.Entities.HR;
using Nyika.Domain.Abstract.HR;
using Microsoft.AspNet.Identity;
using Nyika.Domain.Abstract.Setup;
using Nyika.WebUI.Models;
using Nyika.Domain.Abstract.Accounts;

namespace Nyika.WebUI.Areas.HRnPayroll.Controllers
{
    [Authorize(Roles = "HR Executive,Super Admin")]
    public class EmployeeShowcausesController : Controller
    {
        private IEmployeeShowcauseRepo db;
        private IResignReasonRepo resignreasondb;
        private IEmployeeRepo employeedb;
        private IBusinessDayRepo bddb;
        private string instanceId;

        public EmployeeShowcausesController(IEmployeeShowcauseRepo DB, IResignReasonRepo ResignreasonDB, IEmployeeRepo EmployeeDB, IBusinessDayRepo bdDB)
        {
            this.db = DB;
            this.employeedb = EmployeeDB;
            this.resignreasondb = ResignreasonDB;
            this.bddb = bdDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: HRnPayroll/EmployeeShowcause
        public ActionResult Index()
        {

            // 
            var EmployeeShowcause = db.EmployeeShowcause("x");
            return View(EmployeeShowcause.ToList());
        }

       

        // GET: HRnPayroll/EmployeeShowcause/Create
        public ActionResult Create()
        {
            
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN");
            //ViewBag.ResignReasonID = new SelectList(resignreasondb.ResignReason(instanceId), "ResignReasonID", "ResignReasonName");
            return View();
        }

        // POST: HRnPayroll/EmployeeShowcause/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeShowcauseID,EmployeeID,Subject,Details,ActionTaken")] EmployeeShowcause EmployeeShowcause)
        {
            
            if (ModelState.IsValid)
            {
                EmployeeShowcause.WorkDate = bddb.WorkDate(instanceId);
                EmployeeShowcause.EntryBy = User.Identity.Name;
                EmployeeShowcause.InstanceID = instanceId;
                db.SaveEmployeeShowcause(EmployeeShowcause);
                return RedirectToAction("Index");

            }
            //ViewBag.ResignReasonID = new SelectList(resignreasondb.ResignReason(instanceId), "ResignReasonID", "ResignReasonName", EmployeeShowcause.ResignReasonID);
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN", EmployeeShowcause.EmployeeID);
            return View(EmployeeShowcause);
        }

        // GET: HRnPayroll/EmployeeShowcause/Edit/5
        public ActionResult Edit(long id=0)
        {
            
            EmployeeShowcause EmployeeShowcause = db.Single(instanceId, id);
            if (EmployeeShowcause == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeID == EmployeeShowcause.EmployeeID && e.EmployeeStatus == 0), "EmployeeID", "PIN", EmployeeShowcause.EmployeeID);
            return View(EmployeeShowcause);
        }

        // POST: HRnPayroll/EmployeeShowcause/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeShowcauseID,EmployeeID,Subject,Details,ActionTaken")] EmployeeShowcause EmployeeShowcause)
        {
            if (ModelState.IsValid)
            {
                EmployeeShowcause.EntryBy = User.Identity.Name;
                db.SaveEmployeeShowcause(EmployeeShowcause);
                return RedirectToAction("Index");
            }
            
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeID == EmployeeShowcause.EmployeeID && e.EmployeeStatus == 0), "EmployeeID", "PIN", EmployeeShowcause.EmployeeID);

            //ViewBag.ResignReasonID = new SelectList(resignreasondb.ResignReason(instanceId), "ResignReasonID", "LeaveName", EmployeeShowcause.ResignReasonID);
            return View(EmployeeShowcause);
        }

        // GET: HRnPayroll/EmployeeShowcause/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            EmployeeShowcause EmployeeShowcause = db.Single(instanceId, id);
            if (EmployeeShowcause == null)
            {
                return RedirectToAction("Index");
            }
            return View(EmployeeShowcause);
        }

        // POST: HRnPayroll/EmployeeShowcause/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            db.DeleteEmployeeShowcause(id);
            return RedirectToAction("Index");
        }

        // GET: Employees search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string txtSearch = "")
        {
            if (txtSearch == "")
            {
                txtSearch = "?";
            }
            
            var EmployeeShowcause = db.Search(instanceId, txtSearch);
            return View("Index", EmployeeShowcause.ToList());
        }
    }
}
