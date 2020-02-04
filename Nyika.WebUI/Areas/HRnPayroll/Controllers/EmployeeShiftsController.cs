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
    public class EmployeeShiftsController : Controller
    {
        private IEmployeeShiftRepo db;
        private IShiftRepo Shiftdb;
        private IEmployeeRepo employeedb;
        private IBusinessDayRepo bddb;
        private string instanceId;

        public EmployeeShiftsController(IEmployeeShiftRepo DB, IShiftRepo ShiftDB, IEmployeeRepo EmployeeDB, IBusinessDayRepo bdDB)
        {
            this.db = DB;
            this.employeedb = EmployeeDB;
            this.Shiftdb = ShiftDB;
            this.bddb = bdDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: HRnPayroll/EmployeeShifts
        public ActionResult Index()
        {
            var employeeShifts = db.EmployeeShift("x");
            return View(employeeShifts.ToList());
        }

       

        // GET: HRnPayroll/EmployeeShifts/Create
        public ActionResult Create()
        {
            
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus==0), "EmployeeID", "PIN");
            ViewBag.ShiftID = new SelectList(Shiftdb.Shift(instanceId), "ShiftID", "ShiftName");
            return View();
        }

        // POST: HRnPayroll/EmployeeShifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeShiftID,EmployeeID,ShiftID,FromDate,TillDate,Particulars")] EmployeeShift employeeShift)
        {
            
            if (ModelState.IsValid)
            {
                if (employeeShift.FromDate <= employeeShift.TillDate)
                {
                    employeeShift.WorkDate = bddb.WorkDate(instanceId);
                    employeeShift.EntryBy = User.Identity.Name;
                    employeeShift.InstanceID = instanceId;
                    db.SaveEmployeeShift(employeeShift);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.ShiftID = new SelectList(Shiftdb.Shift(instanceId), "ShiftID", "ShiftName", employeeShift.ShiftID);
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN", employeeShift.EmployeeID);
            return View(employeeShift);
        }

        // GET: HRnPayroll/EmployeeShifts/Edit/5
        public ActionResult Edit(long id=0)
        {
            
            EmployeeShift employeeShift = db.Single(instanceId, id);
            if (employeeShift == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeID== employeeShift.EmployeeID && e.EmployeeStatus==0), "EmployeeID", "PIN", employeeShift.EmployeeID);
            ViewBag.ShiftID = new SelectList(Shiftdb.Shift(instanceId), "ShiftID", "ShiftName", employeeShift.ShiftID);
            return View(employeeShift);
        }

        // POST: HRnPayroll/EmployeeShifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeShiftID,EmployeeID,ShiftID,FromDate,TillDate,Particulars")] EmployeeShift employeeShift)
        {
            if (ModelState.IsValid)
            {
                if (employeeShift.FromDate <= employeeShift.TillDate)
                {
                    employeeShift.EntryBy = User.Identity.Name;
                    db.SaveEmployeeShift(employeeShift);
                    return RedirectToAction("Index");
                }
            }
            
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeID == employeeShift.EmployeeID && e.EmployeeStatus==0), "EmployeeID", "PIN", employeeShift.EmployeeID);
            ViewBag.ShiftID = new SelectList(Shiftdb.Shift(instanceId), "ShiftID", "ShiftName", employeeShift.ShiftID);
            return View(employeeShift);
        }

        // GET: HRnPayroll/EmployeeShifts/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            EmployeeShift employeeShift = db.Single(instanceId, id);
            if (employeeShift == null)
            {
                return RedirectToAction("Index");
            }
            return View(employeeShift);
        }

        // POST: HRnPayroll/EmployeeShifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            db.DeleteEmployeeShift(id);
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
            
            var EmployeeShift = db.Search(instanceId, txtSearch);
            return View("Index", EmployeeShift.ToList());
        }
    }
}
