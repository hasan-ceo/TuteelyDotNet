using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Entities.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.Accounts
{
    public class EFAccountTypeRepo : IAccountTypeRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<AccountType> AccountType(string InstanceID)
        {
             return context.AccountType.Where(a => a.InstanceID==InstanceID); 
        }

        public AccountType Single(string InstanceID, long ID)
        {
            return context.AccountType.Where(a => a.InstanceID == InstanceID && a.AccountTypeID == ID).FirstOrDefault();
        }

        public void SaveAccountType(AccountType AccountType)
        {

            if (AccountType.AccountTypeID == 0)
            {
                context.AccountType.Add(AccountType);
            }
            else
            {
                AccountType dbEntry = context.AccountType.Find(AccountType.AccountTypeID);
                if (dbEntry != null)
                {
                    //dbEntry.AccountTypeId = AccountType.AccountTypeId;
                    dbEntry.AccountTypeName = AccountType.AccountTypeName;
                }
            }
            context.SaveChanges();
        }

        public AccountType DeleteAccountType(long AccountTypeID)
        {
            AccountType dbEntry = context.AccountType.Find(AccountTypeID);
            if (dbEntry != null)
            {
                context.AccountType.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
