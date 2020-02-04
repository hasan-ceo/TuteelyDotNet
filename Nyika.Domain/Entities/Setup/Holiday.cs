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
    public class Holiday
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long HolidayID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Holiday Name")]
        public string HolidayName { get; set; }

        [Required(ErrorMessage = "Please enter From Date")]
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "Please enter Till Date")]
        [Display(Name = "Till Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TillDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

    }
}
