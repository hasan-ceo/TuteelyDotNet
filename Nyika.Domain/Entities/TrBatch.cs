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
    [Table("TrBatch")]
    public class TrBatch
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int TrBatchID { get; set; }

        [MaxLength(255)]
        [Required]
        [Display(Name = "Batch Name (*)")]
        public string TrBatchName { get; set; }

        [Display(Name = "Training Name (*)")]
        [Required]
        public int TrainingID { get; set; }
        public virtual Training Training { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Venue Name (*)")]
        public string VenueName { get; set; }

        [Required]
        [Display(Name = "Start Date (*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date (*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }


        [MaxLength(255)]
        [Required]
        [Display(Name = "Facilitator (*)")]
        public string Facilitator { get; set; }



    }
}
