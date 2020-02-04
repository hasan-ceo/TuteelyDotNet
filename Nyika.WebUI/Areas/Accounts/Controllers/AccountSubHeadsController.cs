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
using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Entities.Accounts;
using Nyika.WebUI.Models;

namespace Nyika.WebUI.Areas.Accounts.Controllers
{
    [Authorize(Roles = "Accountant,Super Admin")]
    public class AccountSubHeadsController : Controller
    {
        private IAccountSubHeadRepo db;
        private IAccountMainHeadRepo dbMH;
        private string instanceId;

        public AccountSubHeadsController(IAccountSubHeadRepo DB, IAccountMainHeadRepo DBMH)
        {
            this.db = DB;
            this.dbMH = DBMH;
            instanceId = new InstanceVM().InstanceID;

        }


        // GET: BasicSetup/AccountSubHeads
        public ActionResult Index()
        {
            
            return View(db.AccountSubHead(instanceId).ToList());
        }

        

        // GET: BasicSetup/AccountSubHeads/Create
        public ActionResult Create()
        {
            
            ViewBag.AccountMainHeadID = new SelectList(dbMH.AccountMainHead(instanceId).Where(a => !(a.AccountMainHeadName== "Cash" || a.AccountMainHeadName == "Party")), "AccountMainHeadID", "AccountMainHeadName");
           
            return View();
        }

        // POST: BasicSetup/AccountSubHeads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountSubHeadID,AccountSubHeadName,AccountMainHeadID,AccountSubHeadCode")] AccountSubHead AccountSubHead)
        {
            
            if (ModelState.IsValid)
            {
                AccountSubHead.InstanceID = instanceId;
                AccountSubHead.AutoAc = false;
                db.SaveAccountSubHead(AccountSubHead);
                return RedirectToAction("Index");
            }
            ViewBag.AccountMainHeadID = new SelectList(dbMH.AccountMainHead(instanceId), "AccountMainHeadID", "AccountMainHeadName");
            return View(AccountSubHead);
        }

        // GET: BasicSetup/AccountSubHeads/Edit/5
        public ActionResult Edit(long id=0)
        {
            
            AccountSubHead AccountSubHead = db.AccountSubHead(instanceId).Where(r => r.AccountSubHeadID == id).FirstOrDefault();
            if (AccountSubHead == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.AccountMainHeadID = new SelectList(dbMH.AccountMainHead(instanceId), "AccountMainHeadID", "AccountMainHeadName", AccountSubHead.AccountMainHeadID);
            return View(AccountSubHead);
        }

        // POST: BasicSetup/AccountSubHeads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountSubHeadID,AccountSubHeadName,AccountMainHeadID,AccountSubHeadCode")] AccountSubHead AccountSubHead)
        {
            
            if (ModelState.IsValid)
            {
                AccountSubHead.AutoAc = false;
                db.SaveAccountSubHead(AccountSubHead);
                return RedirectToAction("Index");
            }
            ViewBag.AccountMainHeadID = new SelectList(dbMH.AccountMainHead(instanceId), "AccountMainHeadID", "AccountMainHeadName", AccountSubHead.AccountMainHeadID);
                       return View(AccountSubHead);
        }

        // GET: BasicSetup/AccountSubHeads/Delete/5
        public ActionResult Delete(long id=0)
        {

            
            AccountSubHead AccountSubHead = db.Single(instanceId, id);
            
            if (AccountSubHead == null)
            {
                return RedirectToAction("Index");
            }


            if (db.IsExists(AccountSubHead.AccountSubHeadID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(AccountSubHead);
        }

        // POST: BasicSetup/AccountSubHeads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            
            AccountSubHead AccountSubHead = db.Single(instanceId,id);
            if (AccountSubHead != null)
            {
                db.DeleteAccountSubHead(AccountSubHead.AccountSubHeadID);
            }
            return RedirectToAction("Index");
        }

    }
}
