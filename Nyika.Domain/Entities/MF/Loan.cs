using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.MF
{
    public class Loan
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public long LoanID { get; set; }

        [Required]
        [Display(Name = "Member")]
        public long MemberID { get; set; }
        public virtual Member Member { get; set; }

        [Required]
        [Display(Name = "Product")]
        public long ProductID { get; set; }

        [Required]
        [Display(Name = "Scheme")]
        public long SchemeID { get; set; }
        public virtual Scheme Scheme { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Loan No")]
        public string LoanNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Disbursement Date")]
        public DateTime DisbursementDate { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Loan Status")]
        public string LoanStatus { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Settlement Date")]
        public DateTime SettlementDate { get; set; }

        public bool isSettle { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Loan Cycle")]
        public int LoanCycle { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Disbursed Amount")]
        public double DisbursedAmount { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Interest Rate")]
        public double InterestRate { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Interest Amount")]
        public double InterestAmount { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Total Loan Amount")]
        public double TotalLoanAmount { get; set; }

        [Required]
        [DefaultValue(0)]
        [DisplayFormat(DataFormatString = "{0:0.0000000000}")]
        [Display(Name = "Int Factor")]
        public double IntFactor { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Duration")]
        public int Duration { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "No Of Installment")]
        public int NoOfInstallment { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Installment Amount")]
        public double InstallmentAmount { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Security Amount")]
        public double SecurityAmount { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Principal Amount")]
        public double PrincipalAmount { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Interest Receivable")]
        public double InterestReceivable { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Interest Realizable")]
        public double InterestRealizable { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Loan Due")]
        public double LoanDue { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Overdue Amount")]
        public double OverdueAmount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Loan Expire Date")]
        public DateTime LoanExpireDate { get; set; }


        [DefaultValue(false)]
        [Display(Name = "Has Provision")]
        public bool HasProvision { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Has Collection")]
        public bool HasCollection { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Loan Aprroved")]
        public bool LoanAprroved { get; set; }

        [MaxLength(50)]
        [Display(Name = "Entry By")]
        public string EntryBy { get; set; }

        [MaxLength(50)]
        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Approved Date")]
        public DateTime ApprovedDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

       
    }
}
