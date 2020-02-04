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
    public class EFEmployeePayrollRepo : IEmployeePayrollRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<EmployeePayroll> EmployeePayroll(string InstanceID)
        {
            return context.EmployeePayroll.Include(e => e.Employee).Where(e => e.InstanceID == InstanceID); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public EmployeePayroll Single(string InstanceID, long ID)
        {
            return context.EmployeePayroll.Include(e => e.Employee).Where(e => e.InstanceID == InstanceID && e.EmployeePayrollID == ID && e.Employee.EmployeeStatus == 0).FirstOrDefault(); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }


        public IEnumerable<EmployeePayroll> Search(string InstanceID, string txtSearch)
        {
            return context.EmployeePayroll.Where(e => (e.Employee.PIN.Contains(txtSearch) || e.Employee.EmployeeName.Contains(txtSearch)) && e.InstanceID == InstanceID).OrderBy(e => e.WorkDate);
        }

        public void SaveEmployeePayroll(EmployeePayroll EmployeePayroll)
        {
            if (EmployeePayroll.EmployeePayrollID == 0)
            {
                context.EmployeePayroll.Add(EmployeePayroll);
            }
            else
            {
                EmployeePayroll dbEntry = context.EmployeePayroll.Find(EmployeePayroll.EmployeePayrollID);
                var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
                if (dbEntry != null && dbEntry.WorkDate == wd)
                {
                    //dbEntry.EmployeeID = EmployeePayroll.EmployeeID;                
                    dbEntry.WorkDate = EmployeePayroll.WorkDate;
                    dbEntry.EntryBy = EmployeePayroll.EntryBy;
                    dbEntry.WorkDate = wd;
                }
            }

            context.SaveChanges();
            //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=1 where EmployeeID={0}", EmployeePayroll.EmployeeID);
        }

        public EmployeePayroll DeleteEmployeePayroll(long EmployeePayrollID)
        {
            EmployeePayroll dbEntry = context.EmployeePayroll.Find(EmployeePayrollID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;

            if (dbEntry != null && dbEntry.WorkDate == wd)
            {
                context.EmployeePayroll.Remove(dbEntry);
                context.SaveChanges();
                //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=0 where EmployeeID={0}", dbEntry.EmployeeID);
            }
            return dbEntry;
        }

        public int DeleteStatus(long EmployeePayrollID)
        {
            EmployeePayroll dbEntry = context.EmployeePayroll.Find(EmployeePayrollID);
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
