using System.Collections.Generic;
using Nyika.Domain.Entities.MF;

namespace Nyika.Domain.Abstract.MF
{
    public interface ILoanRepo
    {
        IEnumerable<Loan> Loan(string InstanceID);
        IEnumerable<Loan> UnAprrovedLoan(string InstanceID);
        Loan Single(string InstanceID, long ID);
        IEnumerable<Loan> Search(string InstanceID, string txtSearch);
        long SaveLoan(long MemberID, long ProductID, long SchemeID, double DisbursedAmount, string instanceId, string EntryBy);
        Loan DeleteLoan(long LoanID);
        int IsMemberExists(long MemberID);
        int IsDelete(long LoanID);
        int IsApproved(long LoanID);
        void Approved(long LoanID, string InstanceID, string ApprovedBy);
        IEnumerable<Loan> isSettle(string InstanceID);
        bool UndoSettleLoan(string InstanceID, long ID = 0);
        IEnumerable<Loan> SearchRegular(string InstanceID, string txtSearch);
        int SettleLoan(string InstanceID, string EntryBy, long ID = 0);
    }
}
