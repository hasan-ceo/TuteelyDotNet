using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel;

namespace Nyika.Domain.Entities.MF
{
    public class Member
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public long MemberID { get; set; }

        [Required]
        [Display(Name = "Group")]
        public long GroupsID { get; set; }
        public virtual Groups Groups { get; set; }

        [Display(Name = "Member No")]
        public long MemberNo { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Member Name")]
        public string MemberName { get; set; }

        //[Required]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Application Date")]
        //public DateTime ApplicationDate { get; set; }

      

        //[Required]
        //[MaxLength(20)]
        //[Display(Name = "Salutation")]
        //public string Salutation { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "National ID")]
        public string NationalID { get; set; }

        [MaxLength(20)]
        [Display(Name = "Passport No")]
        public string PassportNo { get; set; }

        [MaxLength(20)]
        [Display(Name = "Driving Licence No")]
        public string DrivingLicenceNo { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [MaxLength(50)]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [MaxLength(50)]
        [Display(Name = "Occupation")]
        public string Occupation { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public DateTime DoB { get; set; }

        [MaxLength(50)]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }

        [MaxLength(256)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Present Address")]
        public string PresentAddress { get; set; }

        [MaxLength(256)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Nominee Name")]
        public string NomineeName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Nominee Relationship")]
        public string NomineeRelationship { get; set; }

        [MaxLength(50)]
        [Display(Name = "Gurdian Name")]
        public string GurdianName { get; set; }

        [MaxLength(20)]
        [Display(Name = "Gurdian National ID")]
        public string GurdianNationalID { get; set; }

        [MaxLength(20)]
        [Display(Name = "Gurdian Relationship")]
        public string GurdianRelationship { get; set; }

        //[Display(Name = "Member Image")]
        //public byte[] MemberImage { get; set; }

        //[Display(Name = "Nominee Image")]
        //public byte[] NomineeImage { get; set; }

        //[Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Membership Date")]
        public DateTime MembershipDate { get; set; }

        //[Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Membership Expire Date")]
        public DateTime MembershipExpireDate { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Security Amount Total")]
        public double SecurityAmountTotal { get; set; }

        [DefaultValue("")]
        [MaxLength(255)]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Inactive")]
        public bool Inactive { get; set; }

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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Inactive Date")]
        public DateTime InactiveDate { get; set; }
    }
}

