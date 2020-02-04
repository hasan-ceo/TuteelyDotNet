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
    [Table("TransferInfo")]
    public class TransferInfo
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public long TransferInfoID { get; set; }

        [Display(Name = "Pin")]
        public long StaffInfoID { get; set; }
        public virtual StaffInfo StaffInfo { get; set; }

        [Required]
        [Display(Name = "Company(*)")]
        public int CompanyID { get; set; }

        [Required]
        [Display(Name = "Division(*)")]
        public int DivisionID { get; set; }
        
        [Required]
        [Display(Name = "Region(*)")]
        public int RegionID { get; set; }
       
        [Required]
        [Display(Name = "Area(*)")]
        public int AreaID { get; set; }
        
        [Required]
        [Display(Name = "Branch(*)")]
        public int BranchID { get; set; }

        [Required]
        [Display(Name = "Designation(*)")]
        public int DesignationID { get; set; }
              
        [Required]
        [Display(Name = "Program(*)")]
        public int ProgramID { get; set; }
               
        [Required]
        [Display(Name = "Salary Grade")]
        public int SalaryGradeID { get; set; }
        
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

      
        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Required]
        [Display(Name = "Update Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Updatedate { get; set; }

    }

}
