using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;
using Nyika.Domain.Entities.Setup;

namespace Nyika.Domain.Entities.HR
{
    public class EmployeePayroll
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long EmployeePayrollID { get; set; }

        [Display(Name = "Year")]
        public int SalaryYear { get; set; }

        [Display(Name = "Month")]
        public int SalaryMonth { get; set; }

        [Display(Name = "Employee")]
        public long EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [Display(Name = "PIN/Employee No")]
        [MaxLength(50)]
        public String PIN { get; set; }

        [Display(Name = "Employee Name")]
        [MaxLength(100)]
        public string EmployeeName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Company Address")]
        public string CompanyAddress { get; set; }

        [MaxLength(50)]
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Section")]
        public string SectionName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Designation")]
        public string DesignationName { get; set; }

        [Display(Name = "Joining Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoiningDate { get; set; }

        [MaxLength(10)]
        [DefaultValue("")]
        [Display(Name = "PPF / NSSF")]
        public string PPFNSSF { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        [Display(Name = "PPF / NSSF Number")]
        public string PPFNSSFNumber { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        [Display(Name = "Bank Account Number")]
        public string BankAccountNumber { get; set; }

        [DefaultValue("")]
        [Display(Name = "Bank Name")]
        [MaxLength(50)]
        public string BankName { get; set; }

        [DefaultValue("")]
        [Display(Name = "Study Index Number")]
        [MaxLength(32)]
        public string IndexNumber { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Basic Salary")]
        public double BasicSalary { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Other Benefits")]
        public double OtherBenefits { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Gross Salary")]
        public double GrossSalary { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Lunch Allowance")]
        public double LunchAllowance { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Professional Allowance")]
        public double ProfessionalAllowance { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Total Allowance")]
        public double TotalAllowance { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Total Deduction")]
        public double TotalDeduction { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Total Income")]
        public double TotalIncome { get; set; }

        [DefaultValue(0)]
        [Display(Name = "SDL")]
        public double SDL { get; set; }

        [DefaultValue(0)]
        [Display(Name = "NSSF/PPF Employee")]
        public double NSSFPPFEmployee { get; set; }

        [DefaultValue(0)]
        [Display(Name = "NSSF/PPF Employer")]
        public double NSSFPPFEmployer { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Total NSSF/PPF Employee+Employer")]
        public double TotalNSSFPPF { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Taxable Income")]
        public double TaxableIncome { get; set; }

        [DefaultValue(0)]
        [Display(Name = "TAX PAYE")]
        public double TAXPAYE { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Higher Study Loan")]
        public double HigherStudyLoan { get; set; }

        [DefaultValue(0)]
        [Display(Name = "National Health Insurance Fund (NHIF)")]
        public double NHIF { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Other Deduction")]
        public double OtherDeduction { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Net payment")]
        public double Netpayment { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Total Salary Expenses")]
        public double TotalSalaryExpenses { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Total SDL + TAX-PAYE")]
        public double TotalSDLTAXPAYE { get; set; }



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
