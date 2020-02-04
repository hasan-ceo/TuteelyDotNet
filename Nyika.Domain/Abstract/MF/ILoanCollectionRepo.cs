using System.Collections.Generic;
using Nyika.Domain.Entities.MF;

namespace Nyika.Domain.Abstract.MF
{
    public interface ILoanCollectionRepo
    {
        IEnumerable<LoanCollectionVM> LoanCollection(string InstanceID, long ID);
        LoanCollection Single(string InstanceID, long ID);
        void SaveLoanCollection(string InstanceID, long LoanCollectionID, double RealizedAmount, long LoanID, string entryby);
        LoanCollection DeleteLoanCollection(long LoanCollectionID);
        int IsExists(long LoanCollectionID);
    }
}
