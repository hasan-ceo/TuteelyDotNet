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
    public class EFEmployeeTourRepo : IEmployeeTourRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<EmployeeTour> EmployeeTour(string InstanceID)
        {
            return context.EmployeeTour.Include(e => e.Employee).Where(e => e.InstanceID == InstanceID); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public EmployeeTour Single(string InstanceID, long ID)
        {
            return context.EmployeeTour.Include(e => e.Employee).Where(e => e.InstanceID == InstanceID && e.EmployeeTourID == ID && e.Employee.EmployeeStatus == 0).FirstOrDefault(); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }


        public IEnumerable<EmployeeTour> Search(string InstanceID, string txtSearch)
        {
            return context.EmployeeTour.Where(e => (e.Employee.PIN.Contains(txtSearch) || e.Employee.EmployeeName.Contains(txtSearch)) && e.InstanceID == InstanceID).OrderBy(e => e.WorkDate);
        }

        public void SaveEmployeeTour(EmployeeTour EmployeeTour)
        {
            if (EmployeeTour.EmployeeTourID == 0)
            {
                context.EmployeeTour.Add(EmployeeTour);
            }
            else
            {
                EmployeeTour dbEntry = context.EmployeeTour.Find(EmployeeTour.EmployeeTourID);
                var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
                if (dbEntry != null && dbEntry.WorkDate == wd)
                {
                    //dbEntry.EmployeeID = EmployeeTour.EmployeeID;
                    dbEntry.TourType = EmployeeTour.TourType;
                    dbEntry.FromDate = EmployeeTour.FromDate;
                    dbEntry.TillDate = EmployeeTour.TillDate;
                    dbEntry.ApplicationDate = EmployeeTour.ApplicationDate;
                    dbEntry.Particulars = EmployeeTour.Particulars;
                    dbEntry.EntryBy = EmployeeTour.EntryBy;
                    dbEntry.WorkDate = wd;
                }
            }

            context.SaveChanges();
            //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=1 where EmployeeID={0}", EmployeeTour.EmployeeID);
        }

        public EmployeeTour DeleteEmployeeTour(long EmployeeTourID)
        {
            EmployeeTour dbEntry = context.EmployeeTour.Find(EmployeeTourID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;

            if (dbEntry != null && dbEntry.WorkDate == wd)
            {
                context.EmployeeTour.Remove(dbEntry);
                context.SaveChanges();
                //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=0 where EmployeeID={0}", dbEntry.EmployeeID);
            }
            return dbEntry;
        }

        public int DeleteStatus(long EmployeeTourID)
        {
            EmployeeTour dbEntry = context.EmployeeTour.Find(EmployeeTourID);
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
