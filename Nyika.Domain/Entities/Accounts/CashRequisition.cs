using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.Accounts
{
    public class CashRequisition
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long CashRequisitionID { get; set; }

        [Required]
        [MaxLength(512)]
        [DefaultValue("")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Particulars")]
        public string Particulars { get; set; }

        [DefaultValue(0)]
        public double Amount { get; set; }

        public bool Approved { get; set; }

        [MaxLength(50)]
        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }

        [Display(Name = "Approved Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ApprovedDate { get; set; }

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
