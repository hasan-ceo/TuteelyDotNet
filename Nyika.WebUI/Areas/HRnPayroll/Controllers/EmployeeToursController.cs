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
    public class EmployeeToursController : Controller
    {
        private IEmployeeTourRepo db;
        private IEmployeeRepo employeedb;
        private IBusinessDayRepo bddb;
        private IAttenStatusRepo attsdb;
        private string instanceId;

        public EmployeeToursController(IEmployeeTourRepo DB, IEmployeeRepo EmployeeDB, IBusinessDayRepo bdDB, IAttenStatusRepo AttsDB)
        {
            this.db = DB;
            this.employeedb = EmployeeDB;
            this.bddb = bdDB;
            this.attsdb = AttsDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: HRnPayroll/EmployeeTours
        public ActionResult Index()
        {
            var employeeTours = db.EmployeeTour("x");
            return View(employeeTours.ToList());
        }

        

        // GET: HRnPayroll/EmployeeTours/Create
        public ActionResult Create()
        {
            
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN");
            ViewBag.TourType = new SelectList(attsdb.AttenStatus.Where(a => a.AttenStatusID == 7 || a.AttenStatusID == 8), "AttenStatusID", "AttenStatusName");
            return View();
        }

        // POST: HRnPayroll/EmployeeTours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeTourID,EmployeeID,FromDate,TillDate,ApplicationDate,Particulars,TourType")] EmployeeTour employeeTour)
        {
            
            if (ModelState.IsValid)
            {
                if (employeeTour.ApplicationDate <= employeeTour.FromDate && employeeTour.FromDate <= employeeTour.TillDate)
                {
                    employeeTour.WorkDate = bddb.WorkDate(instanceId);
                    employeeTour.EntryBy = User.Identity.Name;
                    employeeTour.InstanceID = instanceId;
                    db.SaveEmployeeTour(employeeTour);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN", employeeTour.EmployeeID);
            return View(employeeTour);
        }

        // GET: HRnPayroll/EmployeeTours/Edit/5
        public ActionResult Edit(long id=0)
        {
            
            EmployeeTour employeeTour = db.Single(instanceId, id);
            if (employeeTour == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeID == employeeTour.EmployeeID && e.EmployeeStatus == 0), "EmployeeID", "PIN", employeeTour.EmployeeID);
            ViewBag.TourType = new SelectList(attsdb.AttenStatus.Where(a => a.AttenStatusID == 7 || a.AttenStatusID == 8), "AttenStatusID", "AttenStatusName", employeeTour.TourType);
            return View(employeeTour);
        }

        // POST: HRnPayroll/EmployeeTours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeTourID,FromDate,TillDate,ApplicationDate,Particulars,TourType,EmployeeID")] EmployeeTour employeeTour)
        {
            if (ModelState.IsValid)
            {
                if (employeeTour.ApplicationDate <= employeeTour.FromDate && employeeTour.FromDate <= employeeTour.TillDate)
                {
                    employeeTour.EntryBy = User.Identity.Name;
                    db.SaveEmployeeTour(employeeTour);
                    return RedirectToAction("Index");
                }
            }
            
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeID == employeeTour.EmployeeID && e.EmployeeStatus == 0), "EmployeeID", "PIN", employeeTour.EmployeeID);
            ViewBag.TourType = new SelectList(attsdb.AttenStatus.Where(a => a.AttenStatusID == 7 || a.AttenStatusID == 8), "AttenStatusID", "AttenStatusName", employeeTour.TourType);

            return View(employeeTour);
        }

        // GET: HRnPayroll/EmployeeTours/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            EmployeeTour employeeTour = db.Single(instanceId, id);
            if (employeeTour == null)
            {
                return RedirectToAction("Index");
            }
            return View(employeeTour);
        }

        // POST: HRnPayroll/EmployeeTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            db.DeleteEmployeeTour(id);
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
            
            var EmployeeTour = db.Search(instanceId, txtSearch);
            return View("Index", EmployeeTour.ToList());
        }
    }
}
