using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.Accounts
{
    public class AccountType
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long AccountTypeID { get; set; }

        [Display(Name = "Account Type Name")]
        [Required(ErrorMessage = "Please enter a Name")]
        [MaxLength(50)]
        public string AccountTypeName { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

    }
}
