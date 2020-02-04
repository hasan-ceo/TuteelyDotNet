using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nyika.Domain.Entities.Setup
{
    public class ShopPage
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public long ShopPageID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [AllowHtmlAttribute]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [AllowHtmlAttribute]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description Local")]
        public string DescriptionLocal { get; set; }

    }
}
