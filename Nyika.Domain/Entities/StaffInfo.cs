using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRMSMvc.Domain.Entities
{
    [Table("StaffInfo")]
    public class StaffInfo
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public long StaffInfoID { get; set; }

        [Required]
        [Display(Name = "Company(*)")]
        public int CompanyID { get; set; }
        public virtual Company Company { get; set; }

        [Required]
        [Display(Name = "Division(*)")]
        public int DivisionID { get; set; }
        public virtual Division Division { get; set; }

        [Required]
        [Display(Name = "Region(*)")]
        public int RegionID { get; set; }
        public virtual Region Region { get; set; }

        [Required]
        [Display(Name = "Area(*)")]
        public int AreaID { get; set; }
        public virtual Area Area { get; set; }

        [Required]
        [Display(Name = "Branch(*)")]
        public int BranchID { get; set; }
        public virtual Branch Branch { get; set; }

        [Required]
        [Display(Name = "Staff Type(*)")]
        public int StaffTypeID { get; set; }
        public virtual StaffType StaffType { get; set; }

        [Display(Name = "PIN")]
        public String PIN { get; set; }

        [Required]
        [Display(Name = "Staff Name(*)")]
        public string StaffName { get; set; }

        [Required]
        [Display(Name = "Mother Name(*)")]
        public string MotherName { get; set; }

        [Display(Name = "Spouse Name(*)")]
        public string SpouseName { get; set; }


        [Required]
        [Display(Name = "Date of Birth(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DoB { get; set; }

        [Required]
        [Display(Name = "Gender(*)")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Address(*)")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Home Country(*)")]
        public int HomeCountryID { get; set; }
        public virtual HomeCountry HomeCountry { get; set; }

        [Required]
        [Display(Name = "National Id Card(*)")]
        public string NationalIdNumber { get; set; }

        [Display(Name = "Passport Number")]
        public string PassportNumber { get; set; }

        [Required]
        [Display(Name = "1st Contact #(*)")]
        public string PrimaryContact { get; set; }

        [Display(Name = "2nd Contact #")]
        public string SecondaryContact { get; set; }

        [Required]
        [Display(Name = "Religion(*)")]
        public string Religion { get; set; }

        [Required]
        [Display(Name = "Marital Status(*)")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [Required]
        [Display(Name = "Joining Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime JoiningDate { get; set; }

        [Required]
        [Display(Name = "1st Reference(*)")]
        public string Reference01 { get; set; }

        [Required]
        [Display(Name = "2nd Reference(*)")]
        public string Reference02 { get; set; }

        [Required]
        [Display(Name = "Designation(*)")]
        public int DesignationID { get; set; }
        public virtual Designation Designation { get; set; }

        [Required]
        [Display(Name = "Used Desig.(*)")]
        public string UsedDesignation { get; set; }

        [Required]
        [Display(Name = "Work Station(*)")]
        public string WorkStationType { get; set; }

        [Required]
        [Display(Name = "Last Education(*)")]
        public int EducationID { get; set; }
        public virtual Education Education { get; set; }

        [Required]
        [Display(Name = "Program(*)")]
        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }

        [Display(Name = "W.Permit Expire(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? WorkPermitEndDate { get; set; }

        [Display(Name = "Visa Type")]
        public string VisaType { get; set; }

        [Display(Name = "Visa Expiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? VisaExpiryDate { get; set; }

        [Required]
        [Display(Name = "Cont. Start Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ContractStartDate { get; set; }

        [Required]
        [Display(Name = "Cont. End Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ContractEndDate { get; set; }

        public bool ProbationEnd { get; set; }

        [Required]
        [Display(Name = "Employee Status(*)")]
        public string EmployeeStatus { get; set; }

        [Display(Name = "Inactive Reason")]
        public int InactiveReasonID { get; set; }
        public virtual InactiveReason InactiveReason { get; set; }

        [Display(Name = "Inactive Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? InactiveReasonDate { get; set; }

        [Display(Name = "Supervisor PIN")]
        public long SupervisorPIN { get; set; }

        [Required]
        [Display(Name = "Salary Grade")]
        public int SalaryGradeID { get; set; }
        public virtual SalaryGrade SalaryGrade { get; set; }


        [Required]
        [DefaultValue(0)]
        [Display(Name = "Salary Slab")]
        public int SalarySlab { get; set; }
        
        [Required]
        [DefaultValue(0)]
        [Display(Name = "Gross Salary")]
        public double GrossSalary { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Basic Salary")]
        public double? BasicSalary { get; set; }

        [DefaultValue(0)]
        [Display(Name = "House Rent")]
        public double? HouseRent { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Transport")]
        public double? Transport { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Medical")]
        public double? Medical { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Others Allowance")]
        public double OthersAllowance { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Lunch Allowance")]
        public double LunchAllowance { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "MF Allowance")]
        public double MFAllowance { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Charge Allowance")]
        public double ChargeAllowance { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "Tax Deduction")]
        public double TaxDeduction { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "PPF / NSSF")]
        public double PPFNSSF { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "SDL")]
        public double SDL { get; set; }

        [Required]
        [DefaultValue("")]
        [Display(Name = "Form Four Index Number")]
        public string FormFourIndexNumber { get; set; }

        [Required]
        [DefaultValue("")]
        [Display(Name = "Full Name UsedD uring Study")]
        public string FullNameUsedDuringStudy { get; set; }

        [Required]
        [DefaultValue("")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Required]
        [DefaultValue("")]
        [Display(Name = "Current Name If Changed")]
        public string CurrentNameIfChanged { get; set; }

        [Required]
        [DefaultValue("")]
        [Display(Name = "Year Of Completion")]
        public string YearOfCompletion { get; set; }

        [Required]
        [DefaultValue("")]
        [Display(Name = "Name Of Institution")]
        public string NameOfInstitution { get; set; }
    }

}
