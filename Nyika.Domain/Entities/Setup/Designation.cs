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
    public class Designation
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long DesignationID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Designation Name")]
        public string DesignationName { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

    }
}
