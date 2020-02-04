using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.Accounts
{
    public enum TransType
    {
        CASH,
        BANK
    }

    public class AccountGL
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long AccountGLID { get; set; }

        //[Required(ErrorMessage = "Please enter a Voucher No")]
        //[ReadOnly(true)]
        [MaxLength(50)]
        public string Vno { get; set; }

        [Required(ErrorMessage = "Please enter Transaction Date")]
        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime WorkDate { get; set; }

        [Display(Name = "Account Head")]
        public long AccountSubHeadID { get; set; }
        public virtual AccountSubHead AccountSubHead { get; set; }

        [Display(Name = "Transaction Type")]
        public TransType TransType { get; set; }

        [DefaultValue(0)]
        public double dr { get; set; }

        [DefaultValue(0)]
        public double cr { get; set; }

        [Required(ErrorMessage = "Please enter particulars")]
        [Display(Name = "Particulars")]
        [MaxLength(512)]
        [DataType(DataType.MultilineText)]
        public string Particulars { get; set; }

        [Display(Name = "Date of Entry")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EntryDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        [DefaultValue(0)]
        public long BankID { get; set; }


        [HiddenInput(DisplayValue = false)]
        [DefaultValue(0)]
        public long PartyID { get; set; }

        [HiddenInput(DisplayValue = false)]
        [DefaultValue(0)]
        public long ProjectID { get; set; }

        [HiddenInput(DisplayValue = false)]
        [DefaultValue(0)]
        public long GroupsID { get; set; }

        [HiddenInput(DisplayValue = false)]
        [DefaultValue(0)]
        public long MemberID { get; set; }

        [HiddenInput(DisplayValue = false)]
        [DefaultValue(0)]
        public long LoanID { get; set; }

        // [HiddenInput(DisplayValue = false)]
        [MaxLength(50)]
        public string VType { get; set; }

        [MaxLength(50)]
        public string RefID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Entry By")]
        public string EntryBy { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

    }

    public class AccountGLVM
    {
      
        public long AccountGLID { get; set; }
     
        public string Vno { get; set; }

        public DateTime WorkDate { get; set; }

        [DefaultValue(0)]
        public double amount { get; set; }

        public string Particulars { get; set; }

        public string GroupsName { get; set; }

        public string ProjectName { get; set; }

        public string MemberNo { get; set; }

        public string MemberName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Entry By")]
        public string EntryBy { get; set; }

    }
}
