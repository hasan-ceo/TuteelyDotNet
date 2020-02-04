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
    public class EFHolidayRepo : IHolidayRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Holiday> Holiday(string instanceId)
        {
            return context.Holiday.Where(h => h.InstanceID == instanceId); 
        }

        public Holiday Single(string InstanceID, long ID)
        {
            return context.Holiday.Where(a => a.InstanceID == InstanceID && a.HolidayID == ID).FirstOrDefault();
        }

        public void SaveHoliday(Holiday Holiday)
        {

            if (Holiday.HolidayID == 0)
            {
                context.Holiday.Add(Holiday);
            }
            else
            {
                Holiday dbEntry = context.Holiday.Find(Holiday.HolidayID);
                if (dbEntry != null)
                {
                    dbEntry.HolidayID = Holiday.HolidayID;
                    dbEntry.HolidayName = Holiday.HolidayName;
                    dbEntry.FromDate = Holiday.FromDate;
                    dbEntry.TillDate = Holiday.TillDate;

                }
            }
            context.SaveChanges();
        }

        public Holiday DeleteHoliday(long HolidayID)
        {
            Holiday dbEntry = context.Holiday.Find(HolidayID);
            if (dbEntry != null)
            {
                context.Holiday.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long HolidayID)
        {
            return 0;

        }
    }
}
