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
    public class EmployeeResignsController : Controller
    {
        private IEmployeeResignRepo db;
        private IResignReasonRepo resignreasondb;
        private IEmployeeRepo employeedb;
        private IBusinessDayRepo bddb;
        private string instanceId;

        public EmployeeResignsController(IEmployeeResignRepo DB, IResignReasonRepo ResignreasonDB, IEmployeeRepo EmployeeDB, IBusinessDayRepo bdDB)
        {
            this.db = DB;
            this.employeedb = EmployeeDB;
            this.resignreasondb = ResignreasonDB;
            this.bddb = bdDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: HRnPayroll/EmployeeResigns
        public ActionResult Index()
        {

            // 
            var EmployeeResigns = db.EmployeeResign("x");
            return View(EmployeeResigns.ToList());
        }

        

        // GET: HRnPayroll/EmployeeResigns/Create
        public ActionResult Create()
        {
            
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN");
            ViewBag.ResignReasonID = new SelectList(resignreasondb.ResignReason(instanceId), "ResignReasonID", "ResignReasonName");
            return View();
        }

        // POST: HRnPayroll/EmployeeResigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeResignID,EmployeeID,ResignReasonID,Particulars")] EmployeeResign EmployeeResign)
        {
            
            if (ModelState.IsValid)
            {
                EmployeeResign.WorkDate = bddb.WorkDate(instanceId);
                EmployeeResign.EntryBy = User.Identity.Name;
                EmployeeResign.InstanceID = instanceId;
                db.SaveEmployeeResign(EmployeeResign);
                return RedirectToAction("Index");

            }
            ViewBag.ResignReasonID = new SelectList(resignreasondb.ResignReason(instanceId), "ResignReasonID", "ResignReasonName", EmployeeResign.ResignReasonID);
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN", EmployeeResign.EmployeeID);
            return View(EmployeeResign);
        }

        // GET: HRnPayroll/EmployeeResigns/Edit/5
        public ActionResult Edit(long id=0)
        {
            
            EmployeeResign EmployeeResign = db.Single(instanceId, id);
            if (EmployeeResign == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeID == EmployeeResign.EmployeeID && e.EmployeeStatus == 1), "EmployeeID", "PIN", EmployeeResign.EmployeeID);
            ViewBag.ResignReasonID = new SelectList(resignreasondb.ResignReason(instanceId), "ResignReasonID", "ResignReasonName", EmployeeResign.ResignReasonID);
            return View(EmployeeResign);
        }

        // POST: HRnPayroll/EmployeeResigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeResignID,ResignReasonID,Particulars,EmployeeID")] EmployeeResign EmployeeResign)
        {
            if (ModelState.IsValid)
            {
                EmployeeResign.EntryBy = User.Identity.Name;
                db.SaveEmployeeResign(EmployeeResign);
                return RedirectToAction("Index");
            }
            
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeID == EmployeeResign.EmployeeID && e.EmployeeStatus == 1), "EmployeeID", "PIN", EmployeeResign.EmployeeID);
            ViewBag.ResignReasonID = new SelectList(resignreasondb.ResignReason(instanceId), "ResignReasonID", "LeaveName", EmployeeResign.ResignReasonID);
            return View(EmployeeResign);
        }

        // GET: HRnPayroll/EmployeeResigns/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            EmployeeResign EmployeeResign = db.Single(instanceId, id);
            if (EmployeeResign == null)
            {
                return RedirectToAction("Index");
            }
            return View(EmployeeResign);
        }

        // POST: HRnPayroll/EmployeeResigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            db.DeleteEmployeeResign(id);
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
            
            var EmployeeResign = db.Search(instanceId, txtSearch);
            return View("Index", EmployeeResign.ToList());
        }
    }
}
