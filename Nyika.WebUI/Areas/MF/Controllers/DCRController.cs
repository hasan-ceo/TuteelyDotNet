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
    public class DCRController : Controller
    {
        private IAccountGLRepo db;
        private string instanceId;
        private IProjectRepo pdb;

        public DCRController(IAccountGLRepo DB, IProjectRepo PDB)
        {
            this.db = DB;
            this.pdb = PDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: MF/DCR
        public ActionResult Index()
        {
            var dcr = db.DclList(instanceId);
            return View(dcr.ToList());
        }

        // GET: BasicSetup/Groups/Create
        public ActionResult Create()
        {
            DcrVM dcrvm = new DcrVM();
            dcrvm.Project = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName");
            return View(dcrvm);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SelectedProject,SelectedGroup,Amount,Particulars")] DcrVM dcrvm)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(dcrvm.Particulars) ==true)
                {
                    dcrvm.Particulars = "";
                }
                db.DCRSave(dcrvm.SelectedGroup , dcrvm.Amount, dcrvm.Particulars, dcrvm.SelectedProject, instanceId, User.Identity.Name);
                return RedirectToAction("Index");
            }
            dcrvm.Project = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName");
            return View(dcrvm);
        }


        // GET: BasicSetup/Groups/Delete/5
        public ActionResult Delete(long id = 0)
        {
            AccountGL accountGL = db.Single(instanceId, id);
            if (accountGL == null)
            {
                return RedirectToAction("Index");
            }

            return View(accountGL);
        }

        // POST: BasicSetup/Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            if (db.DeleteDCR(id) == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete",new { id=id});
            }
        }
    }
}