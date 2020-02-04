using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;
using Nyika.Domain.Entities.Accounts;
using Nyika.Domain.Entities.Setup;

namespace Nyika.Domain.Entities.Invoices
{
    public class Invoice
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long InvoiceID { get; set; }

        [Required]
        [Display(Name = "Invoice No(*)")]
        [MaxLength(20)]
        public String InvoiceNumber { get; set; }

        [Required]
        [Display(Name = "Company(*)")]
        public long CompanyID { get; set; }
        public virtual Company Company { get; set; }

        [Required]
        [Display(Name = "Party")]
        public long PartyID { get; set; }
        public virtual Party Party { get; set; }

        [Required]
        [Display(Name = "Invoice Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [Required]
        [Display(Name = "Due Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Job")]
        public string JobDescription { get; set; }

        [DefaultValue(0)]
        public double SubTotal { get; set; }

        [DefaultValue(0)]
        //[RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        public double Discount { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Tax %")]
        //[RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        public double TaxPercent { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Tax")]
        //[RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        public double Tax { get; set; }

        [DefaultValue(0)]
        //[RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        [Display(Name = "Invoice Total")]
        public double InvoiceTotal { get; set; }

        [DefaultValue(0)]
        //[RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        [Display(Name = "Paid to date")]
        public double AmountPaid { get; set; }

        [DefaultValue(0)]
        //[RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        [Display(Name = "Balance")]
        public double DueAmount { get; set; }

        [MaxLength(3)]
        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [MaxLength(20)]
        [Display(Name = "Stamp")]
        public string Stamp { get; set; }

        [MaxLength(512)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Payment Terms")]
        public string PaymentTerms { get; set; }

        [MaxLength(512)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Client Notes")]
        public string ClientNotes { get; set; }

        [Display(Name = "Payment Complete")]
        public bool PaymentComplete { get; set; }

        [Required]
        [Display(Name = "Work Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime WorkDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Entry By")]
        public string EntryBy { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}
