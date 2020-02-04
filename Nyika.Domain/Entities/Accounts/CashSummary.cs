using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nyika.Domain.Entities.Accounts
{
    public class CashSummary
    {
        [Key]
        public long CashSummaryID { get; set; }

        [Display(Name = "Acc. ID")]
        [MaxLength(255)]
        public string AccountSubHeadID { get; set; }

        [Display(Name = "Acc. Name")]
        [MaxLength(255)]
        public string AccountSubHeadName { get; set; }

        [Display(Name = "Bank ID")]
        [MaxLength(255)]
        public string BankID { get; set; }

        [Display(Name = "Bank Name")]
        [MaxLength(255)]
        public string BankName { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public double Balance { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }
    }
}
