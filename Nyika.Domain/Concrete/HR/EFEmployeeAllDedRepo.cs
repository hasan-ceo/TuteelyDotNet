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
    public class EFEmployeeAllDedRepo : IEmployeeAllDedRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<EmployeeAllDed> EmployeeAllDed(string InstanceID)
        {
            return context.EmployeeAllDed.Include(e => e.Employee).Include(e => e.AllDed).Where(e => e.InstanceID == InstanceID); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public EmployeeAllDed Single(string InstanceID, long ID)
        {
            return context.EmployeeAllDed.Include(e => e.Employee).Include(e => e.AllDed).Where(e => e.InstanceID == InstanceID && e.EmployeeAllDedID == ID && e.Employee.EmployeeStatus==0).FirstOrDefault(); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public IEnumerable<EmployeeAllDed> Search(string InstanceID, string txtSearch)
        {
            return context.EmployeeAllDed.Where(e => (e.Employee.PIN.Contains(txtSearch) || e.Employee.EmployeeName.Contains(txtSearch)) && e.InstanceID == InstanceID).OrderBy(e => e.WorkDate);
        }

        public void SaveEmployeeAllDed(EmployeeAllDed EmployeeAllDed)
        {
            if (EmployeeAllDed.EmployeeAllDedID == 0)
            {
                context.EmployeeAllDed.Add(EmployeeAllDed);
            }
            else
            {
                EmployeeAllDed dbEntry = context.EmployeeAllDed.Find(EmployeeAllDed.EmployeeAllDedID);
                var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
                if (dbEntry != null && dbEntry.WorkDate == wd)
                {
                    //dbEntry.EmployeeID = EmployeeAllDed.EmployeeID;
                    dbEntry.AllDedID = EmployeeAllDed.AllDedID;
                    dbEntry.EffectiveDate = EmployeeAllDed.EffectiveDate;
                    dbEntry.Amount = EmployeeAllDed.Amount;
                    dbEntry.Particulars = EmployeeAllDed.Particulars;
                    dbEntry.EntryBy = EmployeeAllDed.EntryBy;
                    dbEntry.WorkDate = wd;
                }
            }
                context.SaveChanges();
            //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=1 where EmployeeID={0}", EmployeeAllDed.EmployeeID);
        }

        public EmployeeAllDed DeleteEmployeeAllDed(long EmployeeAllDedID)
        {
            EmployeeAllDed dbEntry = context.EmployeeAllDed.Find(EmployeeAllDedID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;

            if (dbEntry != null && dbEntry.WorkDate == wd)
            {
                context.EmployeeAllDed.Remove(dbEntry);
                context.SaveChanges();
                //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=0 where EmployeeID={0}", dbEntry.EmployeeID);
            }
            return dbEntry;
        }

        public int DeleteStatus(long EmployeeAllDedID)
        {
            EmployeeAllDed dbEntry = context.EmployeeAllDed.Find(EmployeeAllDedID);
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
