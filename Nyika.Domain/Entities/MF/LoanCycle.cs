using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.MF
{
    public class LoanCycle
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long LoanCycleID { get; set; }

        [Required]
        [Display(Name = "Project Id")]
        public long ProjectID { get; set; }
        public virtual Project Project { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Loan Cycle")]
        public int LoanCycleNo { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Min Limit")]
        public double MinLimit { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Max Limit")]
        public double MaxLimit { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}