using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Entities.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.Accounts
{
    public class EFCashRequisitionRepo : ICashRequisitionRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<CashRequisition> CashRequisition(string InstanceID)
        {
            return context.CashRequisition.Where(b => b.InstanceID == InstanceID);
        }

        public IEnumerable<CashRequisition> CashRequisitionUser(string InstanceID, string entryby)
        {
            return context.CashRequisition.Where(b => b.InstanceID == InstanceID && b.EntryBy == entryby);
        }

        public CashRequisition Single(string InstanceID, long ID)
        {
            return context.CashRequisition.Where(a => a.InstanceID == InstanceID && a.CashRequisitionID == ID).FirstOrDefault();
        }

        public void SaveCashRequisition(CashRequisition CashRequisition)
        {

            if (CashRequisition.CashRequisitionID == 0)
            {
                context.CashRequisition.Add(CashRequisition);
            }
            else
            {
                CashRequisition dbEntry = context.CashRequisition.Find(CashRequisition.CashRequisitionID);
                var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
                if (dbEntry != null && dbEntry.WorkDate == wd && dbEntry.Approved == false)
                {
                    //dbEntry.CashRequisitionID = CashRequisition.CashRequisitionID;
                    dbEntry.Particulars = CashRequisition.Particulars;
                    dbEntry.Amount = CashRequisition.Amount;
                }
            }
            context.SaveChanges();
        }

        public void ApproveCashRequisition(long CashRequisitionID, string ApprovedBy)
        {

            if (CashRequisitionID != 0)
            {
                CashRequisition dbEntry = context.CashRequisition.Find(CashRequisitionID);
                var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
                if (dbEntry != null && dbEntry.Approved == false)
                {
                    //dbEntry.CashRequisitionID = CashRequisition.CashRequisitionID;
                    dbEntry.Approved = true;
                    dbEntry.ApprovedBy = ApprovedBy;
                    dbEntry.ApprovedDate = wd;
                }
            }
            context.SaveChanges();
        }

        public CashRequisition DeleteCashRequisition(long CashRequisitionID)
        {
            CashRequisition dbEntry = context.CashRequisition.Find(CashRequisitionID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
            if (dbEntry != null && dbEntry.WorkDate == wd && dbEntry.Approved == false)
            {
                context.CashRequisition.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int DeleteStatus(long CashRequisitionID)
        {
            CashRequisition dbEntry = context.CashRequisition.Find(CashRequisitionID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
            if (dbEntry != null && dbEntry.WorkDate == wd && dbEntry.Approved == false)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
