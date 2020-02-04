using System.Collections.Generic;
using Nyika.Domain.Entities.Accounts;

namespace Nyika.Domain.Abstract.Accounts
{
    public interface ICashSummaryRepo
    {
        IEnumerable<CashSummary> CashSummary(string InstanceID);
        CashSummary Single(string InstanceID, long ID);
        void SaveCashSummary(CashSummary CashSummary);
        CashSummary DeleteCashSummary(long CashSummaryID);
        void CashUpdate(string InstanceID);
    }
}
