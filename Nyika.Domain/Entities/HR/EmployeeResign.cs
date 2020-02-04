using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;
using Nyika.Domain.Entities.Setup;

namespace Nyika.Domain.Entities.HR
{
    public class EmployeeResign
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long EmployeeResignID { get; set; }

        [Display(Name = "Employee")]
        public long EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [Display(Name = "Reason")]
        public long ResignReasonID { get; set; }
        public virtual ResignReason ResignReason { get; set; }

        //[Required]
        //[Display(Name = "Resign Date(*)")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime ResignDate { get; set; }

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
