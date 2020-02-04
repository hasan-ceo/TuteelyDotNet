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
    public class PaypalIPN
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long PaypalIPNID { get; set; }

        public string PaymentType { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public string PendingReason { get; set; }

        public string PayerAddressStatus { get; set; }
        public string PayerStatus { get; set; }

        public string PayerFirstName { get; set; }
        public string PayerLastName { get; set; }
        public string PayerEmail { get; set; }
        public string PayerID { get; set; }
        public string PayerCountry { get; set; }
        public string PayerCountryCode { get; set; }
        public string PayerZipCode { get; set; }
        public string PayerState { get; set; }
        public string PayerCity { get; set; }
        public string PayerStreet { get; set; }

        public string Business { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverID { get; set; }
        public string ItemName { get; set; }
        public string ItemNumber { get; set; }
        public string Quantity { get; set; }
        public string Shipping { get; set; }

        public string Tax { get; set; }
        public string Currency { get; set; }
        public string PaymentFee { get; set; }
        public string PaymentGross { get; set; }
        public string TXN_ID { get; set; }
        public string TXN_Type { get; set; }
        public string NotifyVersion { get; set; }
        public string Custom { get; set; }
        public string Invoice { get; set; }
        public string VerifySign { get; set; }
        public string QuantityCartItems { get; set; }
    }
}
