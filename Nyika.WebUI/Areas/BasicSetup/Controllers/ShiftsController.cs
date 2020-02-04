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
    public class ShiftsController : Controller
    {
        private IShiftRepo db;
        private string instanceId;

        public ShiftsController(IShiftRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: BasicSetup/Shifts
        public ActionResult Index()
        {
            
            return View(db.Shift(instanceId).ToList());
        }

        

        // GET: BasicSetup/Shifts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/Shifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShiftID,ShiftName,ShiftIn,ShiftOut,ShiftAbsent,ShiftLate,ShiftEarly,ShiftLunchFrom,ShiftLunchTill,ShiftLastPunch,DefaultShift")] Shift Shift)
        {
             
            if (ModelState.IsValid)
            {
                Shift.InstanceID = instanceId;
                db.SaveShift(Shift);
                return RedirectToAction("Index");
            }

            return View(Shift);
        }

        // GET: BasicSetup/Shifts/Edit/5
        public ActionResult Edit(long id=0)
        {
            
            Shift Shift = db.Single(instanceId , id);
            if (Shift == null)
            {
                return RedirectToAction("Index");
            }
            return View(Shift);
        }

        // POST: BasicSetup/Shifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShiftID,ShiftName,ShiftIn,ShiftOut,ShiftAbsent,ShiftLate,ShiftEarly,ShiftLunchFrom,ShiftLunchTill,ShiftLastPunch,DefaultShift")] Shift Shift)
        {
            if (ModelState.IsValid)
            {
                 
                db.SaveShift(Shift);
                return RedirectToAction("Index");
            }
            return View(Shift);
        }

        // GET: BasicSetup/Shifts/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            Shift Shift = db.Single(instanceId, id);
            if (Shift == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Shift.ShiftID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }


            return View(Shift);
        }

        // POST: BasicSetup/Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Shift Shift = db.Single(instanceId, id);
            if (Shift != null)
            {
                db.DeleteShift(Shift.ShiftID);
            }            
            return RedirectToAction("Index");
        }

    }
}
