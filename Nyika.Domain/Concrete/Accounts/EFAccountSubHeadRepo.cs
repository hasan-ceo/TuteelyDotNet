using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Entities.Accounts;
using System.Collections.Generic;
using System.Data.Entity;
using System;
using System.Data;
using System.Linq;


namespace Nyika.Domain.Concrete.Accounts
{
    public class EFAccountSubHeadRepo : IAccountSubHeadRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<AccountSubHead> AccountSubHead(string InstanceID)
        {
            return context.AccountSubHead.Include(a => a.AccountMainHead).Where(a => a.InstanceID== InstanceID);
        }

        public AccountSubHead Single(string InstanceID, long ID)
        {
            return context.AccountSubHead.Include(a => a.AccountMainHead).Where(a => a.InstanceID == InstanceID && a.AccountSubHeadID==ID).FirstOrDefault();
        }

        public void SaveAccountSubHead(AccountSubHead AccountSubHead)
        {

            if (AccountSubHead.AccountSubHeadID == 0)
            {
                context.AccountSubHead.Add(AccountSubHead);
            }
            else
            {
                AccountSubHead dbEntry = context.AccountSubHead.Find(AccountSubHead.AccountSubHeadID);
                if (dbEntry != null)
                {
                    //dbEntry.AccountSubHeadId = AccountSubHead.AccountSubHeadId;
                    dbEntry.AccountSubHeadCode = AccountSubHead.AccountSubHeadCode;
                    dbEntry.AccountSubHeadName = AccountSubHead.AccountSubHeadName;
                    dbEntry.AccountMainHeadID = AccountSubHead.AccountMainHeadID;
                  
                    dbEntry.AutoAc = AccountSubHead.AutoAc;
                }
            }
            context.SaveChanges();
        }

        public AccountSubHead DeleteAccountSubHead(long AccountSubHeadID)
        {
            AccountSubHead dbEntry = context.AccountSubHead.Find(AccountSubHeadID);
            var count = context.AccountGL.Where(e => e.AccountSubHeadID == AccountSubHeadID).Count();
            if (dbEntry != null && count==0)
            {
                context.AccountSubHead.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long AccountSubHeadID)
        {
            return context.AccountGL.Where(e => e.AccountSubHeadID == AccountSubHeadID).Count();

        }
    }
}
