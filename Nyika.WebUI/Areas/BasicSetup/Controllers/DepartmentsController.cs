using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nyika.Domain.Concrete.Setup;
using Nyika.Domain.Entities.Setup;
using Nyika.Domain.Abstract.Setup;
using Microsoft.AspNet.Identity;
using Nyika.WebUI.Models;

namespace Nyika.WebUI.Areas.BasicSetup.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class DepartmentsController : Controller
    {
        private IDepartmentRepo db;
        private string instanceId;

        public DepartmentsController(IDepartmentRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: BasicSetup/Departments
        public ActionResult Index()
        {
            
            return View(db.Department(instanceId).ToList());
        }

        

        // GET: BasicSetup/Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,DepartmentName")] Department Department)
        {
            
            if (ModelState.IsValid)
            {
                Department.InstanceID = instanceId;
                db.SaveDepartment(Department);
                return RedirectToAction("Index");
            }

            return View(Department);
        }

        // GET: BasicSetup/Departments/Edit/5
        public ActionResult Edit(long id=0)
        {
            

            Department Department = db.Single(instanceId, id);
            if (Department == null)
            {
                return RedirectToAction("Index");
            }
            return View(Department);
        }

        // POST: BasicSetup/Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentID,DepartmentName")] Department Department)
        {
            if (ModelState.IsValid)
            {
                
                db.SaveDepartment(Department);
                return RedirectToAction("Index");
            }
            return View(Department);
        }

        // GET: BasicSetup/Departments/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            
            Department Department = db.Single(instanceId, id);

            if (Department == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Department.DepartmentID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Department);
        }

        // POST: BasicSetup/Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Department Department = db.Single(instanceId, id);
            if (Department != null)
            {
                db.DeleteDepartment(Department.DepartmentID);
            }
            return RedirectToAction("Index");
        }

    }
}
