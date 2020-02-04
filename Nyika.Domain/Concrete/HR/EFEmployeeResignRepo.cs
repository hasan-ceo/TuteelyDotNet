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
    public class EFEmployeeResignRepo : IEmployeeResignRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<EmployeeResign> EmployeeResign(string InstanceID)
        {
            return context.EmployeeResign.Include(e => e.Employee).Include(e => e.ResignReason).Where(e => e.InstanceID==InstanceID); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public EmployeeResign Single(string InstanceID, long ID)
        {
            return context.EmployeeResign.Include(e => e.Employee).Include(e => e.ResignReason).Where(e => e.InstanceID == InstanceID && e.EmployeeResignID == ID && e.Employee.EmployeeStatus == 1).FirstOrDefault(); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public IEnumerable<EmployeeResign> Search(string InstanceID, string txtSearch)
        {
            return context.EmployeeResign.Where(e => (e.Employee.PIN.Contains(txtSearch) || e.Employee.EmployeeName.Contains(txtSearch)) && e.InstanceID == InstanceID).OrderBy(e => e.WorkDate);
        }

        public void SaveEmployeeResign(EmployeeResign EmployeeResign)
        {
            if (EmployeeResign.EmployeeResignID == 0)
            {
                context.EmployeeResign.Add(EmployeeResign);
            }
            else
            {
                EmployeeResign dbEntry = context.EmployeeResign.Find(EmployeeResign.EmployeeResignID);
                var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
                if (dbEntry != null && dbEntry.WorkDate == wd)
                {
                    //dbEntry.EmployeeID = EmployeeResign.EmployeeID;
                    dbEntry.ResignReasonID = EmployeeResign.ResignReasonID;
                    //dbEntry.ResignDate = EmployeeResign.ResignDate;
                    dbEntry.Particulars = EmployeeResign.Particulars;
                    dbEntry.EntryBy = EmployeeResign.EntryBy;
                    dbEntry.WorkDate = wd;
                }
            }

            context.SaveChanges();
            context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=1 where EmployeeID={0}", EmployeeResign.EmployeeID);
        }

        public EmployeeResign DeleteEmployeeResign(long EmployeeResignID)
        {
            EmployeeResign dbEntry = context.EmployeeResign.Find(EmployeeResignID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;

            if (dbEntry != null && dbEntry.WorkDate == wd)
            {
                context.EmployeeResign.Remove(dbEntry);
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=0 where EmployeeID={0}", dbEntry.EmployeeID);
            }
            return dbEntry;
        }

        public int DeleteStatus(long EmployeeResignID)
        {
            EmployeeResign dbEntry = context.EmployeeResign.Find(EmployeeResignID);
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
