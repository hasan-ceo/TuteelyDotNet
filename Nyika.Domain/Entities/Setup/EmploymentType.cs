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
    public class EmploymentType
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long EmploymentTypeID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Employment Type")]
        public string EmploymentTypeName { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

    }
}
