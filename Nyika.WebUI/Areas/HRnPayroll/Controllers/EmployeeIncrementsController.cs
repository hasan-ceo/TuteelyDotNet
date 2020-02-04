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
    //[HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
    [Authorize(Roles = "HR Executive,Super Admin")]
    public class EmployeeIncrementsController : Controller
    {
        private IEmployeeIncrementRepo db;
        private IEmployeeRepo employeedb;
        private IBusinessDayRepo bddb;
        private string instanceId;

        public EmployeeIncrementsController(IEmployeeIncrementRepo DB, IEmployeeRepo EmployeeDB, IBusinessDayRepo bdDB)
        {
            this.db = DB;
            this.employeedb = EmployeeDB;
            this.bddb = bdDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: HRnPayroll/EmployeeIncrements
        public ActionResult Index()
        {
            var employeeIncrements = db.EmployeeIncrement("x");
            return View(employeeIncrements.ToList());
        }

       

        // GET: HRnPayroll/EmployeeIncrements/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN");
            return View();
        }

        // POST: HRnPayroll/EmployeeIncrements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeIncrementID,EmployeeID,EffectiveDate,BasicSalary,OtherBenefits,GrossSalary,LunchAllowance,ProfessionalAllowance,Particulars")] EmployeeIncrement employeeIncrement)
        {
            var wd = bddb.WorkDate(instanceId);
            if (ModelState.IsValid)
            {
                if (wd <= employeeIncrement.EffectiveDate)
                {
                    if (employeeIncrement.BasicSalary + employeeIncrement.OtherBenefits == employeeIncrement.GrossSalary)
                    {
                        employeeIncrement.WorkDate = bddb.WorkDate(instanceId);
                        employeeIncrement.EntryBy = User.Identity.Name;
                        employeeIncrement.InstanceID = instanceId;
                        db.SaveEmployeeIncrement(employeeIncrement);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("GrossSalary", "Gross Salary should be sum of Basic Salary and Other Benefits");
                    }
                }
                else
                {
                    ModelState.AddModelError("EffectiveDate", "Check Effective Date");
                }
            }
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN", employeeIncrement.EmployeeID);
            return View(employeeIncrement);
        }

        // GET: HRnPayroll/EmployeeIncrements/Edit/5
        public ActionResult Edit(long id=0)
        {
            EmployeeIncrement employeeIncrement = db.Single(instanceId, id);
            if (employeeIncrement == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeID == employeeIncrement.EmployeeID && e.EmployeeStatus == 0), "EmployeeID", "PIN", employeeIncrement.EmployeeID);
            return View(employeeIncrement);
        }

        // POST: HRnPayroll/EmployeeIncrements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeIncrementID,EmployeeID,IncrementID,EffectiveDate,BasicSalary,OtherBenefits,GrossSalary,LunchAllowance,ProfessionalAllowance,Particulars")] EmployeeIncrement employeeIncrement)
        {
            if (ModelState.IsValid)
            {
                var wd = bddb.WorkDate(instanceId);
                if (wd <= employeeIncrement.EffectiveDate)
                {
                    if (employeeIncrement.BasicSalary + employeeIncrement.OtherBenefits == employeeIncrement.GrossSalary)
                    {
                        employeeIncrement.EntryBy = User.Identity.Name;
                        db.SaveEmployeeIncrement(employeeIncrement);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("GrossSalary", "Gross Salary should be sum of Basic Salary and Other Benefits");
                    }
                }
                else
                {
                    ModelState.AddModelError("EffectiveDate", "Check Effective Date");
                }
            }
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeID == employeeIncrement.EmployeeID && e.EmployeeStatus == 0), "EmployeeID", "PIN", employeeIncrement.EmployeeID);
            return View(employeeIncrement);
        }

        // GET: HRnPayroll/EmployeeIncrements/Delete/5
        public ActionResult Delete(long id=0)
        {
            EmployeeIncrement employeeIncrement = db.Single(instanceId, id);
            if (employeeIncrement == null)
            {
                return RedirectToAction("Index");
            }
            return View(employeeIncrement);
        }

        // POST: HRnPayroll/EmployeeIncrements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            db.DeleteEmployeeIncrement(id);
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
            var EmployeeIncrement = db.Search(instanceId, txtSearch);
            return View("Index", EmployeeIncrement.ToList());
        }
    }
}
