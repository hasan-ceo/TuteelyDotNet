using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nyika.Domain.Concrete.MF;
using Nyika.Domain.Entities.MF;
using Nyika.Domain.Abstract.MF;
using Microsoft.AspNet.Identity;
using Nyika.WebUI.Models;
using Nyika.Domain.Abstract.HR;
using Nyika.Domain.Abstract.Accounts;
using Nyika.WebUI.Areas.MF.Models;

namespace Nyika.WebUI.Areas.MF.Controllers
{
    [Authorize(Roles = "Accountant,Super Admin")]
    public class LoanCollctionController : Controller
    {
        private IGroupsRepo db;
        private IProjectRepo pdb;
        private IEmployeeRepo ddb;
        private IBusinessDayRepo bddb;
        private ILoanCollectionRepo lcdb;
        private string instanceId;

        public LoanCollctionController(IGroupsRepo DB, IProjectRepo PDB, IEmployeeRepo DDB, IBusinessDayRepo bdDB, ILoanCollectionRepo LCDB)
        {
            this.db = DB;
            this.pdb = PDB;
            this.ddb = DDB;
            this.bddb = bdDB;
            this.lcdb = LCDB;
            instanceId = new InstanceVM().InstanceID;
        }

        public ActionResult GetGroups(long ProjectID)
        {
            var g1 = db.Groups(instanceId).Where(g => g.ProjectID == ProjectID && g.Inactive == false).OrderBy(g => g.GroupsName);
            var Groups = new SelectList(g1, "GroupsID", "GroupsName");
            return Json(Groups.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGroupsondate(long ProjectID)
        {
            //var products = lstProd.Where(p => p.CategoryID == CategorySubID);
            var g1 = db.GroupsToday(instanceId, ProjectID);
            var Groups = new SelectList(g1, "GroupsID", "GroupsName");
            return Json(Groups.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: BasicSetup/Groups
        public ActionResult Index()
        {
            DcrVM dcrvm = new DcrVM();
            dcrvm.Project = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName");
            return View(dcrvm);
        }


        // GET: BasicSetup/Groups
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(long SelectedProject, long SelectedGroup)
        {
            DcrVM dcrvm = new DcrVM();
            dcrvm.Project = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName", SelectedProject);
            dcrvm.SelectedProject = SelectedProject;
            dcrvm.SelectedGroup = SelectedGroup;
            dcrvm.loanCollection = lcdb.LoanCollection(instanceId, SelectedGroup).ToList();
            dcrvm.TotalTargetAmount = dcrvm.loanCollection.Sum(l => l.TargetAmount);
            return View(dcrvm);
        }

        // GET: BasicSetup/Groups/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName");
            ViewBag.EmployeeIdFC = new SelectList(ddb.Employee(instanceId), "EmployeeID", "EmployeeName");
            ViewBag.EmployeeIdCC = new SelectList(ddb.Employee(instanceId), "EmployeeID", "EmployeeName");
            return View();
        }


        // GET: BasicSetup/Groups
        public ActionResult LoanRealized()
        {
            DcrVM dcrvm = new DcrVM();
            dcrvm.Project = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName");
            return View("index", dcrvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoanRealized([Bind(Include = "LoanCollectionID,RealizedAmount,LoanID")]LoanRealizedVM[] loanColl)
        {
            var PreviousUrl = System.Web.HttpContext.Current.Request.UrlReferrer;

            if (ModelState.IsValid)
            {
                foreach (LoanRealizedVM lc in loanColl)
                {
                    lcdb.SaveLoanCollection(instanceId,lc.LoanCollectionID, lc.RealizedAmount, lc.LoanID,User.Identity.Name);
                }
                ViewBag.error = "Save Successful";
            }
            else
            {
                ViewBag.error = "Please check data";
            }
            return Redirect(PreviousUrl.ToString());
        }

        // GET: BasicSetup/Groups/Edit/5
        //public ActionResult Edit(long id=0)
        //{
        //    

        //    Groups Groups = db.Single(instanceId, id);
        //    if (Groups == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.EmployeeIdFC = new SelectList(ddb.Employee(instanceId), "EmployeeID", "EmployeeName", Groups.EmployeeIdFC);
        //    ViewBag.EmployeeIdCC = new SelectList(ddb.Employee(instanceId), "EmployeeID", "EmployeeName", Groups.EmployeeIdCC);
        //    ViewBag.ProjectID = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName", Groups.ProjectID);
        //    return View(Groups);
        //}

        // POST: BasicSetup/Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "GroupID,GroupsName,ProjectID,Gender,EmployeeIdFC,EmployeeIdCC,CreateDate,ColDay,ColStartDate,Frequency,Status")] Groups Groups)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SaveGroups(Groups);
        //        return RedirectToAction("Index");
        //    }
        //    
        //    ViewBag.EmployeeIdFC = new SelectList(ddb.Employee(instanceId), "EmployeeID", "EmployeeName", Groups.EmployeeIdFC);
        //    ViewBag.EmployeeIdCC = new SelectList(ddb.Employee(instanceId), "EmployeeID", "EmployeeName", Groups.EmployeeIdCC);
        //    ViewBag.ProjectID = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName", Groups.ProjectID);
        //    return View(Groups);
        //}

        // GET: BasicSetup/Groups/Delete/5
        public ActionResult Delete(long id = 0)
        {
            Groups Groups = db.Single(instanceId, id);
            if (Groups == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Groups.GroupsID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Groups);
        }

        // POST: BasicSetup/Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Groups Groups = db.Single(instanceId, id);
            if (Groups != null)
            {
                db.DeleteGroups(Groups.GroupsID);
            }
            return RedirectToAction("Index");
        }

    }
}
