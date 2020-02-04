using Nyika.Domain.Entities.Accounts;
using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nyika.Domain.Entities.HR
{
    public class Employee
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public long EmployeeID { get; set; }

        [Required]
        [Display(Name = "PIN/Employee No(*)")]
        [MaxLength(50)]
        public String PIN { get; set; }

        [Required]
        [Display(Name = "Employee Name(*)")]
        [MaxLength(100)]
        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "Company(*)")]
        public long CompanyID { get; set; }
        public virtual Company Company { get; set; }

        [Required]
        [Display(Name = "Department(*)")]
        public long DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        [Display(Name = "Section(*)")]
        public long SectionID { get; set; }
        public virtual Section Section { get; set; }

        [Required]
        [Display(Name = "Designation(*)")]
        public long DesignationID { get; set; }
        public virtual Designation Designation { get; set; }

        [Required]
        [Display(Name = "Staff Type(*)")]
        public long EmploymentTypeID { get; set; }
        public virtual EmploymentType EmploymentType { get; set; }

        [Required]
        [Display(Name = "Joining Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoiningDate { get; set; }

        [Required]
        [Display(Name = "Cont. Start Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ContractStartDate { get; set; }

        [Required]
        [Display(Name = "Cont. End Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ContractEndDate { get; set; }

        [Required]
        [DefaultValue("")]
        [MaxLength(10)]
        [Display(Name = "Gender(*)")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Date of Birth(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DoB { get; set; }

        [Required]
        [MaxLength(100)]
        [DefaultValue("")]
        [Display(Name = "Mother Name(*)")]
        public string MotherName { get; set; }

        [Display(Name = "Father Name(*)")]
        [MaxLength(100)]
        [Required]
        [DefaultValue("")]
        public string FatherName { get; set; }

        

        [Required]
        [MaxLength(20)]
        [DefaultValue("")]
        [Display(Name = "Religion(*)")]
        public string Religion { get; set; }

        [Required]
        [Display(Name = "Marital Status(*)")]
        [MaxLength(20)]
        [DefaultValue("")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Blood Group")]
        [MaxLength(10)]
        [DefaultValue("")]
        public string BloodGroup { get; set; }

        [Required]
        [Display(Name = "Education(*)")]
        public long EducationID { get; set; }
        public virtual Education Education { get; set; }

        [Required]
        [MaxLength(50)]
        [DefaultValue("")]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Required]
        [Display(Name = "Email address")]
        [MaxLength(50)]
        [DefaultValue("")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [MaxLength(250)]
        [DefaultValue("")]
        [Display(Name = "Address(*)")]
        public string Address { get; set; }

        [MaxLength(50)]
        [Display(Name = "Punch Card Number")]
        public string CardNumber { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Basic Salary")]
        public double BasicSalary { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Other Benefits")]
        public double OtherBenefits { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Gross Salary")]
        public double GrossSalary { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Lunch Allowance")]
        public double LunchAllowance { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Professional Allowance")]
        public double ProfessionalAllowance { get; set; }


        [Required]
        [MaxLength(10)]
        [DefaultValue("")]
        [Display(Name = "PPF / NSSF")]
        public string PPFNSSF { get; set; }

        [Required]
        [DefaultValue("")]
        [MaxLength(50)]
        [Display(Name = "PPF / NSSF Number")]
        public string PPFNSSFNumber { get; set; }

        [Required]
        [DefaultValue("")]
        [MaxLength(50)]
        [Display(Name = "Bank Account Number")]
        public string BankAccountNumber { get; set; }


        [DefaultValue("")]
        [Display(Name = "Bank Name")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter a Name")]
        public string BankName { get; set; }

        [DefaultValue("")]
        [Display(Name = "Study Index Number")]
        [MaxLength(32)]
        [Required(ErrorMessage = "Please enter Study Index Number")]
        public string IndexNumber { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Employee Status(*)")]
        public int EmployeeStatus { get; set; }

        [DefaultValue("")]
        [MaxLength(255)]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [MaxLength(50)]
        [Display(Name = "Entry By")]
        public string EntryBy { get; set; }

        [Required(ErrorMessage = "Please Enter Working Date")]
        [Display(Name = "Working Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime WorkDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

    }

}
