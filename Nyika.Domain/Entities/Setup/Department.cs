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
    public class Department
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long DepartmentID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

        //[Display(Name = "Country")]
        //public int CountryID { get; set; }
        //public virtual Country Country { get; set; }

    }
}
