using Nyika.Domain.Abstract.Accounts;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.BasicSetup.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class DashboardSController : Controller
    {
        private IBusinessDayRepo bd;
        private string instanceId;

        public DashboardSController(IBusinessDayRepo BD)
        {
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