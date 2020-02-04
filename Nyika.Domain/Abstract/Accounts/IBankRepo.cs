using System.Collections.Generic;
using Nyika.Domain.Entities.Accounts;

namespace Nyika.Domain.Abstract.Accounts
{
    public interface IBankRepo
    {
        IEnumerable<Bank> Bank(string InstanceID);
        Bank Single(string InstanceID, long ID);
        void SaveBank(Bank Bank);
        Bank DeleteBank(long BankID);
        int IsExists(long BankID);
    }
}
