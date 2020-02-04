using Nyika.Domain.Abstract.MF;
using Nyika.Domain.Entities.MF;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.MF
{
    public class EFLoanScheduleRepo : ILoanScheduleRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<LoanSchedule> LoanSchedule(string InstanceID)
        {
             return context.LoanSchedule.Where(b => b.InstanceID==InstanceID); 
        }

        public LoanSchedule Single(string InstanceID, long ID)
        {
            return context.LoanSchedule.Where(a => a.InstanceID == InstanceID && a.LoanScheduleID == ID).FirstOrDefault();
        }

        public void SaveLoanSchedule(LoanSchedule LoanSchedule)
        {

            if (LoanSchedule.LoanScheduleID == 0)
            {
                context.LoanSchedule.Add(LoanSchedule);
            }
            else
            {
                LoanSchedule dbEntry = context.LoanSchedule.Find(LoanSchedule.LoanScheduleID);
                if (dbEntry != null)
                {
                    //dbEntry.LoanScheduleID = LoanSchedule.LoanScheduleID;
                    dbEntry.LoanID = LoanSchedule.LoanID;
                    dbEntry.InstallmentNo = LoanSchedule.InstallmentNo;
                    dbEntry.DueDate = LoanSchedule.DueDate;
                    dbEntry.sPrincipalAmount = LoanSchedule.sPrincipalAmount;
                    dbEntry.sInterestAmount = LoanSchedule.sInterestAmount;
                    dbEntry.sTotalAmount = LoanSchedule.sTotalAmount;
                }
            }
            context.SaveChanges();
        }

        public LoanSchedule DeleteLoanSchedule(long LoanScheduleID)
        {
            LoanSchedule dbEntry = context.LoanSchedule.Find(LoanScheduleID);
            if (dbEntry != null)
            {
                context.LoanSchedule.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long LoanScheduleID)
        {
            return 0;//context.Loan.Where(e => e.LoanScheduleID == LoanScheduleID).Count();

        }
    }
}
