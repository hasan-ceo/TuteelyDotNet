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

    public class EmployeeAttendance
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long EmployeeAttendanceID { get; set; }

        [Display(Name = "Employee")]
        public long EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [Display(Name = "Status")]
        public long AttenStatusID { get; set; }
        public virtual AttenStatus AttenStatus { get; set; }

        [Required]
        [MaxLength(256)]
        [DefaultValue("")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Particulars")]
        public string Particulars { get; set; }

        [Required(ErrorMessage = "Please Enter Working Date")]
        [Display(Name = "Working Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime WorkDate { get; set; }

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

        public bool ManualEntry { get; set; }


        [MaxLength(50)]
        [Display(Name = "Entry By")]
        public string EntryBy { get; set; }


        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}
