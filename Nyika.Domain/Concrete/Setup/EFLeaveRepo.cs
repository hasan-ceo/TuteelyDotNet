using Nyika.Domain.Abstract.Setup;
using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.Setup
{
    public class EFLeaveRepo : ILeaveRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Leave> Leave(string instanceId)
        {
            return context.Leave.Where(l => l.InstanceID == instanceId); 
        }

        public Leave Single(string InstanceID, long ID)
        {
            return context.Leave.Where(a => a.InstanceID == InstanceID && a.LeaveID == ID).FirstOrDefault();
        }

        public void SaveLeave(Leave Leave)
        {

            if (Leave.LeaveID == 0)
            {
                context.Leave.Add(Leave);
            }
            else
            {
                Leave dbEntry = context.Leave.Find(Leave.LeaveID);
                if (dbEntry != null)
                {
                    dbEntry.LeaveName = Leave.LeaveName;
                    dbEntry.ShortCode = Leave.ShortCode;
                    dbEntry.YearlyLeave = Leave.YearlyLeave;
                }
            }
            context.SaveChanges();
        }

        public Leave DeleteLeave(long LeaveID)
        {
            Leave dbEntry = context.Leave.Find(LeaveID);
            var count = context.EmployeeLeave.Where(e => e.LeaveID == LeaveID).Count();
            if (dbEntry != null && count==0)
            {
                context.Leave.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long LeaveID)
        {
            return context.EmployeeLeave.Where(e => e.LeaveID == LeaveID).Count(); 

        }
    }
}
