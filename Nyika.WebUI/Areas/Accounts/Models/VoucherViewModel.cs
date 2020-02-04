using Nyika.Domain.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.Accounts.Models
{

    public enum TransType
    {
        CASH,
        BANK
    }


    public class VoucherViewModel
    {
        [Display(Name = "Transaction Type")]
        public TransType TransType { get; set; }

        [HiddenInput(DisplayValue = false)]
        public long? BankId { get; set; }

        [Display(Name = "Account Head")]
        public long AccountHeadId { get; set; }

        [Display(Name = "Company")]
        public long CompanyID { get; set; }


        [RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        [Required(ErrorMessage = "Please enter Amount")]
        [Display(Name = "Amount")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Please enter particulars")]
        [Display(Name = "Particulars")]
        public string Pat { get; set; }

        [Display(Name = "Voucher Type")]
        public string VType { get; set; }
    }
}