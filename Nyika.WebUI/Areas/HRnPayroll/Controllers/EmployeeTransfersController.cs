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
    public class EmployeeTransfersController : Controller
    {
        private IEmployeeTransferRepo db;
        private IEmployeeRepo employeedb;
        private IBusinessDayRepo bddb;
        private IDesignationRepo designationdb;
        private IDepartmentRepo departmentdb;
        private IEmploymentTypeRepo employmenttypedb;
        private ISectionRepo sectiondb;
        private string instanceId;

        public EmployeeTransfersController(IEmployeeTransferRepo DB,  IEmployeeRepo EmployeeDB, IBusinessDayRepo bdDB, IDesignationRepo DesignationDB, IDepartmentRepo DepartmentDB, IEmploymentTypeRepo EmploymentTypeDB, ISectionRepo SectiondDB)
        {
            this.db = DB;
            this.employeedb = EmployeeDB;
            this.bddb = bdDB;
            this.designationdb = DesignationDB;
            this.departmentdb = DepartmentDB;
            this.employmenttypedb = EmploymentTypeDB;
            this.sectiondb = SectiondDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: HRnPayroll/EmployeeTransfers
        public ActionResult Index()
        {
           
            var employeeTransfers = db.Search(instanceId, "x");
            return View(employeeTransfers.ToList());
        }

        

        // GET: HRnPayroll/EmployeeTransfers/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus==0), "EmployeeID", "PIN");
            ViewBag.DepartmentID = new SelectList(departmentdb.Department(instanceId), "DepartmentID", "DepartmentName");
            ViewBag.DesignationID = new SelectList(designationdb.Designation(instanceId), "DesignationID", "DesignationName");
            ViewBag.EmploymentTypeID = new SelectList(employmenttypedb.EmploymentType(instanceId), "EmploymentTypeID", "EmploymentTypeName");
            ViewBag.SectionID = new SelectList(sectiondb.Section(instanceId), "SectionID", "SectionName");

            return View();
        }

        // POST: HRnPayroll/EmployeeTransfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeTransferID,EmployeeID,EffectiveDate,Particulars,DepartmentID,SectionID,DesignationID,EmploymentTypeID")] EmployeeTransfer employeeTransfer)
        {
            var wd= bddb.WorkDate(instanceId); 
            if (ModelState.IsValid)
            {
                if (wd <= employeeTransfer.EffectiveDate)
                {
                    employeeTransfer.WorkDate = bddb.WorkDate(instanceId);
                    employeeTransfer.EntryBy = User.Identity.Name;
                    employeeTransfer.InstanceID = instanceId;
                    db.SaveEmployeeTransfer(employeeTransfer);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.DepartmentID = new SelectList(departmentdb.Department(instanceId), "DepartmentID", "DepartmentName", employeeTransfer.DepartmentID);
            ViewBag.DesignationID = new SelectList(designationdb.Designation(instanceId), "DesignationID", "DesignationName", employeeTransfer.DesignationID);
            ViewBag.EmploymentTypeID = new SelectList(employmenttypedb.EmploymentType(instanceId), "EmploymentTypeID", "EmploymentTypeName", employeeTransfer.EmploymentTypeID);
            ViewBag.SectionID = new SelectList(sectiondb.Section(instanceId), "SectionID", "SectionName", employeeTransfer.SectionID);
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeStatus == 0), "EmployeeID", "PIN", employeeTransfer.EmployeeID);
            return View(employeeTransfer);
        }

        // GET: HRnPayroll/EmployeeTransfers/Edit/5
        public ActionResult Edit(long id=0)
        {
            EmployeeTransfer employeeTransfer = db.Single(instanceId, id);
            if (employeeTransfer == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeID== employeeTransfer.EmployeeID && e.EmployeeStatus==0), "EmployeeID", "PIN", employeeTransfer.EmployeeID);
            ViewBag.DepartmentID = new SelectList(departmentdb.Department(instanceId), "DepartmentID", "DepartmentName", employeeTransfer.DepartmentID);
            ViewBag.DesignationID = new SelectList(designationdb.Designation(instanceId), "DesignationID", "DesignationName", employeeTransfer.DesignationID);
            ViewBag.EmploymentTypeID = new SelectList(employmenttypedb.EmploymentType(instanceId), "EmploymentTypeID", "EmploymentTypeName", employeeTransfer.EmploymentTypeID);
            ViewBag.SectionID = new SelectList(sectiondb.Section(instanceId), "SectionID", "SectionName", employeeTransfer.SectionID);
            return View(employeeTransfer);
        }

        // POST: HRnPayroll/EmployeeTransfers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeTransferID,EmployeeID,EffectiveDate,Particulars,DepartmentID,SectionID,DesignationID,EmploymentTypeID")] EmployeeTransfer employeeTransfer)
        {
            if (ModelState.IsValid)
            {
                var wd = bddb.WorkDate(instanceId);
                if (wd <= employeeTransfer.EffectiveDate)
                {
                    employeeTransfer.EntryBy = User.Identity.Name;
                    db.SaveEmployeeTransfer(employeeTransfer);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.EmployeeID = new SelectList(employeedb.Employee(instanceId).Where(e => e.EmployeeID == employeeTransfer.EmployeeID && e.EmployeeStatus==0), "EmployeeID", "PIN", employeeTransfer.EmployeeID);
            ViewBag.DepartmentID = new SelectList(departmentdb.Department(instanceId), "DepartmentID", "DepartmentName", employeeTransfer.DepartmentID);
            ViewBag.DesignationID = new SelectList(designationdb.Designation(instanceId), "DesignationID", "DesignationName", employeeTransfer.DesignationID);
            ViewBag.EmploymentTypeID = new SelectList(employmenttypedb.EmploymentType(instanceId), "EmploymentTypeID", "EmploymentTypeName", employeeTransfer.EmploymentTypeID);
            ViewBag.SectionID = new SelectList(sectiondb.Section(instanceId), "SectionID", "SectionName", employeeTransfer.SectionID);
            return View(employeeTransfer);
        }

        // GET: HRnPayroll/EmployeeTransfers/Delete/5
        public ActionResult Delete(long id=0)
        {
            EmployeeTransfer employeeTransfer = db.Single(instanceId, id);

            if (employeeTransfer == null)
            {
                return RedirectToAction("Index");
            }
            return View(employeeTransfer);
        }

        // POST: HRnPayroll/EmployeeTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            db.DeleteEmployeeTransfer(id);
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

            var EmployeeTransfer = db.Search(instanceId, txtSearch);
            return View("Index", EmployeeTransfer.ToList());
        }
    }
}
