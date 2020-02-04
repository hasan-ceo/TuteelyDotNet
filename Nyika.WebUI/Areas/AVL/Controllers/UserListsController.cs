using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Nyika.Domain.Concrete;
using Nyika.WebUI.Areas.Accounts.Models;
using Nyika.WebUI.Areas.AVL.Models;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.AVL.Controllers
{
    [Authorize(Roles = "Surzo")]
    public class UserListsController : Controller
    {
        // GET: Dashboard
        private EFDbContext db = new EFDbContext();

        public ActionResult Index()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var applicationDbContext = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

            var urs = applicationDbContext.Database.SqlQuery<UserRoleViewModel>("select * from vwSuperAdmin where RoleName='Super Admin'");

            return View(urs);
        }


        // GET: BasicSetup/Pages/Delete/5
        public ActionResult Delete(string id)
        {
            var applicationDbContext = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

            var urs = applicationDbContext.Database.SqlQuery<UserRoleViewModel>("select * from vwSuperAdmin where RoleName='Super Admin' and id='"+id+"'");
            if (urs == null)
            {
                return RedirectToAction("Index");
            }
            return View(urs.FirstOrDefault());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuperUserDelete(string Id)
        {
            var applicationDbContext = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            applicationDbContext.Database.ExecuteSqlCommand("exec pAVLSuperUserDelete @Id={0}", Id);
            return RedirectToAction("Index");
        }
    }

}