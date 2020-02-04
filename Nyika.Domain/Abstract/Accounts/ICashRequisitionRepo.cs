using System.Collections.Generic;
using Nyika.Domain.Entities.Accounts;

namespace Nyika.Domain.Abstract.Accounts
{
    public interface ICashRequisitionRepo
    {
        IEnumerable<CashRequisition> CashRequisition(string InstanceID);
        IEnumerable<CashRequisition> CashRequisitionUser(string InstanceID, string entryby);
        CashRequisition Single(string InstanceID, long ID);
        void SaveCashRequisition(CashRequisition CashRequisition);
        void ApproveCashRequisition(long CashRequisitionID, string ApprovedBy);
        CashRequisition DeleteCashRequisition(long CashRequisitionID);
        int DeleteStatus(long CashRequisitionID);
    }
}
