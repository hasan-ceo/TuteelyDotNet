using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Entities.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Nyika.Domain.Entities.MF;
using Nyika.Domain.Entities.Setup;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Nyika.Domain.Concrete.Accounts
{
    public class EFAccountGLRepo : IAccountGLRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<AccountGL> AccountGL(string InstanceID)
        {
            return context.AccountGL.Where(a => a.InstanceID == InstanceID);
        }

        

        public AccountGL Single(string InstanceID, long ID)
        {
            return context.AccountGL.Where(a => a.InstanceID == InstanceID && a.AccountGLID == ID).FirstOrDefault();
        }

        //public AccountGL SingleByRef(string InstanceID, string ID)
        //{
        //    return context.AccountGL.Where(a => a.InstanceID == InstanceID && a.RefID == ID).FirstOrDefault();
        //}

        public AccountGL DeleteAccountGL(long AccountGLID)
        {
            AccountGL dbEntry = context.AccountGL.Find(AccountGLID);
            if (dbEntry != null)
            {
                context.AccountGL.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }


        public IEnumerable<AccountGLVM> DclList(string InstanceID)
        {
            //context.Database.CommandTimeout = 180;
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == InstanceID).FirstOrDefault().WorkDate;
            var innerJoinQuery =
             from a in context.AccountGL
             join g in context.Groups on a.GroupsID equals g.GroupsID
             where a.InstanceID == InstanceID && a.WorkDate== wd && a.AccountSubHeadID == 41204294 && a.dr == 0
             select new AccountGLVM { AccountGLID = a.AccountGLID, Vno = a.Vno, WorkDate=a.WorkDate, ProjectName=g.Project.ProjectName, GroupsName=g.GroupsName,amount=a.cr, Particulars=a.Particulars, EntryBy = a.EntryBy,MemberName="",MemberNo=""};

            return innerJoinQuery;
        }

        public bool DeleteDCR(long AccountGLID)
        {
            AccountGL dbEntry = context.AccountGL.Find(AccountGLID);
            if (dbEntry != null)
            {
                context.Database.ExecuteSqlCommand("exec pMFDCRundo @AccountGLID={0},@InstanceID={1}", dbEntry.AccountGLID, dbEntry.InstanceID);
                
                return true;
            }
            return false;
        }

        public void DCRSave(long GroupsID, double Amount, string Particulars, long ProjectID, string InstanceID, string EntryBy)
        {
            Groups dbEntry = context.Groups.Find(GroupsID);
            if (dbEntry != null)
            {
                var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == InstanceID).FirstOrDefault().WorkDate;
                //Weekdays a=new Weekdays();
                //var t=a()
                //if (dbEntry.ColDay==wd.DayOfWeek)
                context.Database.ExecuteSqlCommand("exec pMFDCRCreate @GroupsID={0},@Amount={1},@Particulars={2},@ProjectID={3},@InstanceID={4},@EntryBy={5}", GroupsID, Amount, Particulars, ProjectID, InstanceID, EntryBy);
                
            }
        }


        public IEnumerable<AccountGLVM> SecurityList(string InstanceID)
        {
            //context.Database.CommandTimeout = 180;
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == InstanceID).FirstOrDefault().WorkDate;
            var innerJoinQuery =
             from a in context.AccountGL
             join g in context.Groups on a.GroupsID equals g.GroupsID
             join m in context.Member on a.MemberID equals m.MemberID
             where a.InstanceID == InstanceID && a.WorkDate == wd && a.AccountSubHeadID == 41204293 && a.cr == 0
             select new AccountGLVM { AccountGLID = a.AccountGLID, Vno = a.Vno, WorkDate = a.WorkDate, ProjectName = g.Project.ProjectName, GroupsName = g.GroupsName, amount = a.dr, Particulars = a.Particulars, EntryBy = a.EntryBy, MemberName = m.MemberName, MemberNo = m.MemberNo.ToString() };

            return innerJoinQuery;
        }

        public int SecurityWithdrawSave(long MemberID,long GroupsID, double Amount, long ProjectID, string InstanceID, string EntryBy)
        {
            Member dbEntry = context.Member.Find(MemberID);
            if (dbEntry != null)
            {
                var done = new SqlParameter();
                done.ParameterName = "done";
                done.SqlDbType = SqlDbType.BigInt;//DataType Of OutPut Parameter
                done.Direction = ParameterDirection.Output;
                done.Value = 0;
                context.Database.ExecuteSqlCommand("exec pMFLoanSecurityWithdraw @MemberId,@InstanceID,@EntryBy, @done out",
                     new SqlParameter("@MemberID", MemberID),
                new SqlParameter("@InstanceID", InstanceID),
                new SqlParameter("@EntryBy", EntryBy),
                done);
                return Convert.ToInt32(done.Value);
            }
            return 0;
        }

        public bool DeleteSecurityWithdraw(long AccountGLID)
        {
            AccountGL dbEntry = context.AccountGL.Find(AccountGLID);
            if (dbEntry != null)
            {
                context.Database.ExecuteSqlCommand("exec pMFLoanSecurityWithdrawUndo @AccountGLID={0},@InstanceID={1}", dbEntry.AccountGLID, dbEntry.InstanceID);
                return true;
            }
            return false;
        }

    }
}
