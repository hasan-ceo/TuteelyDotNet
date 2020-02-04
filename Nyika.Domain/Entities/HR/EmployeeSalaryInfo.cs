using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nyika.Domain.Entities.HR
{
    //public enum AttenStatus
    //{
    //    Absent=0, //0
    //    Holiday=1, //1
    //    Late=2, //2
    //    Leave=3, //3
    //    Off_Day=4, //4
    //    Present=5, //5
    //    Weekly_off=6 //6
    //}

    public class EmployeeSalaryInfo
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long EmployeeSalaryInfoID { get; set; }

        [Display(Name = "Employee")]
        public long EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [DefaultValue(0)]
        //[RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        [Display(Name = "Basic Salary")]
        public double BasicSalary { get; set; }

        [DefaultValue(0)]
        //[RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        [Display(Name = "Other Benefits")]
        public double OtherBenefits { get; set; }

        [DefaultValue(0)]
        //[RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        [Display(Name = "Gross Salary")]
        public double Grosssalary { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Lunch Allowance")]
        //[RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        public double LunchAllowance { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Professional Allowance")]
        public double Professionalallowance { get; set; }

        

        [MaxLength(50)]
        [Display(Name = "Entry By")]
        public string EntryBy { get; set; }


        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}
