using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Nyika.WebUI.Models
{
    public class SubscriptionVM
    {
        public string PayerName { get; set; }
        public string SubscriptionType { get; set; }
        public string Amount { get; set; }
    }
}