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
using Nyika.WebUI.Areas.HRnPayroll.Models;

namespace Nyika.WebUI.Areas.HRnPayroll.Controllers
{
    [Authorize(Roles = "HR Executive,Super Admin")]
    public class EmployeeAttendancesController : Controller
    {
        private IEmployeeAttendanceRepo db;
        private IEmployeeRepo employeedb;
        private IBusinessDayRepo bddb;
        private IAttenStatusRepo attsdb;
        private string instanceId;

        public EmployeeAttendancesController(IEmployeeAttendanceRepo DB, IEmployeeRepo EmployeeDB, IBusinessDayRepo bdDB, IAttenStatusRepo AttsDB)
        {
            this.db = DB;
            this.employeedb = EmployeeDB;
            this.bddb = bdDB;
            this.attsdb = AttsDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: HRnPayroll/EmployeeAttendances
        public ActionResult Index()
        {
            var EmployeeAttendances = db.EmployeeAttendance("x");
            return View(EmployeeAttendances.ToList());
        }

        

        // GET: HRnPayroll/EmployeeAttendances/Create
        public ActionResult Create()
        {
            
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN");
            ViewBag.AttenStatusID = new SelectList(attsdb.AttenStatus.Where(a => a.AttenStatusID == 0 || a.AttenStatusID==2 || a.AttenStatusID==5 || a.AttenStatusID==7 || a.AttenStatusID==8 ), "AttenStatusID", "AttenStatusName");
            return View();
        }

        // POST: HRnPayroll/EmployeeAttendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,AttenStatusID,Particulars,InTime,OutTime,FromDate,TillDate")] EmployeeAttendanceVM eavm)
        {
            
            if (ModelState.IsValid)
            {
                db.SaveEmployeeAttendance(eavm.EmployeeID, eavm.AttenStatusID, eavm.Particulars, eavm.InTime, eavm.OutTime, eavm.FromDate, eavm.TillDate, User.Identity.Name, instanceId);
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN", eavm.EmployeeID);
            ViewBag.AttenStatusID = new SelectList(attsdb.AttenStatus, "AttenStatusID", "AttenStatusName", eavm.AttenStatusID);
            //ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId), "EmployeeID", "PIN", eavm.EmployeeID);
            return View(eavm);
        }

        // GET: HRnPayroll/EmployeeAttendances/Edit/5
        //public ActionResult Edit(long id=0)
        //{
        //    
        //    EmployeeAttendance EmployeeAttendance = db.Single(instanceId, id);
        //    if (EmployeeAttendance == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View(EmployeeAttendance);
        //}

        // POST: HRnPayroll/EmployeeAttendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "EmployeeAttendanceID,LeaveID,FromDate,TillDate,ApplicationDate,Particulars,Withoutpay")] EmployeeAttendanceVM EmployeeAttendance)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (EmployeeAttendance.FromDate <= EmployeeAttendance.TillDate)
        //        {
        //            EmployeeAttendance.EntryBy = User.Identity.Name;
        //            db.SaveEmployeeAttendance(EmployeeAttendance);
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    
        //    return View(EmployeeAttendance);
        //}

        // GET: HRnPayroll/EmployeeAttendances/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            EmployeeAttendance EmployeeAttendance = db.Single(instanceId, id);
            if (EmployeeAttendance == null)
            {
                return RedirectToAction("Index");
            }
            return View(EmployeeAttendance);
        }

        // POST: HRnPayroll/EmployeeAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            db.DeleteEmployeeAttendance(id);
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
            
            var EmployeeAttendance = db.Search(instanceId, txtSearch);
            return View("Index", EmployeeAttendance.ToList());
        }
    }
}
