using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Nyika.Domain.Entities.MF;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.MF.Models
{
    public class LoanRealizedVM
    {
        [Required]
        public long LoanCollectionID { get; set; }

        [Required]
        public long LoanID { get; set; }

        [Required]
        [Display(Name = "Realized Amount")]
        public long RealizedAmount { get; set; }
    }
}