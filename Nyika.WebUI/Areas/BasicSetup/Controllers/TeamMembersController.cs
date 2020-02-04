using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Nyika.WebUI.Areas.Accounts.Models;
using Nyika.WebUI.Areas.BasicSetup.Models;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.BasicSetup.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class TeamMembersController : Controller
    {
        // GET: Dashboard


        public ActionResult Index()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var applicationDbContext = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

            var users = (from u in applicationDbContext.Users
                         from ur in u.Roles
                         join r in applicationDbContext.Roles on ur.RoleId equals r.Id
                         where u.Id != user.Id && u.InstanceID == user.InstanceID
                         select new { u.Id, u.UserName, r.Name, u.status }).ToList();

            var urs = from u in users
                      select new UserRoleViewModel
                      {
                          Id = u.Id,
                          UserName = u.UserName,
                          RoleName = u.Name,
                          Status =u.status
                      };

            return View(urs);
        }

        [HttpPost]
        public ActionResult userStatus(string Id, string BtnAll)
        {
            var applicationDbContext = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            applicationDbContext.Database.ExecuteSqlCommand("exec pSetupUserStatus @Id={0}, @BtnAll={1}", Id, BtnAll);
            return RedirectToAction("Index");
        }
    }

}