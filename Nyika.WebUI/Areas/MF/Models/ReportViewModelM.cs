using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.MF.Models
{
    public class ReportViewModelM
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

        [Display(Name = "Loan")]
        public long SelectedLoan { get; set; }

        [Display(Name = "Collection Day")]
        public int SelectedDay { get; set; }


        [Required(ErrorMessage = "Please enter From Date")]
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "Please enter Till Date")]
        [Display(Name = "Till Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TillDate { get; set; }
       
    }
}