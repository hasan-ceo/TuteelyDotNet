using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;
using Nyika.Domain.Entities.Setup;

namespace Nyika.Domain.Entities.HR
{
    public class EmployeeTransfer
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long EmployeeTransferID { get; set; }

        [Display(Name = "Employee")]
        public long EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [Required]
        [Display(Name = "Effective Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveDate { get; set; }

        [Required]
        [Display(Name = "Department(*)")]
        public long DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        [Display(Name = "Section(*)")]
        public long SectionID { get; set; }
        public virtual Section Section { get; set; }

        [Required]
        [Display(Name = "Designation(*)")]
        public long DesignationID { get; set; }
        public virtual Designation Designation { get; set; }

        [Required]
        [Display(Name = "Staff Type(*)")]
        public long EmploymentTypeID { get; set; }
        public virtual EmploymentType EmploymentType { get; set; }

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
