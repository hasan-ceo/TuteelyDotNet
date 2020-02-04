using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nyika.Domain.Entities.Stock
{

    public class Brand
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long BrandID { get; set; }

        [MaxLength(255)]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Brand Name Local")]
        public string BrandNameLocal { get; set; }
    }
}
