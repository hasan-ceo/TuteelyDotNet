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
    [Table("SurveyAnswer")]
    public class SurveyAnswer
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int SurveyAnswerID { get; set; }

        [Display(Name = "Survey Name")]
        public int SurveyID { get; set; }
        public virtual Survey Survey { get; set; }

        [Display(Name = "Question")]      
        public string Question { get; set; }

        [MaxLength(255)]
        [Display(Name = "Answer")]
        public string Answer { get; set; }

        [MaxLength(255)]
        [Display(Name = "PIN")]
        public string PIN { get; set; }

    }
}
