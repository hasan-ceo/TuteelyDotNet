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
    public class BanksController : Controller
    {
        private IBankRepo db;
        private string instanceId;

        public BanksController(IBankRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: BasicSetup/Banks
        public ActionResult Index()
        {
             
            return View(db.Bank(instanceId).ToList());
        }

      

        // GET: BasicSetup/Banks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/Banks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BankID,BankName,AccountNumber,Currency,BankAddress")] Bank Bank)
        {
             
            if (ModelState.IsValid)
            {
                Bank.InstanceID = instanceId;
                db.SaveBank(Bank);
                return RedirectToAction("Index");
            }

            return View(Bank);
        }

        // GET: BasicSetup/Banks/Edit/5
        public ActionResult Edit(long id=0)
        {
             

            Bank Bank = db.Single(instanceId, id);
            if (Bank == null)
            {
                return RedirectToAction("Index");
            }
            return View(Bank);
        }

        // POST: BasicSetup/Banks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BankID,BankName,AccountNumber,Currency,BankAddress")] Bank Bank)
        {
            if (ModelState.IsValid)
            {
                 
                db.SaveBank(Bank);
                return RedirectToAction("Index");
            }
            return View(Bank);
        }

        // GET: BasicSetup/Banks/Delete/5
        public ActionResult Delete(long id=0)
        {

             
            Bank Bank = db.Single(instanceId, id);
            if (Bank == null)
            {
                return RedirectToAction("Index");
            }
            if (db.IsExists(Bank.BankID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Bank);
        }

        // POST: BasicSetup/Banks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
             
            Bank Bank = db.Single(instanceId, id);
            if (Bank != null)
            {
                db.DeleteBank(Bank.BankID);
            }            
            return RedirectToAction("Index");
        }

    }
}
