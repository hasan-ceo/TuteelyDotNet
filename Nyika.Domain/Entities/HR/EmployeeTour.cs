using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;
using Nyika.Domain.Entities.Setup;

namespace Nyika.Domain.Entities.HR
{
    public class EmployeeTour
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long EmployeeTourID { get; set; }

        [Display(Name = "Employee")]
        public long EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [Display(Name = "Tour")]
        public int TourType { get; set; }
   
        [Required]
        [Display(Name = "From Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [Required]
        [Display(Name = "Till Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TillDate { get; set; }

        [Required]
        [Display(Name = "Application Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ApplicationDate { get; set; }

        [Required]
        [MaxLength(256)]
        [DefaultValue("")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Particulars")]
        public string Particulars { get; set; }

        [Required]
        [Display(Name = "Work Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime WorkDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Entry By")]
        public string EntryBy { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

    }
}
