using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Nyika.WebUI.Models
{
    public class ItemListVM
    {
        public string CategoryName { get; set; }
        public string CategoryUrl { get; set; }
        public string CategorySubName { get; set; }
        public string HeaderUrl { get; set; }
        public List<ItemVM> itemVM { get; set; }
        public List<CategorySubVM> categorySubVM { get; set; }
    }
}