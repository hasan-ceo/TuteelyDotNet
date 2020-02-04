using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nyika.Domain.Entities.Stock
{
    public class Category
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long CategoryID { get; set; }

        [MaxLength(255)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Category Name Local")]
        public string CategoryNameLocal { get; set; }

        [MaxLength(255)]
        [Display(Name = "Slug")]
        public string UrlLink { get; set; }

        //[DefaultValue("")]
        //[MaxLength(255)]
        //[DataType(DataType.ImageUrl)]
        //public string ImageUrl { get; set; }

        [DefaultValue("")]
        [MaxLength(255)]
        [DataType(DataType.ImageUrl)]
        public string HeaderUrl { get; set; }

        [DefaultValue("")]
        [MaxLength(255)]
        [DataType(DataType.ImageUrl)]
        public string LogoUrl { get; set; }
    }
}
