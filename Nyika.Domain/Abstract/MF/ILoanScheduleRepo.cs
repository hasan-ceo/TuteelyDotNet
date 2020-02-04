using System.Collections.Generic;
using Nyika.Domain.Entities.MF;

namespace Nyika.Domain.Abstract.MF
{
    public interface ILoanScheduleRepo
    {
        IEnumerable<LoanSchedule> LoanSchedule(string InstanceID);
        LoanSchedule Single(string InstanceID, long ID);
        void SaveLoanSchedule(LoanSchedule LoanSchedule);
        LoanSchedule DeleteLoanSchedule(long LoanScheduleID);
        int IsExists(long LoanScheduleID);
    }
}
