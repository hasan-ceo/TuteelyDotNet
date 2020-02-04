using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.Accounts
{
    public class BusinessMonthStatus
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long BusinessMonthStatusID { get; set; }

        [Display(Name = "BusinessMonth Name")]
        public int BusinessYear { get; set; }

        [Display(Name = "Account Number")]
        public int BusinessMonth { get; set; }
               
        public bool Status { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}
