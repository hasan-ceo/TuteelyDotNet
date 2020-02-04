using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Nyika.Domain.Entities.Setup
{   
    public class Shift
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long ShiftID { get; set; }

        [Display(Name = "Shift Name")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter Shift Name")]
        public string ShiftName { get; set; }

        [Required(ErrorMessage = "Please enter Shift In")]
        [Display(Name = "Shift In (ex: 08:15 am)")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime ShiftIn { get; set; }

        [Required(ErrorMessage = "Please enter Shift Out")]
        [Display(Name = "Shift Out (ex: 05:15 pm)")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime ShiftOut { get; set; }

        [Required(ErrorMessage = "Please enter Shift Absent")]
        [Display(Name = "Shift Absent (ex: 09:15 am)")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime ShiftAbsent { get; set; }

        [Required(ErrorMessage = "Please enter Shift Late")]
        [Display(Name = "Shift Late (ex: 08:45 am)")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime ShiftLate { get; set; }

        [Required(ErrorMessage = "Please enter Shift Early")]
        [Display(Name = "Shift Early (ex: 08:00 am)")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime ShiftEarly { get; set; }

        [Required(ErrorMessage = "Please enter Lunch From")]
        [Display(Name = "Shift Lunch From (ex: 01:15 pm)")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime ShiftLunchFrom { get; set; }

        [Required(ErrorMessage = "Please enter Lunch Till")]
        [Display(Name = "Shift Lunch Till (ex: 01:45 pm)")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime ShiftLunchTill { get; set; }

        [Required(ErrorMessage = "Please enter Last Punch")]
        [Display(Name = "Shift Last Punch (ex: 08:15 pm)")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime ShiftLastPunch { get; set; }

        [Display(Name = "Default Shift")]
        [DefaultValue(false)]
        public bool DefaultShift { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}
