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
    public class PartiesController : Controller
    {
        private IPartyRepo db;
        private string instanceId;

        public PartiesController(IPartyRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: BasicSetup/Partys
        public ActionResult Index()
        {
             
            return View(db.Party(instanceId).ToList());
        }

        

        // GET: BasicSetup/Partys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/Partys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartyID,PartyName,Email,ContactNumber,Address,ZIPCode")] Party Party)
        {
             
            if (ModelState.IsValid)
            {
                Party.InstanceID = instanceId;
                db.SaveParty(Party);
                return RedirectToAction("Index");
            }

            return View(Party);
        }

        // GET: BasicSetup/Partys/Edit/5
        public ActionResult Edit(long id=0)
        {
            
            Party Party = db.Single(instanceId, id);
            if (Party == null)
            {
                return RedirectToAction("Index");
            }
            return View(Party);
        }

        // POST: BasicSetup/Partys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartyID,PartyName,Email,ContactNumber,Address,ZIPCode")] Party Party)
        {
            if (ModelState.IsValid)
            {
                 
                db.SaveParty(Party);
                return RedirectToAction("Index");
            }
            return View(Party);
        }

        // GET: BasicSetup/Partys/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            Party Party = db.Single(instanceId, id);
            if (Party == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Party.PartyID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Party);
        }

        // POST: BasicSetup/Partys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Party Party = db.Single(instanceId, id);
            if (Party != null)
            {
                db.DeleteParty(Party.PartyID);
            }            
            return RedirectToAction("Index");
        }

    }
}
