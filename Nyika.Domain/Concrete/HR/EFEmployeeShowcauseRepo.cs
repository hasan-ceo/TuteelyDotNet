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
    public class EFEmployeeShowcauseRepo : IEmployeeShowcauseRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<EmployeeShowcause> EmployeeShowcause(string InstanceID)
        {
            return context.EmployeeShowcause.Include(e => e.Employee).Where(e => e.InstanceID == InstanceID); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public EmployeeShowcause Single(string InstanceID, long ID)
        {
            return context.EmployeeShowcause.Include(e => e.Employee).Where(e => e.InstanceID == InstanceID && e.EmployeeShowcauseID == ID && e.Employee.EmployeeStatus == 0).FirstOrDefault(); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public IEnumerable<EmployeeShowcause> Search(string InstanceID, string txtSearch)
        {
            return context.EmployeeShowcause.Where(e => (e.Employee.PIN.Contains(txtSearch) || e.Employee.EmployeeName.Contains(txtSearch)) && e.InstanceID == InstanceID).OrderBy(e => e.WorkDate);
        }

        public void SaveEmployeeShowcause(EmployeeShowcause EmployeeShowcause)
        {
            if (EmployeeShowcause.EmployeeShowcauseID == 0)
            {
                context.EmployeeShowcause.Add(EmployeeShowcause);
            }
            else
            {
                EmployeeShowcause dbEntry = context.EmployeeShowcause.Find(EmployeeShowcause.EmployeeShowcauseID);
                var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
                if (dbEntry != null && dbEntry.WorkDate == wd)
                {
                    dbEntry.EmployeeShowcauseID = EmployeeShowcause.EmployeeShowcauseID;
                    dbEntry.EmployeeID = EmployeeShowcause.EmployeeID;
                    dbEntry.Subject = EmployeeShowcause.Subject;
                    dbEntry.Details = EmployeeShowcause.Details;
                    dbEntry.ActionTaken = EmployeeShowcause.ActionTaken;
                    dbEntry.EntryBy = EmployeeShowcause.EntryBy;
                    dbEntry.WorkDate = wd;
                }
            }

            context.SaveChanges();
            //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=1 where EmployeeID={0}", EmployeeShowcause.EmployeeID);
        }

        public EmployeeShowcause DeleteEmployeeShowcause(long EmployeeShowcauseID)
        {
            EmployeeShowcause dbEntry = context.EmployeeShowcause.Find(EmployeeShowcauseID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
            if (dbEntry != null && dbEntry.WorkDate == wd)
            {
                context.EmployeeShowcause.Remove(dbEntry);
                context.SaveChanges();
                //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=0 where EmployeeID={0}", dbEntry.EmployeeID);
            }
            return dbEntry;
        }

        public int DeleteStatus(long EmployeeShowcauseID)
        {
            EmployeeShowcause dbEntry = context.EmployeeShowcause.Find(EmployeeShowcauseID);
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
