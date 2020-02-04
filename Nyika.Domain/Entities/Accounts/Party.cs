using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.Accounts
{
    public class Party
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long PartyID { get; set; }

        [Display(Name = "Party Name")]
        [MaxLength(255)]
        [Required(ErrorMessage = "Please enter a Name")]
        public string PartyName { get; set; }

        [Display(Name = "Email address")]
        [MaxLength(50)]
        [DefaultValue("")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Contact Number")]
        [MaxLength(20)]
        [Required(ErrorMessage = "Please enter Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Address")]
        [MaxLength(255)]
        [Required(ErrorMessage = "Please enter Address")]
        public string Address { get; set; }

        [Display(Name = "ZIP Code")]
        [MaxLength(255)]
        [Required(ErrorMessage = "Please enter ZIP Code")]
        public string ZIPCode { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}
