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
    public class Section
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public long SectionID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Section Name")]
        public string SectionName { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

    }
}
