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
using Nyika.WebUI.Helper;
using Nyika.WebUI.Areas.BasicSetup.Models;
using Nyika.WebUI.Models;

namespace Nyika.WebUI.Areas.BasicSetup.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class HolidaysController : Controller
    {
        private IHolidayRepo db;
        private string instanceId;

        public HolidaysController(IHolidayRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: BasicSetup/Holidays
        public ActionResult Index()
        {
            
            return View(db.Holiday(instanceId).ToList());
        }

       

        // GET: BasicSetup/Holidays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/Holidays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HolidayID,HolidayName,FromDate,TillDate")] Holiday holiday)
        {            
               
                if (ModelState.IsValid)
                {
                    
                    holiday.InstanceID = instanceId;
                    db.SaveHoliday(holiday);
                    return RedirectToAction("Index");
                }
            return View(holiday);
        }

        // GET: BasicSetup/Holidays/Edit/5
        public ActionResult Edit(long id=0)
        {
             
            Holiday Holiday = db.Single(instanceId, id);
            if (Holiday == null)
            {
                return RedirectToAction("Index");
            }
            
            return View(Holiday);
        }

        // POST: BasicSetup/Holidays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HolidayID,HolidayName,FromDate,TillDate")] Holiday holiday)
        {
            if (ModelState.IsValid)
            {
                db.SaveHoliday(holiday);
                return RedirectToAction("Index");
            }
            return View(holiday);
        }

        // GET: BasicSetup/Holidays/Delete/5
        public ActionResult Delete(long id=0)
        {
             
            Holiday Holiday = db.Single(instanceId, id);
            if (Holiday == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Holiday.HolidayID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Holiday);
        }

        // POST: BasicSetup/Holidays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Holiday Holiday = db.Single(instanceId, id);
            if (Holiday != null)
            {
                db.DeleteHoliday(Holiday.HolidayID);
            }
            return RedirectToAction("Index");
        }

    }
}
