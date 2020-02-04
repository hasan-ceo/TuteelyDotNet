using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRMSMvc.Domain.Entities
{
    [Table("SurveyQuestionQuestion")]
    public class SurveyQuestion
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int SurveyQuestionID { get; set; }

        [Display(Name = "Survey Name")]
        public int SurveyID { get; set; }
        public virtual Survey Survey { get; set; }

        [MaxLength(255)]
        [Display(Name = "Question")]
        public string Question { get; set; }

        [MaxLength(255)]
        [Display(Name = "Option01")]
        public string Option01 { get; set; }

        [MaxLength(255)]
        [Display(Name = "Option02")]
        public string Option02 { get; set; }

        [MaxLength(255)]
        [Display(Name = "Option03")]
        public string Option03 { get; set; }

        [MaxLength(255)]
        [Display(Name = "Option04")]
        public string Option04 { get; set; }

        [MaxLength(255)]
        [Display(Name = "Option05")]
        public string Option05 { get; set; }

        [MaxLength(255)]
        [Display(Name = "Option06")]
        public string Option06 { get; set; }

        [MaxLength(255)]
        [Display(Name = "Option07")]
        public string Option07 { get; set; }

        [MaxLength(255)]
        [Display(Name = "Option08")]
        public string Option08 { get; set; }

        [MaxLength(255)]
        [Display(Name = "Option09")]
        public string Option09 { get; set; }

        [MaxLength(255)]
        [Display(Name = "Option10")]
        public string Option10 { get; set; }



    }
}
