using System;
using System.ComponentModel.DataAnnotations;

namespace Nyika.Domain.Entities.MF
{
    public class LoanCollectionVM
    {
        [Required]
        public long LoanCollectionID { get; set; }

        [Display(Name = "Member No")]
        public long MemberNo { get; set; }

        [Display(Name = "Member Name")]
        public string MemberName { get; set; }

        [Display(Name = "Security balance")]
        public double SecurityAmountTotal { get; set; }

        [Display(Name = "Loan ID")]
        public long LoanID { get; set; }

        [Display(Name = "Loan No")]
        public string LoanNo { get; set; }

        [Display(Name = "Disbursement Date")]
        public DateTime DisbursementDate { get; set; }

        [Display(Name = "Loan Status")]
        public string LoanStatus { get; set; }

        [Display(Name = "Disbursed Amount")]
        public double DisbursedAmount { get; set; }

        [Display(Name = "Installment Amount")]
        public double InstallmentAmount { get; set; }

        [Display(Name = "Target Amount")]
        public double TargetAmount { get; set; }

        [Display(Name = "Realized Amount")]
        public double RealizedAmount { get; set; }

        [Display(Name = "Collected")]
        public bool Collected { get; set; }
    }
}