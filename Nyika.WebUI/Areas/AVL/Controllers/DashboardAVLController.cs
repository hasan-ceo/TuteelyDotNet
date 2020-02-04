using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.AVL.Controllers
{
    [Authorize(Roles = "Surzo")]
    public class DashboardAVLController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}