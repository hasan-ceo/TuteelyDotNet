using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Nyika.WebUI.Models
{
    public class UserExpireVM
    {
        public string id { get; set; }
        public string email { get; set; }
        public int bit { get; set; }
    }
}