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
    public class EFEmployeeLeaveRepo : IEmployeeLeaveRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<EmployeeLeave> EmployeeLeave(string InstanceID)
        {
            return context.EmployeeLeave.Include(e => e.Employee).Include(e => e.Leave).Where(e => e.InstanceID == InstanceID); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public EmployeeLeave Single(string InstanceID, long ID)
        {
            return context.EmployeeLeave.Include(e => e.Employee).Include(e => e.Leave).Where(e => e.InstanceID == InstanceID && e.EmployeeLeaveID == ID && e.Employee.EmployeeStatus == 0).FirstOrDefault(); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public IEnumerable<EmployeeLeave> Search(string InstanceID, string txtSearch)
        {
            return context.EmployeeLeave.Where(e => (e.Employee.PIN.Contains(txtSearch) || e.Employee.EmployeeName.Contains(txtSearch)) && e.InstanceID == InstanceID).OrderBy(e => e.WorkDate);
        }

        public void SaveEmployeeLeave(EmployeeLeave EmployeeLeave)
        {
            if (EmployeeLeave.EmployeeLeaveID == 0)
            {
                context.EmployeeLeave.Add(EmployeeLeave);
            }
            else
            {
                EmployeeLeave dbEntry = context.EmployeeLeave.Find(EmployeeLeave.EmployeeLeaveID);
                var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
                if (dbEntry != null && dbEntry.WorkDate == wd)
                {
                    //dbEntry.EmployeeID = EmployeeLeave.EmployeeID;
                    dbEntry.LeaveID = EmployeeLeave.LeaveID;
                    dbEntry.FromDate = EmployeeLeave.FromDate;
                    dbEntry.TillDate = EmployeeLeave.TillDate;
                    dbEntry.ApplicationDate = EmployeeLeave.ApplicationDate;
                    dbEntry.Withoutpay = EmployeeLeave.Withoutpay;
                    dbEntry.Particulars = EmployeeLeave.Particulars;
                    dbEntry.EntryBy = EmployeeLeave.EntryBy;
                    dbEntry.WorkDate = wd;
                }
            }

            context.SaveChanges();
            //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=1 where EmployeeID={0}", EmployeeLeave.EmployeeID);
        }

        public EmployeeLeave DeleteEmployeeLeave(long EmployeeLeaveID)
        {
            EmployeeLeave dbEntry = context.EmployeeLeave.Find(EmployeeLeaveID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;

            if (dbEntry != null && dbEntry.WorkDate == wd)
            {
                context.EmployeeLeave.Remove(dbEntry);
                context.SaveChanges();
                //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=0 where EmployeeID={0}", dbEntry.EmployeeID);
            }
            return dbEntry;
        }

        public int DeleteStatus(long EmployeeLeaveID)
        {
            EmployeeLeave dbEntry = context.EmployeeLeave.Find(EmployeeLeaveID);
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
