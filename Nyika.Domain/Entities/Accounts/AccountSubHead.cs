using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.Accounts
{
    public class AccountSubHead
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long AccountSubHeadID { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Please enter a code")]
        [Display(Name = "Sub Head Code")]
        public string AccountSubHeadCode { get; set; }

        [Required(ErrorMessage = "Please enter a Name")]
        [MaxLength(50)]
        [Display(Name = "Sub Head Name")]
        public string AccountSubHeadName { get; set; }

        [Display(Name = "Main Head")]
        public long AccountMainHeadID { get; set; }
        public virtual AccountMainHead AccountMainHead { get; set; }

        public bool AutoAc { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

    }
}
