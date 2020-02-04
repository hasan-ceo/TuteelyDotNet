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
    public class ResignReasonsController : Controller
    {
        private IResignReasonRepo db;
        private string instanceId;

        public ResignReasonsController(IResignReasonRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: BasicSetup/ResignReasons
        public ActionResult Index()
        {
            
            return View(db.ResignReason(instanceId).ToList());
        }

        

        // GET: BasicSetup/ResignReasons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/ResignReasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResignReasonID,ResignReasonName")] ResignReason resignReason)
        {
            
            if (ModelState.IsValid)
            {
                resignReason.InstanceID = instanceId;
                db.SaveResignReason(resignReason);
                return RedirectToAction("Index");
            }

            return View(resignReason);
        }

        // GET: BasicSetup/ResignReasons/Edit/5
        public ActionResult Edit(long id=0)
        {
            

            ResignReason resignReason = db.Single(instanceId, id);
            if (resignReason == null)
            {
                return RedirectToAction("Index");
            }
            return View(resignReason);
        }

        // POST: BasicSetup/ResignReasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResignReasonID,ResignReasonName")] ResignReason resignReason)
        {
            if (ModelState.IsValid)
            {
                
                db.SaveResignReason(resignReason);
                return RedirectToAction("Index");
            }
            return View(resignReason);
        }

        // GET: BasicSetup/ResignReasons/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            ResignReason resignReason = db.Single(instanceId , id);
            if (resignReason == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(resignReason.ResignReasonID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(resignReason);
        }

        // POST: BasicSetup/ResignReasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            ResignReason resignReason = db.Single(instanceId, id);
            if (resignReason != null)
            {
                db.DeleteResignReason(resignReason.ResignReasonID);
            }
            return RedirectToAction("Index");
        }

    }
}
