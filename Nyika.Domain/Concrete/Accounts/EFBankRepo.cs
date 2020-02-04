using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Entities.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.Accounts
{
    public class EFBankRepo : IBankRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Bank> Bank(string InstanceID)
        {
             return context.Bank.Where(b => b.InstanceID==InstanceID); 
        }

        public Bank Single(string InstanceID, long ID)
        {
            return context.Bank.Where(a => a.InstanceID == InstanceID && a.BankID == ID).FirstOrDefault();
        }

        public void SaveBank(Bank Bank)
        {

            if (Bank.BankID == 0)
            {
                context.Bank.Add(Bank);
            }
            else
            {
                Bank dbEntry = context.Bank.Find(Bank.BankID);
                if (dbEntry != null)
                {
                    //dbEntry.BankID = Bank.BankID;
                    dbEntry.BankName = Bank.BankName;
                    dbEntry.AccountNumber = Bank.AccountNumber;
                    dbEntry.Currency = Bank.Currency;
                    dbEntry.BankAddress = Bank.BankAddress;
                }
            }
            context.SaveChanges();
        }

        public Bank DeleteBank(long BankID)
        {
            Bank dbEntry = context.Bank.Find(BankID);
            var count = context.AccountGL.Where(e => e.BankID == BankID).Count();
            if (dbEntry != null && count==0)
            {
                context.Bank.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long BankID)
        {
            return context.AccountGL.Where(e => e.BankID == BankID).Count();

        }
    }
}
