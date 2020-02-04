using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nyika.Domain.Entities.Stock
{
    public class Stock
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long StockID { get; set; }

        [MaxLength(100)]
        [Display(Name = "Stock Name")]
        public string StockName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Tag Line")]
        public string TagLine { get; set; }

        [MaxLength(125)]
        [Display(Name = "Logo (Use PNG file Width: 200Px, Height: 100Px)")]
        public string logo { get; set; }

        [MaxLength(125)]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [MaxLength(125)]
        public string Facebook { get; set; }

        [MaxLength(125)]
        public string twitter { get; set; }

        [Required(ErrorMessage = "Please enter a meta Title")]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a meta Keywords")]
        [MaxLength(255)]
        public string Keywords { get; set; }

        [Required(ErrorMessage = "Please enter a meta Description")]
        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(125)]
        [Display(Name = "Slide01 (Use PNG file Width:1120Px, Height: 400Px)")]
        public string Slide01 { get; set; }

        [MaxLength(125)]
        [Display(Name = "Slide01 Terget Url")]
        public string Slide01Link { get; set; }

        [MaxLength(125)]
        [Display(Name = "Slide02 (Use PNG file Width: 1120Px, Height: 400Px)")]
        public string Slide02 { get; set; }

        [MaxLength(125)]
        [Display(Name = "Slide02 Terget Url")]
        public string Slide02Link { get; set; }

        [MaxLength(125)]
        [Display(Name = "Slide03 (Use PNG file Width: 1120Px, Height: 400Px)")]
        public string Slide03 { get; set; }

        [MaxLength(125)]
        [Display(Name = "Slide03 Terget Url")]
        public string Slide03Link { get; set; }

    }
}
