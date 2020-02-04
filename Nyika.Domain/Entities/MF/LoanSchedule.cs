using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.MF
{
    public class LoanSchedule
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long LoanScheduleID { get; set; }

        [Display(Name = "Loan")]
        public long LoanID { get; set; }
        public virtual Loan Loan { get; set; }

        [Display(Name = "Installment No")]
        public int InstallmentNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Principal Amount")]
        public double sPrincipalAmount { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Interest Amount")]
        public double sInterestAmount { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Total Amount")]
        public double sTotalAmount { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}