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
    public class EFEmployeeShiftRepo : IEmployeeShiftRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<EmployeeShift> EmployeeShift(string InstanceID)
        {
            return context.EmployeeShift.Include(e => e.Employee).Include(e => e.Shift).Where(e => e.InstanceID == InstanceID); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public EmployeeShift Single(string InstanceID, long ID)
        {
            return context.EmployeeShift.Include(e => e.Employee).Include(e => e.Shift).Where(e => e.InstanceID == InstanceID && e.EmployeeShiftID == ID && e.Employee.EmployeeStatus==0).FirstOrDefault(); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public IEnumerable<EmployeeShift> Search(string InstanceID, string txtSearch)
        {
            return context.EmployeeShift.Where(e => (e.Employee.PIN.Contains(txtSearch) || e.Employee.EmployeeName.Contains(txtSearch)) && e.InstanceID == InstanceID).OrderBy(e => e.WorkDate);
        }

        public void SaveEmployeeShift(EmployeeShift EmployeeShift)
        {
            if (EmployeeShift.EmployeeShiftID == 0)
            {
                context.EmployeeShift.Add(EmployeeShift);
            }
            else
            {
                EmployeeShift dbEntry = context.EmployeeShift.Find(EmployeeShift.EmployeeShiftID);
                var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
                if (dbEntry != null && dbEntry.WorkDate == wd)
                {
                    //dbEntry.EmployeeID = EmployeeShift.EmployeeID;
                    dbEntry.ShiftID = EmployeeShift.ShiftID;
                    dbEntry.FromDate = EmployeeShift.FromDate;
                    dbEntry.TillDate = EmployeeShift.TillDate;
                    dbEntry.Particulars = EmployeeShift.Particulars;
                    dbEntry.EntryBy = EmployeeShift.EntryBy;
                    dbEntry.WorkDate = wd;
                }
            }
                context.SaveChanges();
            //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=1 where EmployeeID={0}", EmployeeShift.EmployeeID);
        }

        public EmployeeShift DeleteEmployeeShift(long EmployeeShiftID)
        {
            EmployeeShift dbEntry = context.EmployeeShift.Find(EmployeeShiftID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;

            if (dbEntry != null && dbEntry.WorkDate == wd)
            {
                context.EmployeeShift.Remove(dbEntry);
                context.SaveChanges();
                //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=0 where EmployeeID={0}", dbEntry.EmployeeID);
            }
            return dbEntry;
        }

        public int DeleteStatus(long EmployeeShiftID)
        {
            EmployeeShift dbEntry = context.EmployeeShift.Find(EmployeeShiftID);
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
