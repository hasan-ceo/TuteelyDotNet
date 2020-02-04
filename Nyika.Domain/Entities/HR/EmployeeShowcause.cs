using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.HR
{
    public class EmployeeShowcause
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long EmployeeShowcauseID { get; set; }

        [Display(Name = "Employee")]
        public long EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [Required]
        [MaxLength(110)]
        [DefaultValue("")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }


        [Required]
        [MaxLength(512)]
        [DefaultValue("")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Details")]
        public string Details { get; set; }

        [Required]
        [MaxLength(512)]
        [DefaultValue("")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Action Taken")]
        public string ActionTaken { get; set; }

        [Required]
        [Display(Name = "Work Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime WorkDate { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        [Display(Name = "Entry By")]
        public string EntryBy { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

    }
}
