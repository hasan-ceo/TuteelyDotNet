using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nyika.Domain.Concrete.HR;
using Nyika.Domain.Entities.HR;
using Nyika.Domain.Abstract.HR;
using Microsoft.AspNet.Identity;
using Nyika.Domain.Abstract.Setup;
using Nyika.WebUI.Models;
using Nyika.Domain.Abstract.Accounts;
using System.IO;

namespace Nyika.WebUI.Areas.HRnPayroll.Controllers
{
    [Authorize(Roles = "HR Executive,Super Admin")]
    public class EmployeesController : Controller
    {

        private IEmployeeRepo db;
        private ICompanyRepo companydb;
        private IDesignationRepo designationdb;
        private IDepartmentRepo departmentdb;
        private IEmploymentTypeRepo employmenttypedb;
        private ISectionRepo sectiondb;
        private IEducationRepo educationdb;
        private IBusinessDayRepo bddb;
        private string instanceId;

        public EmployeesController(IEmployeeRepo DB, ICompanyRepo CompanyDB, IDesignationRepo DesignationDB, IDepartmentRepo DepartmentDB, IEmploymentTypeRepo EmploymentTypeDB, ISectionRepo SectiondDB, IEducationRepo EducationDB, IBusinessDayRepo bdDB)
        {
            this.db = DB;
            this.companydb = CompanyDB;
            this.designationdb = DesignationDB;
            this.departmentdb = DepartmentDB;
            this.employmenttypedb = EmploymentTypeDB;
            this.sectiondb = SectiondDB;
            this.educationdb = EducationDB;
            this.bddb = bdDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: Employees
        public ActionResult Index()
        {
            // 
            var employee = db.Employee("x");
            return View(employee.ToList());
        }

       

        // GET: Employees/Create
        public ActionResult Create()
        {
            
            ViewBag.CompanyID = new SelectList(companydb.Company(instanceId), "CompanyID", "CompanyName");
            ViewBag.DepartmentID = new SelectList(departmentdb.Department(instanceId), "DepartmentID", "DepartmentName");
            ViewBag.DesignationID = new SelectList(designationdb.Designation(instanceId), "DesignationID", "DesignationName");
            ViewBag.EmploymentTypeID = new SelectList(employmenttypedb.EmploymentType(instanceId), "EmploymentTypeID", "EmploymentTypeName");
            ViewBag.SectionID = new SelectList(sectiondb.Section(instanceId), "SectionID", "SectionName");
            ViewBag.EducationID = new SelectList(educationdb.Education(instanceId), "EducationID", "EducationName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,PIN,EmployeeName,CompanyID,DepartmentID,SectionID,DesignationID,EmploymentTypeID,JoiningDate,BasicSalary,OtherBenefits,GrossSalary,LunchAllowance,ProfessionalAllowance,PPFNSSFNumber,BankAccountNumber,BankName,ContractStartDate,ContractEndDate,ContactNumber,Email,DoB,Gender,MotherName,FatherName,Address,Religion,MaritalStatus,BloodGroup,EducationID,CardNumber,PPFNSSF,IndexNumber")] Employee employee, HttpPostedFileBase ImageUpload)
        {
            var validImageTypes = new string[]
            {
                 //"image/gif",
                //"image/pjpeg",
                //"image/png",
                "image/jpeg"
            };

            
            if (ModelState.IsValid)
            {
                if (db.isEmailExists(instanceId,employee.Email) == false)
                {
                    if ((DateTime.Now.Date.Year - employee.DoB.Year) >= 18)
                    {
                        if (employee.BasicSalary + employee.OtherBenefits == employee.GrossSalary)
                        {
                            employee.ImageUrl = "~/images/employee.jpg";
                            if (ImageUpload != null && ImageUpload.ContentLength > 0)
                            {
                                if (validImageTypes.Contains(ImageUpload.ContentType))
                                {
                                    var ImageName = Guid.NewGuid().ToString() + ".jpg";
                                    var uploadDir = "~/Images/Company";
                                    var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageName);
                                    ImageUpload.SaveAs(imagePath);
                                    var imageUrl = Path.Combine(uploadDir, ImageName);
                                    employee.ImageUrl = imageUrl.Replace("\\", "/");
                                }
                            }

                            employee.WorkDate = bddb.WorkDate(instanceId);
                            employee.EntryBy = User.Identity.Name;
                            employee.EmployeeStatus = 0;
                            employee.InstanceID = instanceId;
                            db.SaveEmployee(employee);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("GrossSalary", "Gross Salary should be sum of Basic Salary and Other Benefits");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("DoB", "Check Date of Birth");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Email already Exists");
                }
            }

            ViewBag.CompanyID = new SelectList(companydb.Company(instanceId), "CompanyID", "CompanyName", employee.CompanyID);
            ViewBag.DepartmentID = new SelectList(departmentdb.Department(instanceId), "DepartmentID", "DepartmentName", employee.DepartmentID);
            ViewBag.DesignationID = new SelectList(designationdb.Designation(instanceId), "DesignationID", "DesignationName", employee.DesignationID);
            ViewBag.EmploymentTypeID = new SelectList(employmenttypedb.EmploymentType(instanceId), "EmploymentTypeID", "EmploymentTypeName", employee.EmploymentTypeID);
            ViewBag.SectionID = new SelectList(sectiondb.Section(instanceId), "SectionID", "SectionName", employee.SectionID);
            ViewBag.EducationID = new SelectList(educationdb.Education(instanceId), "EducationID", "EducationName", employee.EducationID);

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(long id=0)
        {
            
            Employee employee = db.Single(instanceId, id);
            if (employee == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(companydb.Company(instanceId), "CompanyID", "CompanyName", employee.CompanyID);
            ViewBag.DepartmentID = new SelectList(departmentdb.Department(instanceId), "DepartmentID", "DepartmentName", employee.DepartmentID);
            ViewBag.DesignationID = new SelectList(designationdb.Designation(instanceId), "DesignationID", "DesignationName", employee.DesignationID);
            ViewBag.EmploymentTypeID = new SelectList(employmenttypedb.EmploymentType(instanceId), "EmploymentTypeID", "EmploymentTypeName", employee.EmploymentTypeID);
            ViewBag.SectionID = new SelectList(sectiondb.Section(instanceId), "SectionID", "SectionName", employee.SectionID);
            ViewBag.EducationID = new SelectList(educationdb.Education(instanceId), "EducationID", "EducationName", employee.EducationID);


            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,PIN,EmployeeName,CompanyID,DepartmentID,SectionID,DesignationID,EmploymentTypeID,JoiningDate,BasicSalary,OtherBenefits,GrossSalary,LunchAllowance,ProfessionalAllowance,PPFNSSFNumber,BankAccountNumber,BankName,ContractStartDate,ContractEndDate,ContactNumber,Email,DoB,Gender,MotherName,FatherName,Address,Religion,MaritalStatus,BloodGroup,EducationID,CardNumber,ImageUrl,PPFNSSF,IndexNumber")] Employee employee, HttpPostedFileBase ImageUpload)
        {
            
            if (ModelState.IsValid)
            {
                var validImageTypes = new string[]
                {
                 //"image/gif",
                //"image/pjpeg",
                //"image/png",
                "image/jpeg"
                };

                if (ImageUpload != null && ImageUpload.ContentLength > 0)
                {
                    if (validImageTypes.Contains(ImageUpload.ContentType))
                    {
                        var uploadDir = "~/Images/Company";
                        string filename = Path.GetFileName(employee.ImageUrl);
                        if (filename == "employee.jpg")
                        {
                            filename = Guid.NewGuid().ToString() + ".jpg";
                        }
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), filename);
                        ImageUpload.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, filename);
                        employee.ImageUrl = imageUrl.Replace("\\", "/");
                    }
                }
                //employee.EmployeeStatus = 0;
                employee.EntryBy = User.Identity.Name;
                db.SaveEmployee(employee);
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(companydb.Company(instanceId), "CompanyID", "CompanyName", employee.CompanyID);
            ViewBag.DepartmentID = new SelectList(departmentdb.Department(instanceId), "DepartmentID", "DepartmentName", employee.DepartmentID);
            ViewBag.DesignationID = new SelectList(designationdb.Designation(instanceId), "DesignationID", "DesignationName", employee.DesignationID);
            ViewBag.EmploymentTypeID = new SelectList(employmenttypedb.EmploymentType(instanceId), "EmploymentTypeID", "EmploymentTypeName", employee.EmploymentTypeID);
            ViewBag.SectionID = new SelectList(sectiondb.Section(instanceId), "SectionID", "SectionName", employee.SectionID);
            ViewBag.EducationID = new SelectList(educationdb.Education(instanceId), "EducationID", "EducationName", employee.EducationID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            Employee employee = db.Single(instanceId, id);
            if (employee == null)
            {
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            
            Employee employee = db.Single(instanceId, id);
            if (employee != null)
            {
                db.DeleteEmployee(employee.EmployeeID);
            }
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
            
            var Employee = db.Search(instanceId, txtSearch);
            return View("Index", Employee.ToList());
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

    }
}
