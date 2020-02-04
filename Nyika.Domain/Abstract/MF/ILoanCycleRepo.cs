using System.Collections.Generic;
using Nyika.Domain.Entities.MF;

namespace Nyika.Domain.Abstract.MF
{
    public interface ILoanCycleRepo
    {
        IEnumerable<LoanCycle> LoanCycle(string InstanceID);
        LoanCycle Single(string InstanceID, long ID);
        void SaveLoanCycle(LoanCycle LoanCycle);
        LoanCycle DeleteLoanCycle(long LoanCycleID);
        bool CreateNew(string InstanceID, long ProjectID);
        int IsExists(string InstanceID, long ProjectID);
    }
}
