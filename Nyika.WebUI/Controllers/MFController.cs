using CaptchaMvc.HtmlHelpers;
using Nyika.Domain.Abstract.AVL;
using Nyika.Domain.Entities.AVL;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Controllers
{
    public class MFController : Controller
    {

   

        public ActionResult Index()
        {
            return View();
        }

        

    }


}