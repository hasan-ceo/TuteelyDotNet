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
    public class MemberVM
    {
        public SelectList Project { get; set; }

        [Required]
        [Display(Name = "Project")]
        public long SelectedProject { get; set; }

        [Required]
        [Display(Name = "Group")]
        public long SelectedGroup { get; set; }

        public Member member { get; set; }
    }
}