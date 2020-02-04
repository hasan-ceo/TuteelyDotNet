using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRMSMvc.Domain.Entities
{
    [Table("SalaryGrade")]
    public class SalaryGrade
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int SalaryGradeID { get; set; }

        [MaxLength(255)]
        [Display(Name = "SalaryGrade Name")]
        public string SalaryGradeName { get; set; }

        //[Display(Name = "Country")]
        //public int CountryID { get; set; }
        //public virtual Country Country { get; set; }

    }
}
