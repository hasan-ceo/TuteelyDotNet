using Nyika.Domain.Abstract.MF;
using Nyika.Domain.Entities.MF;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Nyika.Domain.Concrete.MF
{
    public class EFLoanCollectionRepo : ILoanCollectionRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<LoanCollectionVM> LoanCollection(string InstanceID, long ID)
        {
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == InstanceID).FirstOrDefault().WorkDate;
            return context.Database.SqlQuery<LoanCollectionVM>("SELECT * FROM [dbo].[fnMFLoanCollection] (@CollectionDate,@GroupsID) order by memberno", new SqlParameter("CollectionDate", wd), new SqlParameter("GroupsID", ID));
        }



        public LoanCollection Single(string InstanceID, long ID)
        {
            return context.LoanCollection.Include(b => b.Loan).Where(a => a.InstanceID == InstanceID && a.LoanCollectionID == ID).FirstOrDefault();
        }

        public void SaveLoanCollection(string InstanceID, long LoanCollectionID, double RealizedAmount, long LoanID, string entryby)
        {
            //  context.Database.ExecuteSqlCommand("exec pMFLoanCollectionSave @LoanCollectionID,@LoanID,@RealizedAmount,@EntryBy,@InstanceID", new SqlParameter("LoanCollectionID", LoanCollectionID), new SqlParameter("LoanID", LoanID), new SqlParameter("RealizedAmount", RealizedAmount), new SqlParameter("EntryBy", entryby), new SqlParameter("InstanceID", InstanceID));
            context.Database.ExecuteSqlCommand("exec pMFLoanCollectionSave @LoanID,@Amount,@EntryBy", new SqlParameter("LoanID", LoanID), new SqlParameter("Amount", RealizedAmount), new SqlParameter("EntryBy", entryby));

        }

        public LoanCollection DeleteLoanCollection(long LoanCollectionID)
        {
            LoanCollection dbEntry = context.LoanCollection.Find(LoanCollectionID);
            if (dbEntry != null)
            {
                context.LoanCollection.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long LoanCollectionID)
        {
            return 0;// context.Loan.Where(e => e.LoanCollectionID == LoanCollectionID).Count();

        }
    }
}
