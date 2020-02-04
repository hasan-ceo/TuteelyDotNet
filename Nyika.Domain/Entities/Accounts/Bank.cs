using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.Accounts
{
    public class Bank
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long BankID { get; set; }

        [Display(Name = "Bank Name")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter a Name")]
        public string BankName { get; set; }

        [Display(Name = "Account Number")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter a Name")]
        public string AccountNumber { get; set; }

        [Display(Name = "Currency (ex. USD / TSH (Local Currency)")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Please enter a Currency")]
        public string Currency { get; set; }

        [Display(Name = "Address")]
        [MaxLength(256)]
        [Required(ErrorMessage = "Please enter address")]
        public string BankAddress { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}
