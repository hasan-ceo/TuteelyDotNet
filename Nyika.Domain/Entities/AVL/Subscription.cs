using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nyika.Domain.Entities.AVL
{
    public class Subscription
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public long SubscriptionID { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        [Display(Name = "User ID")]
        public string UserEmail { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        [Display(Name = "Payer Email")]
        public string PayerEmail { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        [Display(Name = "Payer Name")]
        public string PayerName { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        [Display(Name = "Subscription Type")]
        public string SubscriptionType { get; set; }

        [Display(Name = "Subscription Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SubscriptionDate { get; set; }

        [Display(Name = "Expire Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpairDate { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        [Display(Name = "PayPal Transaction ID")]
        public string PaypalTransactionID { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Receive Amount")]
        public double Amount { get; set; }

       
    }
}
