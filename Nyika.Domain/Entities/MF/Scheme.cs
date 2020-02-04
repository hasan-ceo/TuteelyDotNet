using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.MF
{
    public class Scheme
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public long SchemeID { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Scheme Name")]
        public string SchemeName { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}
