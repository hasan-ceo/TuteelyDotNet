using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nyika.Domain.Entities.Setup
{
    public class ErrorLog
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public long ErrorLogID { get; set; }

        [MaxLength(500)]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required]
        [Display(Name = "Date & Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ErrorDateTime { get; set; }

    }
}
