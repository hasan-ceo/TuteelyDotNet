using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Entities.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.Accounts
{
    public class EFAccountMainHeadRepo : IAccountMainHeadRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<AccountMainHead> AccountMainHead(string InstanceID)
        {
            return context.AccountMainHead.Where(a => a.InstanceID==InstanceID); 
        }

        public AccountMainHead Single(string InstanceID, long ID)
        {
            return context.AccountMainHead.Where(a => a.InstanceID == InstanceID && a.AccountMainHeadID == ID).FirstOrDefault();
        }

        public void SaveAccountMainHead(AccountMainHead AccountMainHead)
        {

            if (AccountMainHead.AccountMainHeadID == 0)
            {
                context.AccountMainHead.Add(AccountMainHead);
            }
            else
            {
                AccountMainHead dbEntry = context.AccountMainHead.Find(AccountMainHead.AccountMainHeadID);
                if (dbEntry != null)
                {
                    //dbEntry.AccountMainHeadId = AccountMainHead.AccountMainHeadId;
                    dbEntry.AccountMainHeadCode = AccountMainHead.AccountMainHeadCode;
                    dbEntry.AccountMainHeadName = AccountMainHead.AccountMainHeadName;
                    dbEntry.AccountTypeID = AccountMainHead.AccountTypeID;
                    dbEntry.AutoAc = AccountMainHead.AutoAc;
                }
            }
            context.SaveChanges();
        }

        public AccountMainHead DeleteAccountMainHead(long AccountMainHeadID)
        {
            AccountMainHead dbEntry = context.AccountMainHead.Find(AccountMainHeadID);
            var count = context.AccountSubHead.Where(e => e.AccountMainHeadID == AccountMainHeadID).Count();
            if (dbEntry != null && count==0)
            {
                context.AccountMainHead.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long AccountMainHeadID)
        {
            return context.AccountSubHead.Where(e => e.AccountMainHeadID == AccountMainHeadID).Count();

        }
    }
}
