using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Abstract.HR;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.User.Controllers
{
    [Authorize(Roles = "User,Super Admin,Manager")]
    public class DashboardUController : Controller
    {
        private IBusinessDayRepo bd;
        private string instanceId;

        public DashboardUController(IBusinessDayRepo BD)
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult dashboard(string BtnAll)
        //{
        //    
        //    if (BtnAll == "Attendance Process")
        //    {
        //        db.ProcessAttendance(instanceId);
        //        TempData["ProcessComplete"] = "Process Complete";
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}