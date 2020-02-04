using System.Collections.Generic;
using Nyika.Domain.Entities.Accounts;

namespace Nyika.Domain.Abstract.Accounts
{
    public interface IAccountGLRepo
    {
        IEnumerable<AccountGL> AccountGL(string InstanceID);
        AccountGL Single(string InstanceID,long ID);
        //void PayRec(TransType TransType, long BankID, long AccountSubHeadID, double Amount, string Pat, string VType);
        //void Journal(long AccountSubHeadIDDr, long AccountSubHeadIDCr, double Amount, string Pat, string VType);
        //void Transfer(TransType TransType, long BankID, double Amount, string Pat, string VType);
        //void Reverse();
        AccountGL DeleteAccountGL(long AccountGLID);
        IEnumerable<AccountGLVM> DclList(string InstanceID);
        bool DeleteDCR(long AccountGLID);
        void DCRSave(long GroupsID, double Amount, string Particulars, long ProjectID, string InstanceID, string EntryBy);
        IEnumerable<AccountGLVM> SecurityList(string InstanceID);
        int SecurityWithdrawSave(long MemberID, long GroupsID, double Amount, long ProjectID, string InstanceID, string EntryBy);
        bool DeleteSecurityWithdraw(long AccountGLID);
    }
}
