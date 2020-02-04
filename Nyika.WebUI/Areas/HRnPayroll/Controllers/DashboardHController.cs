using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Abstract.HR;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.HRnPayroll.Controllers
{
    [Authorize(Roles = "HR Executive,Manager,Super Admin")]
    public class DashboardHController : Controller
    {
        private IBusinessDayRepo bd;
        private string instanceId;

        public DashboardHController(IBusinessDayRepo BD)
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