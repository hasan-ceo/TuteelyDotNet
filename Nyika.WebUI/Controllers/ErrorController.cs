using Nyika.Domain.Abstract.Setup;
using Nyika.Domain.Concrete;
using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        private EFDbContext db = new EFDbContext();

        public ActionResult Index(string error)
        {
            //ViewBag.Description = Resources.ErrorMessage.GeneralError;
            if (!string.IsNullOrEmpty(error))
            {
                ErrorLog errorLog=new ErrorLog();
                errorLog.Message = error;
                errorLog.ErrorDateTime = DateTime.Now;
                db.ErrorLog.Add(errorLog);
                db.SaveChanges();
                ViewBag.DetailError = error;
            }
            else
            {
                ViewBag.DetailError = string.Empty;
            }

            return View("ErrorIndex");
        }
    }
}