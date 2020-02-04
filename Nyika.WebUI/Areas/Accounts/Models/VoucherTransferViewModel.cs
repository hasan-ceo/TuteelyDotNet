using Nyika.Domain.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.Accounts.Models
{

    public enum TransferType
    {
        Deposit,
        Withdraw
    }

    public class VoucherTransferViewModel
    {

        [Display(Name = "Transaction Type")]
        public TransferType TransType { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int BankID { get; set; }

        [RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        [Required(ErrorMessage = "Please enter Amount")]
        [Display(Name = "Amount")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Please enter particulars")]
        [Display(Name = "Particulars")]
        [DataType(DataType.MultilineText)]
        public string Pat { get; set; }

        [Display(Name = "Voucher Type")]
        public string VType { get; set; }
    }
}