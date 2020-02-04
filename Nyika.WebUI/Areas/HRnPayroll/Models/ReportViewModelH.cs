using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Nyika.WebUI.Areas.Accounts.Models;

namespace Nyika.WebUI.Areas.HRnPayroll.Models
{
    public class ReportViewModelH 
    {
        //[Display(Name = "Account Head")]
        //public int AccountSubHeadID { get; set; }

        //[Display(Name = "Bank Head")]
        //public int BankID { get; set; }

        [Required(ErrorMessage = "Please enter From Date")]
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "Please enter Till Date")]
        [Display(Name = "Till Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TillDate { get; set; }

        [Display(Name = "Employee")]
        public long EmployeeID { get; set; }


    }
}