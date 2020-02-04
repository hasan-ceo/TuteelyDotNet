using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Entities.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.Accounts
{
    public class EFCashSummaryRepo : ICashSummaryRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<CashSummary> CashSummary(string InstanceID)
        {
            return context.CashSummary.Where(c => c.InstanceID==InstanceID); 
        }

        public CashSummary Single(string InstanceID, long ID)
        {
            return context.CashSummary.Where(a => a.InstanceID == InstanceID && a.CashSummaryID == ID).FirstOrDefault();
        }


        public void CashUpdate(string InstanceID)
        {
            context.Database.ExecuteSqlCommand("exec pAccCashUpdate @InstanceID={0}", InstanceID);
        }

        public void SaveCashSummary(CashSummary CashSummary)
        {

            if (CashSummary.CashSummaryID == 0)
            {
                context.CashSummary.Add(CashSummary);
            }
            else
            {
                CashSummary dbEntry = context.CashSummary.Find(CashSummary.CashSummaryID);
                if (dbEntry != null)
                {
                    dbEntry.AccountSubHeadName = CashSummary.AccountSubHeadName;
                    dbEntry.BankID = CashSummary.BankID;
                    dbEntry.BankName = CashSummary.BankName;
                    dbEntry.Balance = CashSummary.Balance;
                }
            }
            context.SaveChanges();
        }

        public CashSummary DeleteCashSummary(long CashSummaryID)
        {
            CashSummary dbEntry = context.CashSummary.Find(CashSummaryID);
            if (dbEntry != null)
            {
                context.CashSummary.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
