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
    public class EFEmployeeIncrementRepo : IEmployeeIncrementRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<EmployeeIncrement> EmployeeIncrement(string InstanceID)
        {
            return context.EmployeeIncrement.Include(e => e.Employee).Where(e => e.InstanceID == InstanceID); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public EmployeeIncrement Single(string InstanceID, long ID)
        {
            return context.EmployeeIncrement.Include(e => e.Employee).Where(e => e.InstanceID == InstanceID && e.EmployeeIncrementID == ID && e.Employee.EmployeeStatus==0).FirstOrDefault(); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public IEnumerable<EmployeeIncrement> Search(string InstanceID, string txtSearch)
        {
            return context.EmployeeIncrement.Where(e => (e.Employee.PIN.Contains(txtSearch) || e.Employee.EmployeeName.Contains(txtSearch)) && e.InstanceID == InstanceID).OrderBy(e => e.WorkDate);
        }

        public void SaveEmployeeIncrement(EmployeeIncrement EmployeeIncrement)
        {
            if (EmployeeIncrement.EmployeeIncrementID == 0)
            {
                context.EmployeeIncrement.Add(EmployeeIncrement);
            }
            else
            {
                EmployeeIncrement dbEntry = context.EmployeeIncrement.Find(EmployeeIncrement.EmployeeIncrementID);
                var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
                if (dbEntry != null && dbEntry.WorkDate == wd)
                {
                    //dbEntry.EmployeeID = EmployeeIncrement.EmployeeID;
                    dbEntry.EffectiveDate = EmployeeIncrement.EffectiveDate;
                    dbEntry.BasicSalary = EmployeeIncrement.BasicSalary;
                    dbEntry.OtherBenefits = EmployeeIncrement.OtherBenefits;
                    dbEntry.GrossSalary = EmployeeIncrement.GrossSalary;
                    dbEntry.LunchAllowance = EmployeeIncrement.LunchAllowance;
                    dbEntry.ProfessionalAllowance = EmployeeIncrement.ProfessionalAllowance;
                    dbEntry.Particulars = EmployeeIncrement.Particulars;
                    dbEntry.EntryBy = EmployeeIncrement.EntryBy;
                    dbEntry.WorkDate = wd;
                }
            }
                context.SaveChanges();
            //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=1 where EmployeeID={0}", EmployeeIncrement.EmployeeID);
        }

        public EmployeeIncrement DeleteEmployeeIncrement(long EmployeeIncrementID)
        {
            EmployeeIncrement dbEntry = context.EmployeeIncrement.Find(EmployeeIncrementID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;

            if (dbEntry != null && dbEntry.WorkDate == wd)
            {
                context.EmployeeIncrement.Remove(dbEntry);
                context.SaveChanges();
                //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=0 where EmployeeID={0}", dbEntry.EmployeeID);
            }
            return dbEntry;
        }

        public int DeleteStatus(long EmployeeIncrementID)
        {
            EmployeeIncrement dbEntry = context.EmployeeIncrement.Find(EmployeeIncrementID);
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

    }
}
