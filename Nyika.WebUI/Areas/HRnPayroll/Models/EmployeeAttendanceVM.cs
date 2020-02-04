using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Nyika.WebUI.Areas.Accounts.Models;
using System.ComponentModel;

namespace Nyika.WebUI.Areas.HRnPayroll.Models
{
    public class EmployeeAttendanceVM
    {
       
        [Display(Name = "Employee")]
        public long EmployeeID { get; set; }

        [Display(Name = "Status")]
        public int AttenStatusID { get; set; }

        [Required]
        [MaxLength(256)]
        [DefaultValue("")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Particulars")]
        public string Particulars { get; set; }

        [Required(ErrorMessage = "Please Enter Working Date")]
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "Please Enter Working Date")]
        [Display(Name = "Till Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TillDate { get; set; }

        [Required(ErrorMessage = "Please enter In Time")]
        [Display(Name = "In Time (ex: 08:15)")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime InTime { get; set; }

        [Required(ErrorMessage = "Please enter Out Time")]
        [Display(Name = "Out Time (ex: 17:15)")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime OutTime { get; set; }

    }
}