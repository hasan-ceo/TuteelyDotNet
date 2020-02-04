using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Nyika.WebUI.Models
{
    public class TopicVM
    {
        public long TopicID { get; set; }
        public string TopicName { get; set; }
    }
}