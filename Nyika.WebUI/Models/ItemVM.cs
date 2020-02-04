using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Nyika.WebUI.Models
{
    public class ItemVM
    {
        public string UrlLink { get; set; }
        public long ItemID { get; set; }
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public string Thumbnail { get; set; }
        public string FrontUrl { get; set; }
        public string BackUrl { get; set; }
        public string SideUrl { get; set; }
        public Decimal OldPrice { get; set; }
        public Decimal NewPrice { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }

        public string CategoryName { get; set; }
        public string CategoryUrl { get; set; }
        public string CategorySubName { get; set; }
        public string CategorySubUrl { get; set; }
    }
}