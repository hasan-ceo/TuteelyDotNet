using Nyika.Domain.Abstract.MF;
using Nyika.Domain.Entities.MF;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.MF
{
    public class EFLoanCycleRepo : ILoanCycleRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<LoanCycle> LoanCycle(string InstanceID)
        {
             return context.LoanCycle.Where(b => b.InstanceID==InstanceID); 
        }

        public LoanCycle Single(string InstanceID, long ID)
        {
            return context.LoanCycle.Where(a => a.InstanceID == InstanceID && a.LoanCycleID == ID).FirstOrDefault();
        }

        public void SaveLoanCycle(LoanCycle LoanCycle)
        {

            if (LoanCycle.LoanCycleID == 0)
            {
                context.LoanCycle.Add(LoanCycle);
            }
            else
            {
                LoanCycle dbEntry = context.LoanCycle.Find(LoanCycle.LoanCycleID);
                if (dbEntry != null)
                {
                    //dbEntry.LoanCycleID = LoanCycle.LoanCycleID;
                    //dbEntry.ProjectID = LoanCycle.ProjectID;
                    dbEntry.LoanCycleNo = LoanCycle.LoanCycleNo;
                    dbEntry.MinLimit = LoanCycle.MinLimit;
                    dbEntry.MaxLimit = LoanCycle.MaxLimit;
                }
            }
            context.SaveChanges();
        }

        public LoanCycle DeleteLoanCycle(long LoanCycleID)
        {
            LoanCycle dbEntry = context.LoanCycle.Find(LoanCycleID);
            if (dbEntry != null)
            {
                var count = context.LoanCycle.Where(e => e.ProjectID == dbEntry.ProjectID && e.InstanceID == dbEntry.InstanceID).Count();
                if (dbEntry != null && count > 1)
                {
                    context.LoanCycle.Remove(dbEntry);
                    context.SaveChanges();
                }
            }
            return dbEntry;
        }

        public bool CreateNew(string InstanceID,long ProjectID)
        {
            if (context.LoanCycle.Where(e => e.ProjectID == ProjectID && e.InstanceID == InstanceID).Count() < 5)
                return true;
            else
                return false;

        }

        public int IsExists(string InstanceID, long ProjectID)
        {
            var count = context.LoanCycle.Where(e => e.ProjectID == ProjectID && e.InstanceID == InstanceID).Count();
            return count;
        }
    }
}
