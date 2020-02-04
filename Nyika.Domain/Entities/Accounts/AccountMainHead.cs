using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.Accounts
{
    public class AccountMainHead
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long  AccountMainHeadID { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Please enter a code")]
        [Display(Name = "Main Head Code")]
        public string AccountMainHeadCode { get; set; }

        [Display(Name = "Main Head Name")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter a Name")]
        public string AccountMainHeadName { get; set; }

        [Display(Name = "Account Type")]
        public long AccountTypeID { get; set; }
        public virtual AccountType AccountType { get; set; }

        public bool AutoAc { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

    }
}
