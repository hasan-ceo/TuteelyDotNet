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
    public class LoansController : Controller
    {
        private ILoanRepo db;
        private IGroupsRepo gdb;
        private IProductRepo pdb;
        private IMemberRepo mdb;
        private ISchemeRepo sdb;
        private IBusinessDayRepo bddb;
        private string instanceId;

        public LoansController(ILoanRepo DB, IGroupsRepo gDB, IProductRepo PDB, IMemberRepo MDB, ISchemeRepo SDB, IBusinessDayRepo bdDB)
        {
            this.db = DB;
            this.gdb = gDB;
            this.pdb = PDB;
            this.mdb = MDB;
            this.sdb = SDB;
            this.bddb = bdDB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: BasicSetup/Groups
        public ActionResult Index()
        {
            return View(db.UnAprrovedLoan(instanceId).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string txtSearch = "")
        {
            if (txtSearch == "")
            {
                txtSearch = "?";
            }
            var Loan = db.Search(instanceId, txtSearch);
            return View("Index", Loan.ToList());
        }


        public ActionResult GetLoan(long MemberID)
        {
            var loan = new SelectList(db.Loan(instanceId).Where(m => m.MemberID == MemberID), "LoanID", "LoanNo");
            return Json(loan.ToList(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetLoanActive(long MemberID)
        {
            var loan = new SelectList(db.Loan(instanceId).Where(m => m.MemberID == MemberID && m.LoanStatus!="Close"), "LoanID", "LoanNo");
            return Json(loan.ToList(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult SelectMember()
        {
            return View(mdb.Member("x").ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectMember(string txtSearch = "")
        {
            if (txtSearch == "")
            {
                txtSearch = "?";
            }

            var Member = mdb.Search(instanceId, txtSearch,true);
            return View(Member.ToList());
        }


        public ActionResult Create(long id=0)
        {
            if (id == 0)
            {
                ModelState.AddModelError("MemberID", "Selected Member");
            }
            else
            {
                if (db.IsMemberExists(id) == 0)
                {
                    Member Member = mdb.Single(instanceId, id);
                    LoanVM loanvm = new LoanVM();
                    loanvm.MemberID = Member.MemberID;
                    loanvm.ProjectName = Member.Groups.Project.ProjectName;
                    loanvm.GroupsName = Member.Groups.GroupsName;
                    loanvm.MemberNo = Member.MemberNo;
                    loanvm.MemberName = Member.MemberName;
                    ViewBag.ProductID = new SelectList(pdb.ProductProjectWise(instanceId, Member.Groups.ProjectID), "ProductID", "ProductName");
                    ViewBag.SchemeID = new SelectList(sdb.Scheme(instanceId), "SchemeID", "SchemeName");
                    return View(loanvm);
                }
                else
                {
                    ModelState.AddModelError("MemberID", "Selected Member Already Has  Active Loan");
                }
            }
            return View("SelectMember", mdb.Member("x").ToList());
        }

        // POST: BasicSetup/Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberID,ProductID,SchemeID,DisbursedAmount")] LoanVM loanVM)
        {
            if (ModelState.IsValid)
            {
                var t=db.SaveLoan(loanVM.MemberID, loanVM.ProductID, loanVM.SchemeID, loanVM.DisbursedAmount, instanceId, User.Identity.Name);
                if (t == 3)
                {
                    ModelState.AddModelError("MemberID", "Save Successful");
                }
                else if (t == 2)
                { 
                    ModelState.AddModelError("MemberID", "Member has loan, please enter select correct member");
                }
                else if (t == 1)
                {
                    ModelState.AddModelError("MemberID", "Please check project Start-Date/End-Date/First-Disbursement-Date");
                }
                return View("SelectMember", mdb.Member("x").ToList());

            }
            //ViewBag.GroupsID = new SelectList(gdb.Groups(instanceId), "GroupsID", "GroupsName", loanVM.GroupsID);
           // ViewBag.MemberID = new SelectList(mdb.Member(instanceId), "MemberID", "MemberName", loanVM.MemberID);
            ViewBag.ProductID = new SelectList(pdb.Product(instanceId), "ProductID", "ProductName", loanVM.ProductID);
            ViewBag.SchemeID = new SelectList(sdb.Scheme(instanceId), "SchemeID", "SchemeName", loanVM.SchemeID);
            return View(loanVM);
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

        // GET: BasicSetup/Groups/Approve/5
        public ActionResult Approve(long id=0)
        {
            Loan Loan = db.Single(instanceId, id);
            if (Loan == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsApproved(Loan.LoanID) != 0)
            {
                TempData["Approve"] = "Already Approved";
            }
            else
            {
                TempData["Approve"] = "";
            }

            return View(Loan);
        }

        // POST: BasicSetup/Groups/Delete/5
        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveConfirmed(long id)
        {
            Loan Loan = db.Single(instanceId, id);
            if (Loan != null)
            {
                db.Approved(Loan.LoanID, instanceId, User.Identity.Name);
            }
            return RedirectToAction("Index");
        }

        // GET: BasicSetup/Groups/Delete/5
        public ActionResult Delete(long id=0)
        {
            Loan Loan = db.Single(instanceId, id);
            if (Loan == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsDelete(Loan.LoanID) != 0)
            {
                TempData["Delete"] = "";
            }
            else
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }

            return View(Loan);
        }

        // POST: BasicSetup/Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Loan Loan = db.Single(instanceId, id);
            if (Loan != null)
            {
                db.DeleteLoan(Loan.LoanID);
            }
            return RedirectToAction("Index");
        }

    }
}
