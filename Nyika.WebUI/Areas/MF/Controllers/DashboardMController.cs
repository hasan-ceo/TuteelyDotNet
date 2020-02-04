using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Abstract.HR;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.MF.Controllers
{
    [Authorize(Roles = "Accountant,Manager,Super Admin")]
    public class DashboardMController : Controller
    {
        private IBusinessDayRepo bd;
        private IEmployeeAttendanceRepo db;
        private string instanceId;

        public DashboardMController(IBusinessDayRepo BD, IEmployeeAttendanceRepo DB)
        {
            this.db = DB;
            this.bd = BD;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: Dashboard
        public ActionResult Index()
        {

            DashboardVM dashboardVM = new DashboardVM();

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
    }
}