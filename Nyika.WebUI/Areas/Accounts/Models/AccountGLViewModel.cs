using Nyika.Domain.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.Accounts.Models
{

    public class AccountGLViewModel
    {
        [Display(Name = "Acc. ID")]
        public int AccountSubHeadID { get; set; }

        [Display(Name = "Acc. Name")]
        public string AccountSubHeadName { get; set; }

        [Display(Name = "Bank ID")]
        public int BankID { get; set; }

        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public double Balance { get; set; }
    }
}