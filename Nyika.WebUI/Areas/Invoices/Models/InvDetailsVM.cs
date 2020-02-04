using Nyika.Domain.Entities.Invoices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nyika.WebUI.Areas.Invoices.Models
{
    public class InvDetailsVM
    {
        [Required]
        [Display(Name = "Item")]
        [MaxLength(100)]
        public String Item { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MaxLength(512)]
        public String Description { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Cost")]
        public double Cost { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Quantity")]
        public double Quantity { get; set; }
    }
}