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
    public class ResignReason
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long ResignReasonID { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter Reason Name")]
        [Display(Name = "Reason Name")]
        public string ResignReasonName { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}
