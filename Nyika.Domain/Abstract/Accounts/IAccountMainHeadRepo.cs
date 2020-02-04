using System.Collections.Generic;
using Nyika.Domain.Entities.Accounts;

namespace Nyika.Domain.Abstract.Accounts
{
    public interface IAccountMainHeadRepo
    {
        IEnumerable<AccountMainHead> AccountMainHead(string InstanceID);
        AccountMainHead Single(string InstanceID, long ID);
        void SaveAccountMainHead(AccountMainHead AccountMainHead);
        AccountMainHead DeleteAccountMainHead(long AccountMainHeadID);
        int IsExists(long AccountMainHeadID);
    }
}
