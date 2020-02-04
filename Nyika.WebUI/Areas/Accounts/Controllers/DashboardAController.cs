using Microsoft.AspNet.Identity;
using Nyika.Domain.Abstract.Accounts;
using Nyika.WebUI.Areas.Accounts.Models;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.Accounts.Controllers
{
    [Authorize(Roles = "Accountant,Manager,Super Admin")]
    public class DashboardAController : Controller
    {
        private ICashSummaryRepo db;
        private IBusinessDayRepo bd;
        private string instanceId;

        public DashboardAController(ICashSummaryRepo DB, IBusinessDayRepo BD)
        {
            this.db = DB;
            this.bd = BD;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            DashboardVM dashboardVM = new DashboardVM();
            dashboardVM.CashSummary=db.CashSummary(instanceId).ToList();
            var dt = bd.BusinessDay(instanceId).Where(b => b.DayClose == false).FirstOrDefault();
            if (dt != null)
            {
                dashboardVM.WorkDate = dt.WorkDate.ToString("dd/MMM/yyyy");
                dashboardVM.DayClose = false;
                return View(dashboardVM);
            }
            else
            {
                return RedirectToAction("DayOpen", "DashboardA", new { Area = "Accounts" });
            }
        }

        public ActionResult DayOpen()
        {
            DashboardVM dashboardVM = new DashboardVM();
            dashboardVM.CashSummary = db.CashSummary(instanceId).ToList();
            var dt = bd.BusinessDay(instanceId).Where(b => b.DayClose == false).FirstOrDefault();
            if (dt != null)
            {
                dashboardVM.WorkDate = dt.WorkDate.ToString("dd/MMM/yyyy");
                dashboardVM.DayClose = false;
            }
            else
            {
                dashboardVM.WorkDate = bd.BusinessDay(instanceId).Where(b => b.DayClose == true).Max(b => b.WorkDate).ToString("dd/MMM/yyyy");
                dashboardVM.DayClose = true;
            }

            return View(dashboardVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CashUpdate()
        {
            

            db.CashUpdate(instanceId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BusinessDayClose()
        {
            
           
            bd.DayClose(instanceId);
           
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BusinessDayOpen()
        {


            bd.DayOpen(instanceId, User.Identity.Name);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MonthClose()
        {
            

            bd.MonthClose(instanceId);

            return RedirectToAction("Index");
        }
    }
}