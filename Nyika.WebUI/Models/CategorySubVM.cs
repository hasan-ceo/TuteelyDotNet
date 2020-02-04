using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Nyika.WebUI.Models
{
    public class CategorySubVM
    {
        public string UrlLink { get; set; }
        public string CategoryName { get; set; }
        public string LogoUrl { get; set; }
    }
}