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
    public class EmployeeLeavesController : Controller
    {
        private IEmployeeLeaveRepo db;
        private ILeaveRepo leavedb;
        private IEmployeeRepo employeedb;
        private IBusinessDayRepo bddb;
        private string instanceId;

        public EmployeeLeavesController(IEmployeeLeaveRepo DB, ILeaveRepo LeaveDB, IEmployeeRepo EmployeeDB, IBusinessDayRepo bdDB)
        {
            this.db = DB;
            this.employeedb = EmployeeDB;
            this.leavedb = LeaveDB;
            this.bddb = bdDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: HRnPayroll/EmployeeLeaves
        public ActionResult Index()
        {
            var employeeLeaves = db.EmployeeLeave("x");
            return View(employeeLeaves.ToList());
        }

        

        // GET: HRnPayroll/EmployeeLeaves/Create
        public ActionResult Create()
        {
            
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN");
            ViewBag.LeaveID = new SelectList(leavedb.Leave(instanceId), "LeaveID", "LeaveName");
            return View();
        }

        // POST: HRnPayroll/EmployeeLeaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeLeaveID,EmployeeID,LeaveID,FromDate,TillDate,ApplicationDate,Particulars,Withoutpay")] EmployeeLeave employeeLeave)
        {
            
            if (ModelState.IsValid)
            {
                if (employeeLeave.ApplicationDate <= employeeLeave.FromDate && employeeLeave.FromDate <= employeeLeave.TillDate)
                {
                    employeeLeave.WorkDate = bddb.WorkDate(instanceId);
                    employeeLeave.EntryBy = User.Identity.Name;
                    employeeLeave.InstanceID = instanceId;
                    db.SaveEmployeeLeave(employeeLeave);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.LeaveID = new SelectList(leavedb.Leave(instanceId), "LeaveID", "LeaveName", employeeLeave.LeaveID);
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN", employeeLeave.EmployeeID);
            return View(employeeLeave);
        }

        // GET: HRnPayroll/EmployeeLeaves/Edit/5
        public ActionResult Edit(long id=0)
        {
            
            EmployeeLeave employeeLeave = db.Single(instanceId, id);
            if (employeeLeave == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0 && e.EmployeeID == employeeLeave.EmployeeID), "EmployeeID", "PIN", employeeLeave.EmployeeID);
            ViewBag.LeaveID = new SelectList(leavedb.Leave(instanceId), "LeaveID", "LeaveName", employeeLeave.LeaveID);
            return View(employeeLeave);
        }

        // POST: HRnPayroll/EmployeeLeaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeLeaveID,EmployeeID,LeaveID,FromDate,TillDate,ApplicationDate,Particulars,Withoutpay")] EmployeeLeave employeeLeave)
        {
            if (ModelState.IsValid)
            {
                if (employeeLeave.ApplicationDate <= employeeLeave.FromDate && employeeLeave.FromDate <= employeeLeave.TillDate)
                {
                    employeeLeave.EntryBy = User.Identity.Name;
                    db.SaveEmployeeLeave(employeeLeave);
                    return RedirectToAction("Index");
                }
            }
            
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0 && e.EmployeeID == employeeLeave.EmployeeID), "EmployeeID", "PIN", employeeLeave.EmployeeID);
            ViewBag.LeaveID = new SelectList(leavedb.Leave(instanceId), "LeaveID", "LeaveName", employeeLeave.LeaveID);
            return View(employeeLeave);
        }

        // GET: HRnPayroll/EmployeeLeaves/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            EmployeeLeave employeeLeave = db.Single(instanceId, id);
            if (employeeLeave == null)
            {
                return RedirectToAction("Index");
            }
            return View(employeeLeave);
        }

        // POST: HRnPayroll/EmployeeLeaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            db.DeleteEmployeeLeave(id);
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
            
            var EmployeeLeave = db.Search(instanceId, txtSearch);
            return View("Index", EmployeeLeave.ToList());
        }
    }
}
