using HRMSMvc.Domain.Abstract;
using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Concrete
{
    public class EFTrBatchRepo : ITrBatchRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<TrBatch> TrBatch
        {
            get { return context.TrBatch; }
        }

        public void SaveTrBatch(TrBatch TrBatch)
        {

            if (TrBatch.TrBatchID == 0)
            {
                context.TrBatch.Add(TrBatch);
            }
            else
            {
                TrBatch dbEntry = context.TrBatch.Find(TrBatch.TrBatchID);
                if (dbEntry != null)
                {
                    dbEntry.TrBatchName = TrBatch.TrBatchName;
                    dbEntry.TrainingID = TrBatch.TrainingID;
                    dbEntry.VenueName = TrBatch.VenueName;
                    dbEntry.StartDate = TrBatch.StartDate;
                    dbEntry.EndDate = TrBatch.EndDate;
                    dbEntry.Facilitator = TrBatch.Facilitator;
                }
            }
            context.SaveChanges();
        }

        public TrBatch DeleteTrBatch(int TrBatchID)
        {
            TrBatch dbEntry = context.TrBatch.Find(TrBatchID);
            if (dbEntry != null)
            {
                context.TrBatch.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
