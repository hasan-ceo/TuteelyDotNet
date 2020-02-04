using System.Collections.Generic;
using Nyika.Domain.Entities.Accounts;

namespace Nyika.Domain.Abstract.Accounts
{
    public interface IAccountSubHeadRepo
    {
        IEnumerable<AccountSubHead> AccountSubHead(string InstanceID);
        AccountSubHead Single(string InstanceID, long ID);
        void SaveAccountSubHead(AccountSubHead AccountSubHead);
        AccountSubHead DeleteAccountSubHead(long AccountSubHeadID);
        int IsExists(long AccountSubHeadID);
    }
}
