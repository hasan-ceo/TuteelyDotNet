using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;
using Nyika.Domain.Entities.Accounts;
using Nyika.Domain.Entities.Setup;

namespace Nyika.Domain.Entities.Invoices
{
    public class InvoiceDetails
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long InvoiceDetailsID { get; set; }

        public long InvoiceID { get; set; }
        public Invoice Invoice { get; set; }

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

        [DefaultValue(0)]
        [Display(Name = "Amount")]
        public double Amount { get; set; }
        //[Required]
        //[Display(Name = "Work Date(*)")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime WorkDate { get; set; }

        //[MaxLength(50)]
        //[Display(Name = "Entry By")]
        //public string EntryBy { get; set; }

        //[MaxLength(50)]
        //[Display(Name = "InstanceID")]
        //public string InstanceID { get; set; }
    }
}
