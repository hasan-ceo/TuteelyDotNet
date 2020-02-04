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
    public class AttenStatus
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long AttenStatusID { get; set; }

        [MaxLength(255)]
        [Display(Name = "Atten Status")]
        public string AttenStatusName { get; set; }

        [MaxLength(10)]
        [Display(Name = "StatusCode")]
        public string StatusCode { get; set; }

    }
}
