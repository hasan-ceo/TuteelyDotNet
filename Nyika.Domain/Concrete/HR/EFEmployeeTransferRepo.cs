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
    public class EFEmployeeTransferRepo : IEmployeeTransferRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<EmployeeTransfer> EmployeeTransfer(string InstanceID)
        {
            return context.EmployeeTransfer.Include(e => e.Employee).Include(e => e.Department).Include(e => e.Section).Include(e => e.Designation).Include(e => e.EmploymentType).Where(e => e.InstanceID == InstanceID); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public EmployeeTransfer Single(string InstanceID, long ID)
        {
            return context.EmployeeTransfer.Include(e => e.Employee).Include(e => e.Department).Include(e => e.Section).Include(e => e.Designation).Include(e => e.EmploymentType).Where(e => e.InstanceID == InstanceID && e.EmployeeTransferID == ID && e.Employee.EmployeeStatus==0).FirstOrDefault(); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public IEnumerable<EmployeeTransfer> Search(string InstanceID, string txtSearch)
        {
            return context.EmployeeTransfer.Include(e => e.Employee).Include(e => e.Department).Include(e => e.Section).Include(e => e.Designation).Include(e => e.EmploymentType).Where(e => (e.Employee.PIN.Contains(txtSearch) || e.Employee.EmployeeName.Contains(txtSearch)) && e.InstanceID == InstanceID).OrderBy(e => e.WorkDate);
        }

        public void SaveEmployeeTransfer(EmployeeTransfer EmployeeTransfer)
        {
            if (EmployeeTransfer.EmployeeTransferID == 0)
            {
                context.EmployeeTransfer.Add(EmployeeTransfer);
            }
            else
            {
                EmployeeTransfer dbEntry = context.EmployeeTransfer.Find(EmployeeTransfer.EmployeeTransferID);
                var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
                if (dbEntry != null && dbEntry.WorkDate == wd)
                {
                    //dbEntry.EmployeeID = EmployeeTransfer.EmployeeID;
                    dbEntry.EffectiveDate = EmployeeTransfer.EffectiveDate;
                    dbEntry.DepartmentID = EmployeeTransfer.DepartmentID;
                    dbEntry.SectionID = EmployeeTransfer.SectionID;
                    dbEntry.DesignationID = EmployeeTransfer.DesignationID;
                    dbEntry.EmploymentTypeID = EmployeeTransfer.EmploymentTypeID;
                    dbEntry.Particulars = EmployeeTransfer.Particulars;
                    dbEntry.EntryBy = EmployeeTransfer.EntryBy;
                    dbEntry.WorkDate = wd;
                }
            }
                context.SaveChanges();
            //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=1 where EmployeeID={0}", EmployeeTransfer.EmployeeID);
        }

        public EmployeeTransfer DeleteEmployeeTransfer(long EmployeeTransferID)
        {
            EmployeeTransfer dbEntry = context.EmployeeTransfer.Find(EmployeeTransferID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;

            if (dbEntry != null && dbEntry.WorkDate == wd)
            {
                context.EmployeeTransfer.Remove(dbEntry);
                context.SaveChanges();
                //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=0 where EmployeeID={0}", dbEntry.EmployeeID);
            }
            return dbEntry;
        }

        public int DeleteStatus(long EmployeeTransferID)
        {
            EmployeeTransfer dbEntry = context.EmployeeTransfer.Find(EmployeeTransferID);
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
