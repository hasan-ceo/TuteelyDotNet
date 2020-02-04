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
    public class AccountMainHeadsController : Controller
    {
        private IAccountMainHeadRepo db;
        private IAccountTypeRepo dbAT;
        private string instanceId;

        public AccountMainHeadsController(IAccountMainHeadRepo DB, IAccountTypeRepo DBAT)
        {
            this.db = DB;
            this.dbAT = DBAT;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: BasicSetup/AccountMainHeads
        public ActionResult Index()
        {
             
            return View(db.AccountMainHead(instanceId).ToList());
        }

       

        // GET: BasicSetup/AccountMainHeads/Create
        public ActionResult Create()
        {
            
            ViewBag.AccountTypeID = new SelectList(dbAT.AccountType(instanceId), "AccountTypeID", "AccountTypeName");
            return View();
        }

        // POST: BasicSetup/AccountMainHeads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountMainHeadID,AccountMainHeadName,AccountMainHeadCode,AccountTypeID")] AccountMainHead AccountMainHead)
        {
             
            if (ModelState.IsValid)
            {
                AccountMainHead.InstanceID = instanceId;
                AccountMainHead.AutoAc = false;
                db.SaveAccountMainHead(AccountMainHead);
                return RedirectToAction("Index");
            }
            ViewBag.AccountTypeID = new SelectList(dbAT.AccountType(instanceId), "AccountTypeID", "AccountTypeName", AccountMainHead.AccountTypeID);
            return View(AccountMainHead);
        }

        // GET: BasicSetup/AccountMainHeads/Edit/5
        public ActionResult Edit(long id=0)
        {
             
            AccountMainHead AccountMainHead = db.Single(instanceId, id);
            if (AccountMainHead == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.AccountTypeID = new SelectList(dbAT.AccountType(instanceId), "AccountTypeID", "AccountTypeName", AccountMainHead.AccountTypeID);
            return View(AccountMainHead);
        }

        // POST: BasicSetup/AccountMainHeads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountMainHeadID,AccountMainHeadName,AccountMainHeadCode,AccountTypeID")] AccountMainHead AccountMainHead)
        {
            
            if (ModelState.IsValid)
            {
                AccountMainHead.AutoAc = false;
                db.SaveAccountMainHead(AccountMainHead);
                return RedirectToAction("Index");
            }
            ViewBag.AccountTypeID = new SelectList(dbAT.AccountType(instanceId), "AccountTypeID", "AccountTypeName", AccountMainHead.AccountTypeID);

            return View(AccountMainHead);
        }

        // GET: BasicSetup/AccountMainHeads/Delete/5
        public ActionResult Delete(long id=0)
        {

             
            AccountMainHead AccountMainHead = db.Single(instanceId, id);
            if (AccountMainHead == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(AccountMainHead.AccountMainHeadID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }


            return View(AccountMainHead);
        }

        // POST: BasicSetup/AccountMainHeads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
             
            AccountMainHead AccountMainHead = db.Single(instanceId , id);
            if (AccountMainHead != null)
            {
                db.DeleteAccountMainHead(AccountMainHead.AccountMainHeadID);
            }            
            return RedirectToAction("Index");
        }

    }
}
