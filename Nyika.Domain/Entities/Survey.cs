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
    [Table("Survey")]
    public class Survey
    {
        [Key]
        [HiddenInput(DisplayValue = true)]
        public int SurveyID { get; set; }

        [MaxLength(255)]
        [Required]
        [Display(Name = "Survey Name")]
        public string SurveyName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Public Link")]
        public string PublicLink { get; set; }

        //[Display(Name = "Country")]
        //public int CountryID { get; set; }
        //public virtual Country Country { get; set; }

    }
}
