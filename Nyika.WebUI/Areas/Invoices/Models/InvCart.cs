using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nyika.WebUI.Areas.Invoices.Models
{
    public class InvCart
    {
        [DefaultValue(0)]
        public long InvoiceID { get; set; }

        [DefaultValue(false)]
        public bool InEdit { get; set; }

        [DefaultValue(false)]
        public bool DraftSave { get; set; }

        [Required]
        [Display(Name = "Invoice No(*)")]
        [MaxLength(20)]
        public String InvoiceNumber { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

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

        public double SubTotal
        {
            get { return lineCollection.Sum(l => l.Amount); }
        }

        [DefaultValue(0)]
        //[RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        public double Discount { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Tax %")]
        //[RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        public double TaxPercent { get; set; }

        [Display(Name = "Tax")]
        public double Tax
        {
            get
            {
                var SubTotal = lineCollection.Sum(l => l.Amount);
                return ((SubTotal - Discount)* TaxPercent)/100.00;
            }
        }

        [Display(Name = "Invoice Total")]
        public double InvoiceTotal
        {
            get
            {
                var SubTotal = lineCollection.Sum(l => l.Amount);
                return (SubTotal - Discount) + Tax;
            }
        }

        [DefaultValue(0)]
        //[RegularExpression("^0*[0-9][0-9]*(\\.[0-9]+)?|0+\\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Please enter only positive amount")]
        [Display(Name = "Paid to date")]
        public double AmountPaid { get; set; }

        [Display(Name = "Balance")]
        public double DueAmount
        {
            get
            {
                var SubTotal = lineCollection.Sum(l => l.Amount);
                return ((SubTotal - Discount) + Tax) - AmountPaid;
            }
        }



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

        //extra Company info
        [Display(Name = "Company(*)")]
        public long CompanyID { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Email address")]
        public string CompanyEmail { get; set; }

        [Display(Name = "Contact Number")]
        public string CompanyContactNumber { get; set; }

        [Display(Name = "Web Address")]
        public string CompanyWebAddress { get; set; }

        [Display(Name = "Address")]
        public string CompanyAddress { get; set; }

        [Display(Name = "TIN Number")]
        public string CompanyTIN { get; set; }

        [Display(Name = "VAT Number")]
        public string CompanyVAT { get; set; }
        //End Company Info

        //Extra Party Info
        [Required]
        [Display(Name = "Party")]
        public long PartyID { get; set; }

        [Display(Name = "Party Name")]
        public string PartyName { get; set; }

        [Display(Name = "Party Email address")]
        public string PartyEmail { get; set; }

        [Display(Name = "Party Contact Number")]
        public string PartyContactNumber { get; set; }

        [Display(Name = "Party Address")]
        public string PartyAddress { get; set; }

        [Display(Name = "ZIP Code")]
        public string PartyZIPCode { get; set; }
        //End Party info


        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(String Item, String Description, double Cost, double Quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Item == Item && p.Description == Description)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Item = Item,
                    Description = Description,
                    Cost = Cost,
                    Quantity = Quantity,
                    Amount = Cost * Quantity
                });
            }
            //else
            //{
            //    line.Quantity += Quantity;
            //    line.Amount = line.Cost * Quantity;
            //}
        }

        public void RemoveLine(string Item)
        {
            lineCollection.RemoveAll(l => l.Item == Item);
        }


        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
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

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Amount")]
        public double Amount { get; set; }
    }
}
