using Nyika.Domain.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.Accounts.Models
{

    public class VoucherReverseViewModel
    {
        [Required(ErrorMessage = "Please enter Transaction Date")]
        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "Please enter Transaction Date")]
        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TillDate { get; set; }
    }
}