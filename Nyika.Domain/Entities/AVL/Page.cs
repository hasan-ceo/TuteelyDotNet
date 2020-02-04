using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nyika.Domain.Entities.AVL
{
    public class Page
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public long PageID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [AllowHtmlAttribute]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

    }
}
