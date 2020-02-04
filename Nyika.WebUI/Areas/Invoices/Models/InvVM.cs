using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.Invoices.Models
{
    public class InvVM
    {

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

        [Required]
        [Display(Name = "Invoice No(*)")]
        [MaxLength(20)]
        public String InvoiceNumber { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Job")]
        public string JobDescription { get; set; }

        public long PartyID { get; set; }

        [MaxLength(512)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Payment Terms")]
        public string PaymentTerms { get; set; }

        [MaxLength(512)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Client Notes")]
        public string ClientNotes { get; set; }

        [DefaultValue(0)]
        public double Discount { get; set; }

        [DefaultValue(0)]
        public double TaxPercent { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Amount Paid")]
        public double AmountPaid { get; set; }
    }
}