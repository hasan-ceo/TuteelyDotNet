using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.ComponentModel;

namespace Nyika.WebUI.Areas.MF.Models
{
    public class LoanVM
    {
        [MaxLength(50)]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Group Name")]
        public string GroupsName { get; set; }

        [Required]
        [Display(Name = "Member")]
        public long MemberID { get; set; }

        [Display(Name = "Member No")]
        public long MemberNo { get; set; }

        [MaxLength(50)]
        [Display(Name = "Member Name")]
        public string MemberName { get; set; }

        [Required]
        [Display(Name = "Product")]
        public long ProductID { get; set; }

        [Required]
        [Display(Name = "Scheme")]
        public long SchemeID { get; set; }

        [Required]
        [DefaultValue(0)]
        [Range(1, 50000000)]
        [Display(Name = "Disbursed Amount")]
        public double DisbursedAmount { get; set; }
    }
}