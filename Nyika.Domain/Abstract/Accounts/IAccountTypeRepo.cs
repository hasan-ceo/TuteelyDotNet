using System.Collections.Generic;
using Nyika.Domain.Entities.Accounts;

namespace Nyika.Domain.Abstract.Accounts
{
    public interface IAccountTypeRepo
    {
        IEnumerable<AccountType> AccountType(string InstanceID);
        AccountType Single(string InstanceID, long ID);
        void SaveAccountType(AccountType AccountType);
        AccountType DeleteAccountType(long AccountTypeID);
    }
}
