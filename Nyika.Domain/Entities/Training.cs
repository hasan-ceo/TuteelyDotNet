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
    [Table("Training")]
    public class Training
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int TrainingID { get; set; }

        [MaxLength(255)]
        [Display(Name = "Training Name")]
        public string TrainingName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Training Type")]
        public string TrainingType { get; set; }


        //[Display(Name = "Country")]
        //public int CountryID { get; set; }
        //public virtual Country Country { get; set; }

    }
}
