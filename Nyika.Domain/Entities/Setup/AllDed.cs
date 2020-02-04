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
    public enum AllDedType
    {
        Allowance =1 , Deduction = 2 
    }

    public class AllDed
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public long AllDedID { get; set; }

        [MaxLength(255)]
        [Display(Name = "Name")]
        public string AllDedName { get; set; }

        [Display(Name = "Type")]
        public AllDedType ADType { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

    }
}
