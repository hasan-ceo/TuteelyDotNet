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
using Nyika.WebUI.Models;

namespace Nyika.WebUI.Areas.BasicSetup.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class LeavesController : Controller
    {
        private ILeaveRepo db;
        private string instanceId;

        public LeavesController(ILeaveRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: BasicSetup/Leaves
        public ActionResult Index()
        {
            
            return View(db.Leave(instanceId).ToList());
        }

        

        // GET: BasicSetup/Leaves/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/Leaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeaveID,LeaveName,ShortCode,YearlyLeave")] Leave Leave)
        {
             
            if (ModelState.IsValid)
            {
                Leave.InstanceID = instanceId;
                db.SaveLeave(Leave);
                return RedirectToAction("Index");
            }

            return View(Leave);
        }

        // GET: BasicSetup/Leaves/Edit/5
        public ActionResult Edit(long id=0)
        {
            
            Leave Leave = db.Single(instanceId, id);
            if (Leave == null)
            {
                return RedirectToAction("Index");
            }
            return View(Leave);
        }

        // POST: BasicSetup/Leaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeaveID,LeaveName,ShortCode,YearlyLeave")] Leave Leave)
        {
            if (ModelState.IsValid)
            {
                 
                db.SaveLeave(Leave);
                return RedirectToAction("Index");
            }
            return View(Leave);
        }

        // GET: BasicSetup/Leaves/Delete/5
        public ActionResult Delete(long id=0)
        {

             
            Leave Leave = db.Single(instanceId, id);
            if (Leave == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Leave.LeaveID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Leave);
        }

        // POST: BasicSetup/Leaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Leave Leave = db.Single(instanceId, id);
            if (Leave != null)
            {
                db.DeleteLeave(Leave.LeaveID);
            }            
            return RedirectToAction("Index");
        }

    }
}
