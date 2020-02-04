using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nyika.Domain.Concrete;
using Nyika.Domain.Entities.Accounts;
using Nyika.Domain.Abstract.Accounts;
using Nyika.WebUI.Models;

namespace Nyika.WebUI.Areas.User.Controllers
{
    [Authorize(Roles = "User,Super Admin,Manager")]
    public class CashRequisitionsController : Controller
    {
        private ICashRequisitionRepo db;
        private IBusinessDayRepo bddb;
        private string instanceId;

        public CashRequisitionsController(ICashRequisitionRepo DB, IBusinessDayRepo bdDB)
        {
            this.db = DB;
            this.bddb = bdDB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: User/CashRequisitions
        public ActionResult Index()
        {
            
            return View(db.CashRequisitionUser(instanceId,User.Identity.Name));
        }

        

        // GET: User/CashRequisitions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/CashRequisitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CashRequisitionID,Particulars,Amount")] CashRequisition cashRequisition)
        {
            if (ModelState.IsValid)
            {
                
                cashRequisition.WorkDate = bddb.WorkDate(instanceId);
                cashRequisition.EntryBy = User.Identity.Name;
                cashRequisition.InstanceID = instanceId;
                cashRequisition.ApprovedBy = "";
                cashRequisition.ApprovedDate = cashRequisition.WorkDate;
                db.SaveCashRequisition(cashRequisition);
                return RedirectToAction("Index");
            }

            return View(cashRequisition);
        }

        // GET: User/CashRequisitions/Edit/5
        public ActionResult Edit(long id=0)
        {
            
            CashRequisition cashRequisition = db.Single(instanceId, id);
            if (cashRequisition == null)
            {
                return RedirectToAction("Index");
            }
            return View(cashRequisition);
        }

        // POST: User/CashRequisitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CashRequisitionID,Particulars,Amount")] CashRequisition cashRequisition)
        {
            if (ModelState.IsValid)
            {
                db.SaveCashRequisition(cashRequisition);
                return RedirectToAction("Index");
            }
            return View(cashRequisition);
        }

        // GET: User/CashRequisitions/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            CashRequisition cashRequisition = db.Single(instanceId, id);
            if (cashRequisition == null)
            {
                return RedirectToAction("Index");
            }
            return View(cashRequisition);
        }

        // POST: User/CashRequisitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            db.DeleteCashRequisition(id);
            return RedirectToAction("Index");
        }

       
    }
}
