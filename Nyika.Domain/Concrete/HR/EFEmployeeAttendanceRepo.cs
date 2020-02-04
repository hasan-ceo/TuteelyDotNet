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
    public class EFEmployeeAttendanceRepo : IEmployeeAttendanceRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<EmployeeAttendance> EmployeeAttendance(string InstanceID)
        {
            return context.EmployeeAttendance.Include(e => e.Employee).Include(e => e.AttenStatus).Where(e => e.InstanceID == InstanceID); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public EmployeeAttendance Single(string InstanceID, long ID)
        {
            return context.EmployeeAttendance.Include(e => e.Employee).Include(e => e.AttenStatus).Where(e => e.InstanceID == InstanceID && e.EmployeeAttendanceID == ID && e.Employee.EmployeeStatus == 0).FirstOrDefault(); //.Include(e => e.Employee.de .Designation).Include(e => e.EmploymentType).Include(e => e.Section);  }
        }

        public IEnumerable<EmployeeAttendance> Search(string InstanceID, string txtSearch)
        {
            return context.EmployeeAttendance.Where(e => (e.Employee.PIN.Contains(txtSearch) || e.Employee.EmployeeName.Contains(txtSearch)) && e.InstanceID == InstanceID).OrderBy(e => e.WorkDate);
        }

        public void SaveEmployeeAttendance(long EmployeeID, long AttenStatusID, string Particulars, DateTime InTime,DateTime OutTime, DateTime FromDate,DateTime TillDate,string EntryBy, string instanceId)
        {
            context.Database.ExecuteSqlCommand("exec pHRManualAttendance @EmployeeID={0},@AttenStatusID={1},@Particulars={2},@InTime ={3},@OutTime={4},@fromDate={5},@tillDate={6},@EntryBy={7},@InstanceID={8}", EmployeeID, AttenStatusID, Particulars,  InTime,  OutTime,  FromDate,  TillDate,  EntryBy,  instanceId);
        }

        public EmployeeAttendance DeleteEmployeeAttendance(long EmployeeAttendanceID)
        {
            EmployeeAttendance dbEntry = context.EmployeeAttendance.Find(EmployeeAttendanceID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;

            if (dbEntry != null && dbEntry.WorkDate == wd)
            {
                context.EmployeeAttendance.Remove(dbEntry);
                context.SaveChanges();
                //context.Database.ExecuteSqlCommand("update Employee set EmployeeStatus=0 where EmployeeID={0}", dbEntry.EmployeeID);
            }
            return dbEntry;
        }

        public int DeleteStatus(long EmployeeAttendanceID)
        {
            EmployeeAttendance dbEntry = context.EmployeeAttendance.Find(EmployeeAttendanceID);
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

        public void ProcessAttendance(string InstanceID)
        {
            context.Database.ExecuteSqlCommand("exec pHRProcessAttendance @InstanceID={0}", InstanceID);
        }


        public void ProcessSalary(string InstanceID)
        {
            context.Database.ExecuteSqlCommand("exec pHRProcessSalary @InstanceID={0}", InstanceID);
        }























    }
}
