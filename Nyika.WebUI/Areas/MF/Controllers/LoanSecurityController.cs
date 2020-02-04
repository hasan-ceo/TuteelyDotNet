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
    public class LoanSecurityController : Controller
    {
        private IAccountGLRepo db;
        private string instanceId;
        private IProjectRepo pdb;

        public LoanSecurityController(IAccountGLRepo DB, IProjectRepo PDB)
        {
            this.db = DB;
            this.pdb = PDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: MF/DCR
        public ActionResult Index()
        {
            var dcr = db.SecurityList(instanceId);
            return View(dcr.ToList());
        }

        // GET: BasicSetup/Groups/Create
        public ActionResult Withdraw()
        {
            SecurityVM securityVM = new SecurityVM();
            securityVM.Project = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName");
            securityVM.Amount = 0;
            return View(securityVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Withdraw([Bind(Include = "SelectedProject,SelectedGroup,SelectedMember,Amount")] SecurityVM securityVM)
        {
            int r = 0;
            if (ModelState.IsValid)
            {


            r = db.SecurityWithdrawSave(securityVM.SelectedMember, securityVM.SelectedGroup, securityVM.Amount, securityVM.SelectedProject, instanceId, User.Identity.Name);

                
            }
            securityVM.Project = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName");
            if (r == 0)
            {
                return View(securityVM);
            }
            else if (r == 1)
            {
                securityVM.Particulars = "Loan exists, can not withdraw at this moment";
                return View(securityVM);
            }
            else if (r == 2)
            {
                securityVM.Particulars = "You are trying to withdraw more than security deposit";
                return View(securityVM);
            }
            else //if (r == 3)
            {
                return RedirectToAction("Index");
            }

        }


        // GET: BasicSetup/Groups/Delete/5
        public ActionResult Undo(long id = 0)
        {
            AccountGL accountGL = db.Single(instanceId, id);
            if (accountGL == null)
            {
                return RedirectToAction("Index");
            }

            return View(accountGL);
        }

        // POST: BasicSetup/Groups/Delete/5
        [HttpPost, ActionName("Undo")]
        [ValidateAntiForgeryToken]
        public ActionResult UndoConfirmed(long id)
        {
            if (db.DeleteSecurityWithdraw(id) == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Undo", new { id = id });
            }
        }
    }
}