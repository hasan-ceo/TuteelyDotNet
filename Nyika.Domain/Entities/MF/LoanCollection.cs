using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.MF
{ 

    public class LoanCollection
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long LoanCollectionID { get; set; }

        [Display(Name = "Loan")]
        public long LoanID { get; set; }
        public virtual Loan Loan { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Collection No")]
        public int CollectionNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Collection Date")]
        public DateTime CollectionDate { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Target Amount")]
        public double TargetAmount { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Realized Principal")]
        public double RealizedPrincipal { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Realized Interest")]
        public double RealizedInterest { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Realized Amount")]
        public double RealizedAmount { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Loan Due")]
        public double LoanDue { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Overdue Amount")]
        public double OverdueAmount { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Advance Amount")]
        public double AdvanceAmount { get; set; }

        [MaxLength(150)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Collected")]
        public bool Collected { get; set; }
    }



}
