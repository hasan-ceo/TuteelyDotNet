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
    public class Leave
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long LeaveID { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter Leave Name")]
        [Display(Name = "Leave Name")]
        public string LeaveName { get; set; }

        [Required(ErrorMessage = "Please enter Short Code")]
        [MaxLength(4)]
        [Display(Name = "Short Code")]
        public string ShortCode { get; set; }

        [Required(ErrorMessage = "Please enter Yearly Leave")]
        [Display(Name = "Yearly Leave")]
        public int YearlyLeave { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}
