using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Abstract.MF;
using Nyika.Domain.Entities.Accounts;
using Nyika.Domain.Entities.MF;
using Nyika.WebUI.Areas.MF.Models;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.MF.Controllers
{
    [Authorize(Roles = "Accountant,Super Admin")]
    public class LoanSettleController : Controller
    {
        private IAccountGLRepo db;
        private string instanceId;
        private IProjectRepo pdb;
        private ILoanRepo ldb;

        public LoanSettleController(IAccountGLRepo DB, IProjectRepo PDB, ILoanRepo LDB)
        {
            this.db = DB;
            this.pdb = PDB;
            this.ldb = LDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: MF/DCR
        public ActionResult Index()
        {
            return View(ldb.isSettle(instanceId).ToList());
        }

        // GET: MF/DCR
        public ActionResult RegularLoan()
        {
            return View("Index",ldb.isSettle(instanceId).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegularLoan(string txtSearch = "")
        {
            if (txtSearch == "")
            {
                txtSearch = "?";
            }
            var Loan = ldb.SearchRegular(instanceId, txtSearch);
            return View("Index", Loan.ToList());
        }

        

        // GET: BasicSetup/Groups/Delete/5
        public ActionResult Undo(long id = 0)
        {
            Loan Loan = ldb.Single(instanceId, id);
            if (Loan == null)
            {
                return RedirectToAction("Index");
            }

            TempData["Delete"] = "";
            //if (ldb.IsDelete(Loan.LoanID) != 0)
            //{
            //    TempData["Delete"] = "";
            //}
            //else
            //{
            //    TempData["Delete"] = "In use, can not delete at this moment";
            //}

            return View(Loan);
        }

        // POST: BasicSetup/Groups/Delete/5
        [HttpPost, ActionName("Undo")]
        [ValidateAntiForgeryToken]
        public ActionResult UndoConfirmed(long id=0)
        {
            Loan Loan = ldb.Single(instanceId, id);
            if (Loan != null)
            {
                if (ldb.UndoSettleLoan(instanceId, id) == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Undo", new { id = id });
                }
            }
            return RedirectToAction("Index");
        }



        // GET: BasicSetup/Groups/Delete/5
        public ActionResult Settle(long id = 0)
        {
            Loan Loan = ldb.Single(instanceId, id);
            if (Loan == null)
            {
                return RedirectToAction("Index");
            }

            TempData["Delete"] = "";
            //if (ldb.IsDelete(Loan.LoanID) != 0)
            //{
            //    TempData["Delete"] = "";
            //}
            //else
            //{
            //    TempData["Delete"] = "In use, can not delete at this moment";
            //}

            return View(Loan);
        }

        // POST: BasicSetup/Groups/Delete/5
        [HttpPost, ActionName("Settle")]
        [ValidateAntiForgeryToken]
        public ActionResult SettleConfirmed(long id = 0)
        {
            int r = 0;
            Loan Loan = ldb.Single(instanceId, id);
            if (Loan != null)
            {
                r = ldb.SettleLoan(instanceId, User.Identity.Name, id);
                if (r == 3)
                {
                    return RedirectToAction("Index");
                }
                else if (r == 2)
                {
                    ModelState.AddModelError("msgtmp", "Please contact with vendor");
                    return RedirectToAction("Settle", new { id = id });
                }
                else
                {
                    ModelState.AddModelError("msgtmp", "Please undo collection and try again.");
                    return RedirectToAction("Settle", new { id = id });
                }
            }
            return RedirectToAction("Index");
        }

    }
}