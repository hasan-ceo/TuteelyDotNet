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
    public class SecurityVM
    {
        public SelectList Project { get; set; }

        [Required]
        [Display(Name = "Project")]
        public long SelectedProject { get; set; }

        [Required]
        [Display(Name = "Group")]
        public long SelectedGroup { get; set; }

        [Required]
        [Display(Name = "Member")]
        public long SelectedMember { get; set; }


        [Required]
        [Display(Name = "Amount")]
        public long Amount { get; set; }

        [Display(Name = "Particulars")]
        public string Particulars { get; set; }
    }
}