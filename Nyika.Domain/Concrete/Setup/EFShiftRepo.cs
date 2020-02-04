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
    public class EFShiftRepo : IShiftRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Shift> Shift(string instanceId)
        {
            return context.Shift.Where(s=> s.InstanceID==instanceId); 
        }

        public Shift Single(string InstanceID, long ID)
        {
            return context.Shift.Where(a => a.InstanceID == InstanceID && a.ShiftID == ID).FirstOrDefault();
        }

        public void SaveShift(Shift Shift)
        {

            if (Shift.ShiftID == 0)
            {
                context.Shift.Add(Shift);
            }
            else
            {
                Shift dbEntry = context.Shift.Find(Shift.ShiftID);
                if (dbEntry != null)
                {
                    dbEntry.ShiftID = Shift.ShiftID;
                    dbEntry.ShiftName = Shift.ShiftName;

                    dbEntry.ShiftIn = Shift.ShiftIn;
                    dbEntry.ShiftOut = Shift.ShiftOut;
                    dbEntry.ShiftAbsent = Shift.ShiftAbsent;
                    dbEntry.ShiftLate = Shift.ShiftLate;
                    dbEntry.ShiftEarly = Shift.ShiftEarly;
                    dbEntry.ShiftLunchFrom = Shift.ShiftLunchFrom;
                    dbEntry.ShiftLunchTill = Shift.ShiftLunchTill;
                    dbEntry.ShiftLastPunch = Shift.ShiftLastPunch;
                    dbEntry.DefaultShift = Shift.DefaultShift;
                }
            }
            context.SaveChanges();
            if (Shift.DefaultShift == true)
            {
                context.Database.ExecuteSqlCommand("update shift set DefaultShift=0 where ShiftID<>{0}", Shift.ShiftID);
            }
        }

        public Shift DeleteShift(long ShiftID)
        {
            Shift dbEntry = context.Shift.Find(ShiftID);
            var count = context.EmployeeShift.Where(e => e.ShiftID == ShiftID).Count();

            if (dbEntry != null && count == 0)
            {
                context.Shift.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long ShiftID)
        {
            return context.EmployeeShift.Where(e => e.ShiftID == ShiftID).Count();

        }
    }
}
