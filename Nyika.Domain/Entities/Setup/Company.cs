using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

namespace Nyika.Domain.Entities.Setup
{
    public enum Weekdays
    {
        Sunday = 1, Monday=2, Tuesday=3, Wednesday=4, Thursday=5, Friday=6, Saturday=7 // Sunday=1, Monday=2, Tuesday=3, Wednesday=4, Thursday=5, Friday=6, Saturday=7
    }

    public class Company
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public long CompanyID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Email address")]
        [MaxLength(50)]
        [DefaultValue("")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Contact Number")]
        [MaxLength(20)]
        [Required(ErrorMessage = "Please enter Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Web Address")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter Web Address")]
        public string WebAddress { get; set; }

        [MaxLength(255)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [MaxLength(50)]
        [Display(Name = "TIN Number")]
        public string TIN { get; set; }

        [MaxLength(50)]
        [Display(Name = "VAT Number")]
        public string VAT { get; set; }

        [MaxLength(3)]
        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [MaxLength(20)]
        [Display(Name = "Default Stamp")]
        public string Stamp { get; set; }

        [MaxLength(255)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Default Payment Terms")]
        public string PaymentTerms { get; set; }

        [MaxLength(255)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Default Client Notes")]
        public string ClientNotes { get; set; }

        [Display(Name = "1st Week Off Day")]
        public Weekdays WeekOff1 { get; set; }

        [Display(Name = "2nd Week Off Day")]
        public Weekdays WeekOff2 { get; set; }

        //[Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [DefaultValue(0)]
        [Display(Name = "SDL %")]
        public double SDL { get; set; }

        [DefaultValue(0)]
        [Display(Name = "NSSF/PPF %")]
        public double NSSFPPF { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Higher Study Loan %")]
        public double HigherStudyLoan { get; set; }

        [DefaultValue(0)]
        [Display(Name = "National Health Insurance Fund (NHIF) %")]
        public double NHIF { get; set; }

        [MaxLength(20)]
        [DefaultValue("")]
        [Display(Name = "NSSF Employer Number")]
        public string NSSFEmployerNumber { get; set; }

        [MaxLength(20)]
        [DefaultValue("")]
        [Display(Name = "PPF Employer Number")]
        public string PPFEmployerNumber { get; set; }

        [MaxLength(20)]
        [DefaultValue("")]
        [Display(Name = "WCF Number")]
        public string WCFNumber { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        [Display(Name = "NHIF Number")]
        public string NHIFNumber { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

    }
}
