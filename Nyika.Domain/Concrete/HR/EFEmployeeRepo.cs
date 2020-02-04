using Nyika.Domain.Abstract.HR;
using Nyika.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.HR
{
    public class EFEmployeeRepo : IEmployeeRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Employee> Employee(string InstanceID)
        {
            return context.Employee.Include(e => e.Company).Include(e => e.Designation).Include(e => e.EmploymentType).Include(e => e.Section).Where(e => e.InstanceID == InstanceID);
        }

        public IEnumerable<Employee> EmployeeActive(string InstanceID)
        {
            return context.Employee.Include(e => e.Company).Include(e => e.Designation).Include(e => e.EmploymentType).Include(e => e.Section).Where(e => e.InstanceID == InstanceID && e.EmployeeStatus == 0);
        }

        public IEnumerable<Employee> Search(string InstanceID, string txtSearch)
        {
            return context.Employee.Where(e => (e.PIN.Contains(txtSearch) || e.EmployeeName.Contains(txtSearch)) && e.InstanceID == InstanceID && e.EmployeeStatus == 0).OrderBy(e => e.PIN);
        }


        public Employee Single(string InstanceID, long ID)
        {
            return context.Employee.Include(e => e.Company).Include(e => e.Designation).Include(e => e.EmploymentType).Include(e => e.Section).Where(e => e.InstanceID == InstanceID && e.EmployeeID == ID && e.EmployeeStatus == 0).FirstOrDefault();
        }

        public void SaveEmployee(Employee Employee)
        {
            if (Employee.EmployeeID == 0)
            {
                context.Employee.Add(Employee);
            }
            else
            {
                Employee dbEntry = context.Employee.Find(Employee.EmployeeID);
                if (dbEntry != null)
                {
                    var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
                    dbEntry.PIN = Employee.PIN;
                    dbEntry.EmployeeName = Employee.EmployeeName;
                    dbEntry.CompanyID = Employee.CompanyID;
                    dbEntry.DepartmentID = Employee.DepartmentID;
                    dbEntry.SectionID = Employee.SectionID;
                    dbEntry.DesignationID = Employee.DesignationID;
                    dbEntry.EmploymentTypeID = Employee.EmploymentTypeID;
                    dbEntry.EducationID = Employee.EducationID;
                    dbEntry.JoiningDate = Employee.JoiningDate;

                    dbEntry.BasicSalary = Employee.BasicSalary;
                    dbEntry.OtherBenefits = Employee.OtherBenefits;
                    dbEntry.GrossSalary = Employee.GrossSalary;
                    dbEntry.LunchAllowance = Employee.LunchAllowance;
                    dbEntry.ProfessionalAllowance = Employee.ProfessionalAllowance;

                    dbEntry.PPFNSSF = Employee.PPFNSSF;
                    dbEntry.PPFNSSFNumber = Employee.PPFNSSFNumber;
                    dbEntry.BankAccountNumber = Employee.BankAccountNumber;
                    dbEntry.BankName = Employee.BankName;
                    dbEntry.IndexNumber = Employee.IndexNumber;

                    dbEntry.ContractStartDate = Employee.ContractStartDate;
                    dbEntry.ContractEndDate = Employee.ContractEndDate;
                    dbEntry.ContactNumber = Employee.ContactNumber;
                    dbEntry.Email = Employee.Email;

                    dbEntry.DoB = Employee.DoB;
                    dbEntry.Gender = Employee.Gender;
                    dbEntry.MotherName = Employee.MotherName;
                    dbEntry.FatherName = Employee.FatherName;
                    dbEntry.Address = Employee.Address;
                    dbEntry.Religion = Employee.Religion;
                    dbEntry.MaritalStatus = Employee.MaritalStatus;
                    dbEntry.BloodGroup = Employee.BloodGroup;
                    dbEntry.CardNumber = Employee.CardNumber;

                    if (Employee.ImageUrl != null)
                    {
                        dbEntry.ImageUrl = Employee.ImageUrl;
                    }

                    dbEntry.EntryBy = Employee.EntryBy;
                    dbEntry.WorkDate = wd;
                }
            }

            context.SaveChanges();
            //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=1 where EmployeeID={0}", Employee.EmployeeID);
        }

        public Employee DeleteEmployee(long EmployeeID)
        {
            Employee dbEntry = context.Employee.Find(EmployeeID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;

            if (dbEntry != null && dbEntry.WorkDate == wd)
            {
                context.Employee.Remove(dbEntry);
                context.SaveChanges();
               // context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=0 where EmployeeID={0}", dbEntry.EmployeeID);
            }
            return dbEntry;
        }

        public int DeleteStatus(long EmployeeID)
        {
            Employee dbEntry = context.Employee.Find(EmployeeID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
            if (dbEntry != null && dbEntry.WorkDate == wd)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public bool isEmailExists(string InstanceID,string email)
        {
            var count = context.Employee.Where(e => e.Email== email && e.InstanceID==InstanceID).Count();
            if (count==0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
