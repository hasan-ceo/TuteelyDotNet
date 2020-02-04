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
    public class DcrVM
    {
        public SelectList Project { get; set; }

        [Required]
        [Display(Name = "Project")]
        public long SelectedProject { get; set; }

        [Required]
        [Display(Name = "Group")]
        public long SelectedGroup { get; set; }


        [Required]
        [Display(Name = "Amount")]
        public long Amount { get; set; }

        [Display(Name = "Particulars")]
        public string Particulars { get; set; }


        public List<LoanCollectionVM> loanCollection { get; set; }

        [Display(Name = "Total Target Amount")]
        public double TotalTargetAmount { get; set; }
    }
}