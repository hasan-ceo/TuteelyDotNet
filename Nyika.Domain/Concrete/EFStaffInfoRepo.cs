using HRMSMvc.Domain.Abstract;
using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Concrete
{
    public class EFStaffInfoRepo : IStaffInfoRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<StaffInfo> StaffInfo
        {
            get { return context.StaffInfo; }
        }

        public void SaveStaffInfo(StaffInfo StaffInfo)
        {

            if (StaffInfo.StaffInfoID == 0)
            {
                context.StaffInfo.Add(StaffInfo);
            }
            else
            {
                StaffInfo dbEntry = context.StaffInfo.Find(StaffInfo.StaffInfoID);
                if (dbEntry != null)
                {
                   
                    dbEntry.PIN = StaffInfo.PIN;
                    dbEntry.CompanyID = StaffInfo.CompanyID;
                    dbEntry.DivisionID = StaffInfo.DivisionID;
                    dbEntry.RegionID = StaffInfo.RegionID;
                    dbEntry.AreaID = StaffInfo.AreaID;
                    dbEntry.BranchID = StaffInfo.BranchID;
                    dbEntry.StaffTypeID = StaffInfo.StaffTypeID;
                    dbEntry.StaffName = StaffInfo.StaffName;
                    dbEntry.MotherName = StaffInfo.MotherName ;
                    dbEntry.SpouseName = StaffInfo.SpouseName;
                    dbEntry.DoB = StaffInfo.DoB;
                    dbEntry.Gender = StaffInfo.Gender;
                    dbEntry.Address = StaffInfo.Address;
                    dbEntry.HomeCountryID = StaffInfo.HomeCountryID;
                    dbEntry.NationalIdNumber = StaffInfo.NationalIdNumber;
                    dbEntry.PassportNumber = StaffInfo.PassportNumber;
                    dbEntry.PrimaryContact = StaffInfo.PrimaryContact;
                    dbEntry.SecondaryContact = StaffInfo.SecondaryContact;
                    dbEntry.Religion = StaffInfo.Religion;
                    dbEntry.MaritalStatus = StaffInfo.MaritalStatus;
                    dbEntry.BloodGroup = StaffInfo.BloodGroup;
                    dbEntry.JoiningDate = StaffInfo.JoiningDate;
                    dbEntry.Reference01 = StaffInfo.Reference01;
                    dbEntry.Reference02 = StaffInfo.Reference02;
                    dbEntry.DesignationID = StaffInfo.DesignationID;
                    dbEntry.UsedDesignation = StaffInfo.UsedDesignation;
                    dbEntry.WorkStationType = StaffInfo.WorkStationType;
                    dbEntry.EducationID = StaffInfo.EducationID;
                    dbEntry.ProgramId = StaffInfo.ProgramId;
                    dbEntry.ContractStartDate = StaffInfo.ContractStartDate;
                    dbEntry.ContractEndDate = StaffInfo.ContractEndDate;
                    dbEntry.WorkPermitEndDate = StaffInfo.WorkPermitEndDate;
                    dbEntry.VisaType = StaffInfo.VisaType;
                    dbEntry.VisaExpiryDate = StaffInfo.VisaExpiryDate;
                    dbEntry.ProbationEnd = false ;
                    dbEntry.EmployeeStatus = StaffInfo.EmployeeStatus;
                    dbEntry.InactiveReason = StaffInfo.InactiveReason;
                    dbEntry.InactiveReasonDate = StaffInfo.InactiveReasonDate;
                    dbEntry.SupervisorPIN = StaffInfo.SupervisorPIN;
                    dbEntry.SalaryGradeID = StaffInfo.SalaryGradeID;
                    dbEntry.SalarySlab = StaffInfo.SalarySlab;
                    dbEntry.GrossSalary = StaffInfo.GrossSalary;
                    dbEntry.OthersAllowance = StaffInfo.OthersAllowance;
                    dbEntry.LunchAllowance = StaffInfo.LunchAllowance;
                    dbEntry.MFAllowance = StaffInfo.MFAllowance;
                    dbEntry.ChargeAllowance = StaffInfo.ChargeAllowance;
                    dbEntry.TaxDeduction = StaffInfo.TaxDeduction;
                    dbEntry.PPFNSSF = StaffInfo.PPFNSSF;
                    dbEntry.SDL = StaffInfo.SDL;

                    dbEntry.FormFourIndexNumber = StaffInfo.FormFourIndexNumber;
                    dbEntry.FullNameUsedDuringStudy = StaffInfo.FullNameUsedDuringStudy;
                    dbEntry.MobileNumber = StaffInfo.MobileNumber;
                    dbEntry.CurrentNameIfChanged = StaffInfo.CurrentNameIfChanged;
                    dbEntry.YearOfCompletion = StaffInfo.YearOfCompletion;
                    dbEntry.NameOfInstitution = StaffInfo.NameOfInstitution;

                }
            }
            context.SaveChanges();
        }

        public StaffInfo DeleteStaffInfo(int StaffInfoID)
        {
            StaffInfo dbEntry = context.StaffInfo.Find(StaffInfoID);
            if (dbEntry != null)
            {
                context.StaffInfo.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
