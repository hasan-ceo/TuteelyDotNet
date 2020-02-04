using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.Accounts
{
    public class EFBusinessDayRepo : IBusinessDayRepo
    {
        private EFDbContext context = new EFDbContext();


        public IEnumerable<BusinessDay> BusinessDay(string InstanceID)
        {
            return context.BusinessDay.Where(b => b.InstanceID == InstanceID);
        }

        public BusinessDay Single(string InstanceID, long ID)
        {
            return context.BusinessDay.Where(a => a.InstanceID == InstanceID && a.BusinessDayID == ID).FirstOrDefault();
        }

        public DateTime WorkDate(string InstanceID)
        {
            return context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == InstanceID).FirstOrDefault().WorkDate;
        }

        public void DayClose(string InstanceID)
        {
            context.Database.ExecuteSqlCommand("exec pAccDayClose @InstanceID={0}", InstanceID);
        }

        public void DayOpen(string InstanceID, string entryby)
        {
            context.Database.ExecuteSqlCommand("exec pAccDayOpen @InstanceID={0},@EntryBy={1}", InstanceID, entryby);
        }

        public void MonthClose(string InstanceID)
        {
            context.Database.ExecuteSqlCommand("exec pAccMonthClose @InstanceID={0}", InstanceID);
        }

        public void SaveBusinessDay(BusinessDay BusinessDay)
        {

            if (BusinessDay.BusinessDayID == 0)
            {
                context.BusinessDay.Add(BusinessDay);
            }
            else
            {
                BusinessDay dbEntry = context.BusinessDay.Find(BusinessDay.BusinessDayID);
                if (dbEntry != null)
                {
                    //dbEntry.BusinessDayID = BusinessDay.BusinessDayID;
                    dbEntry.WorkDate = BusinessDay.WorkDate;
                    dbEntry.DayClose = BusinessDay.DayClose;
                }
            }
            context.SaveChanges();
        }

        public BusinessDay DeleteBusinessDay(long BusinessDayID)
        {
            BusinessDay dbEntry = context.BusinessDay.Find(BusinessDayID);
            if (dbEntry != null)
            {
                context.BusinessDay.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
