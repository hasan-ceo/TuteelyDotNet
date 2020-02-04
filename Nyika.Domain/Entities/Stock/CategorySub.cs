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
    public class CategorySub
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long CategorySubID { get; set; }

        [MaxLength(255)]
        [Display(Name = "Sub Category Name")]
        public string CategorySubName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Sub Category Name Local")]
        public string CategorySubNameLocal { get; set; }

        [MaxLength(255)]
        [Display(Name = "Slug")]
        public string UrlLink { get; set; }

        [Display(Name = "Category")]
        public long CategoryID { get; set; }
        public virtual Category Category { get; set; }

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
