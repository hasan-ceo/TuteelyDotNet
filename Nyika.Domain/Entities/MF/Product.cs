using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.MF
{
    public class Product
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public long ProductID { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Project")]
        public long ProjectID { get; set; }
        public virtual Project Project { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Interest Rate")]
        public double InterestRate { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Interest Rate Type")]
        public string InterestRateType { get; set; }

        [Required]
        [DefaultValue(0)]
        [DisplayFormat(DataFormatString = "{0:0.0000000000}")]
        [Display(Name = "Int Factor")]
        public double IntFactor { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Duration in Month")]
        public int Duration { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "No Of Installment in Duration")]
        public int NoOfInstallment { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Inactive")]
        public bool Inactive { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}