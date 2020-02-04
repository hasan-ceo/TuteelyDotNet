using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;
using Nyika.Domain.Entities.Setup;

namespace Nyika.Domain.Entities.HR
{
    public class EmployeeAllDed
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long EmployeeAllDedID { get; set; }

        [Display(Name = "Employee")]
        public long EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [Display(Name = "Allowance / Deduction")]
        public long AllDedID { get; set; }
        public virtual AllDed AllDed { get; set; }

        [Required]
        [Display(Name = "Effective Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveDate { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Amount")]
        public double Amount { get; set; }

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
