using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;
using Nyika.Domain.Entities.Setup;

namespace Nyika.Domain.Entities.MF
{
    public class Groups
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long GroupsID { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Group Name")]
        public string GroupsName { get; set; }

        [Required]
        [Display(Name = "Project")]
        public long ProjectID { get; set; }
        public virtual Project Project { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Founder Collection Officer")]
        public long EmployeeIdFC { get; set; }

        [Required]
        [Display(Name = "Current Collection Officer")]
        public long EmployeeIdCC { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [Required]
        [Display(Name = "Collection Day")]
        public Weekdays ColDay { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Collection Start Date")]
        public DateTime ColStartDate { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Frequency")]
        public string Frequency { get; set; }


        [DefaultValue(false)]
        [Display(Name = "Inactive")]
        public bool Inactive { get; set; }

        [MaxLength(50)]
        [Display(Name = "Entry By")]
        public string EntryBy { get; set; }

        [Required(ErrorMessage = "Please Enter Working Date")]
        [Display(Name = "Working Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime WorkDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}