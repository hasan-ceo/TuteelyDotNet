using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Nyika.WebUI.Models
{
    public class InstanceVM
    {
 
        public string InstanceID
        {
            get { return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).InstanceID; }
        }

        public string EntryBy
        {
            get { return HttpContext.Current.User.Identity.GetUserId(); }
        }

        public string ExpireDate
        {
            get { return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).ExpairDate.ToString("dd/MMM/yyyy"); }
        }


    }
}