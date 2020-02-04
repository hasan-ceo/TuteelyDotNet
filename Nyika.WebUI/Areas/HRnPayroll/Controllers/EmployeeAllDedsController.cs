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
    public class EmployeeAllDedsController : Controller
    {
        private IEmployeeAllDedRepo db;
        private IAllDedRepo AllDeddb;
        private IEmployeeRepo employeedb;
        private IBusinessDayRepo bddb;
        private string instanceId;

        public EmployeeAllDedsController(IEmployeeAllDedRepo DB, IAllDedRepo AllDedDB, IEmployeeRepo EmployeeDB, IBusinessDayRepo bdDB)
        {
            this.db = DB;
            this.employeedb = EmployeeDB;
            this.AllDeddb = AllDedDB;
            this.bddb = bdDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: HRnPayroll/EmployeeAllDeds
        public ActionResult Index()
        {
            var employeeAllDeds = db.EmployeeAllDed("x");
            return View(employeeAllDeds.ToList());
        }

        

        // GET: HRnPayroll/EmployeeAllDeds/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus==0), "EmployeeID", "PIN");
            ViewBag.AllDedID = new SelectList(AllDeddb.AllDed(instanceId), "AllDedID", "AllDedName");
            return View();
        }

        // POST: HRnPayroll/EmployeeAllDeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeAllDedID,EmployeeID,AllDedID,EffectiveDate,Amount,Particulars")] EmployeeAllDed employeeAllDed)
        {
            var wd= bddb.WorkDate(instanceId); 
            if (ModelState.IsValid)
            {
                if (wd <= employeeAllDed.EffectiveDate)
                {
                    employeeAllDed.WorkDate = bddb.WorkDate(instanceId);
                    employeeAllDed.EntryBy = User.Identity.Name;
                    employeeAllDed.InstanceID = instanceId;
                    db.SaveEmployeeAllDed(employeeAllDed);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.AllDedID = new SelectList(AllDeddb.AllDed(instanceId), "AllDedID", "AllDedName", employeeAllDed.AllDedID);
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN", employeeAllDed.EmployeeID);
            return View(employeeAllDed);
        }

        // GET: HRnPayroll/EmployeeAllDeds/Edit/5
        public ActionResult Edit(long id=0)
        {
            EmployeeAllDed employeeAllDed = db.Single(instanceId, id);
            if (employeeAllDed == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeID== employeeAllDed.EmployeeID && e.EmployeeStatus==0), "EmployeeID", "PIN", employeeAllDed.EmployeeID);
            ViewBag.AllDedID = new SelectList(AllDeddb.AllDed(instanceId), "AllDedID", "AllDedName", employeeAllDed.AllDedID);
            return View(employeeAllDed);
        }

        // POST: HRnPayroll/EmployeeAllDeds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeAllDedID,EmployeeID,AllDedID,EffectiveDate,Amount,Particulars")] EmployeeAllDed employeeAllDed)
        {
            if (ModelState.IsValid)
            {
                var wd = bddb.WorkDate(instanceId);
                if (wd <= employeeAllDed.EffectiveDate)
                {
                    employeeAllDed.EntryBy = User.Identity.Name;
                    db.SaveEmployeeAllDed(employeeAllDed);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeID == employeeAllDed.EmployeeID && e.EmployeeStatus==0), "EmployeeID", "PIN", employeeAllDed.EmployeeID);
            ViewBag.AllDedID = new SelectList(AllDeddb.AllDed(instanceId), "AllDedID", "AllDedName", employeeAllDed.AllDedID);
            return View(employeeAllDed);
        }

        // GET: HRnPayroll/EmployeeAllDeds/Delete/5
        public ActionResult Delete(long id=0)
        {
            EmployeeAllDed employeeAllDed = db.Single(instanceId, id);
            if (employeeAllDed == null)
            {
                return RedirectToAction("Index");
            }
            return View(employeeAllDed);
        }

        // POST: HRnPayroll/EmployeeAllDeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            db.DeleteEmployeeAllDed(id);
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
            var EmployeeAllDed = db.Search(instanceId, txtSearch);
            return View("Index", EmployeeAllDed.ToList());
        }
    }
}
