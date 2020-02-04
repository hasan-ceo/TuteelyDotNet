using Nyika.Domain.Abstract.MF;
using Nyika.Domain.Entities.MF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.MF
{
    public class EFLoanRepo : ILoanRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Loan> Loan(string InstanceID)
        {
            return context.Loan.Where(b => b.InstanceID == InstanceID);
        }

        public IEnumerable<Loan> UnAprrovedLoan(string InstanceID)
        {
            return context.Loan.Where(b => b.InstanceID == InstanceID && b.LoanAprroved == false);
        }

        public IEnumerable<Loan> isSettle(string InstanceID)
        {
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == InstanceID).FirstOrDefault().WorkDate;
            return context.Loan.Where(b => b.InstanceID == InstanceID && b.SettlementDate == wd && b.isSettle == true);
        }

        public bool UndoSettleLoan(string InstanceID, long ID = 0)
        {
            if (ID != 0)
            {
                context.Database.ExecuteSqlCommand("exec pMFLoanSettleUndo @LoanId={0},@InstanceID={1}", ID, InstanceID);
                return true;
            }
            return false;
        }

        public int SettleLoan(string InstanceID, string EntryBy, long ID = 0)
        {
            if (ID != 0)
            {
                var done = new SqlParameter();
                done.ParameterName = "done";
                done.SqlDbType = SqlDbType.BigInt;//DataType Of OutPut Parameter
                done.Direction = ParameterDirection.Output;
                done.Value = 0;
                context.Database.ExecuteSqlCommand("exec pMFLoanSettle @LoanId,@InstanceID,@EntryBy,@done out",
                new SqlParameter("@LoanId", ID),
                new SqlParameter("@InstanceID", InstanceID),
                new SqlParameter("@EntryBy", EntryBy),
                done);
                return Convert.ToInt32(done.Value);
            }
            return 0;
        }

        public Loan Single(string InstanceID, long ID)
        {
            return context.Loan.Where(a => a.InstanceID == InstanceID && a.LoanID == ID).FirstOrDefault();
        }

        public IEnumerable<Loan> Search(string InstanceID, string txtSearch)
        {
            return context.Loan.Where(e => (e.LoanID.ToString().Contains(txtSearch) || e.Member.Groups.GroupsName.Contains(txtSearch) || e.Member.MemberNo.ToString().Contains(txtSearch) || e.Member.MemberName.Contains(txtSearch) || e.LoanNo.Contains(txtSearch)) && e.InstanceID == InstanceID).OrderBy(e => e.Member.Groups.GroupsID).OrderBy(e => e.Member.MemberNo);
        }

        public IEnumerable<Loan> SearchRegular(string InstanceID, string txtSearch)
        {
            return context.Loan.Where(e => (e.LoanID.ToString().Contains(txtSearch) || e.Member.Groups.GroupsName.Contains(txtSearch) || e.Member.MemberNo.ToString().Contains(txtSearch) || e.Member.MemberName.Contains(txtSearch) || e.LoanNo.Contains(txtSearch)) && e.InstanceID == InstanceID && e.isSettle == false && e.LoanStatus != "Close").OrderBy(e => e.Member.Groups.GroupsID).OrderBy(e => e.Member.MemberNo);
        }

        public long SaveLoan(long MemberID, long ProductID, long SchemeID, double DisbursedAmount, string instanceId, string EntryBy)
        {
            var done = new SqlParameter();
            done.ParameterName = "done";
            done.SqlDbType = SqlDbType.BigInt;//DataType Of OutPut Parameter
            done.Direction = ParameterDirection.Output;
            done.Value = 0;
            context.Database.ExecuteSqlCommand("exec pMFLoanSave  @MemberID, @ProductID, @SchemeID, @DisbursedAmount,  @InstanceID,  @EntryBy, @done out",
                new SqlParameter("@MemberID", MemberID),
                new SqlParameter("@ProductID", ProductID),
                new SqlParameter("@SchemeID", SchemeID),
                new SqlParameter("@DisbursedAmount", DisbursedAmount),
                new SqlParameter("@instanceId", instanceId),
                new SqlParameter("@EntryBy", EntryBy),
                done);
            return Convert.ToInt64(done.Value);
            //if (Loan.LoanID == 0)
            //{
            //    context.Loan.Add(Loan);
            //}
            //else
            //{
            //    Loan dbEntry = context.Loan.Find(Loan.LoanID);
            //    if (dbEntry != null)
            //    {
            //        dbEntry.MemberID = Loan.MemberID;
            //        dbEntry.ProductID = Loan.ProductID;
            //        dbEntry.SchemeID = Loan.SchemeID;
            //        dbEntry.LoanNo = Loan.LoanNo;
            //        dbEntry.DisbursementDate = Loan.DisbursementDate;
            //        dbEntry.LoanStatus = Loan.LoanStatus;
            //        dbEntry.LoanCycle = Loan.LoanCycle;
            //        dbEntry.DisbursedAmount = Loan.DisbursedAmount;
            //        dbEntry.InterestRate = Loan.InterestRate;
            //        dbEntry.InterestAmount = Loan.InterestAmount;
            //        dbEntry.TotalLoanAmount = Loan.TotalLoanAmount;
            //        dbEntry.Duration = Loan.Duration;
            //        dbEntry.NoOfInstallment = Loan.NoOfInstallment;
            //        dbEntry.InstallmentAmount = Loan.InstallmentAmount;
            //        dbEntry.SecurityAmount = Loan.SecurityAmount;
            //        dbEntry.PrincipalAmount = Loan.PrincipalAmount;
            //        dbEntry.InterestReceivable = Loan.InterestReceivable;
            //        dbEntry.InterestRealizable = Loan.InterestRealizable;
            //        dbEntry.LoanDue = Loan.LoanDue;
            //        dbEntry.OverdueAmount = Loan.OverdueAmount;
            //        dbEntry.LoanExpireDate = Loan.LoanExpireDate;
            //        dbEntry.HasProvision = Loan.HasProvision;
            //        dbEntry.HasCollection = Loan.HasCollection;
            //        dbEntry.LoanAprroved = Loan.LoanAprroved;
            //        dbEntry.EntryBy = Loan.EntryBy;
            //        //dbEntry.WorkDate = Loan.WorkDate;
            //        dbEntry.InstanceID = Loan.InstanceID;
            //    }
            //}
            //context.SaveChanges();
        }

        public void Approved(long LoanID, string InstanceID, string ApprovedBy)
        {
            //var done = new SqlParameter();
            //done.ParameterName = "done";
            //done.SqlDbType = SqlDbType.Int;//DataType Of OutPut Parameter
            //done.Direction = ParameterDirection.Output;
            //done.Value = 0;
            context.Database.ExecuteSqlCommand("exec pMFLoanApproved  @LoanID,@ApprovedBy,@InstanceID",
                new SqlParameter("@LoanID", LoanID),
                new SqlParameter("@ApprovedBy", ApprovedBy),
                new SqlParameter("@InstanceID", InstanceID));
            //return Convert.ToInt32(done.Value);

            //Loan dbEntry = context.Loan.Find(LoanID);
            //if (dbEntry != null)
            //{
            //    dbEntry.LoanAprroved = true;
            //    context.SaveChanges();
            //}
        }

        public Loan DeleteLoan(long LoanID)
        {
            Loan dbEntry = context.Loan.Find(LoanID);
            var count = context.Loan.Where(e => e.LoanID == LoanID && (e.HasCollection == false && e.HasProvision == false)).Count();
            if (dbEntry != null && count == 1)
            {
                context.Loan.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsMemberExists(long MemberID)
        {
            return context.Loan.Where(e => e.MemberID == MemberID && e.LoanStatus != "Close").Count();
        }

        public int IsDelete(long LoanID)
        {
            return context.Loan.Where(e => e.LoanID == LoanID && (e.HasCollection == false && e.HasProvision == false)).Count();

        }

        public int IsApproved(long LoanID)
        {
            return context.Loan.Where(e => e.LoanID == LoanID && e.LoanAprroved == true).Count();

        }
    }
}
