using HRMSMvc.Domain.Abstract;
using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Concrete
{
    public class EFTrBatchDetailsRepo : ITrBatchDetailsRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<TrBatchDetails> TrBatchDetails
        {
            get { return context.TrBatchDetails; }
        }

        public void SaveTrBatchDetails(TrBatchDetails TrBatchDetails)
        {

            if (TrBatchDetails.TrBatchDetailsID == 0)
            {
                context.TrBatchDetails.Add(TrBatchDetails);
            }
            else
            {
                TrBatchDetails dbEntry = context.TrBatchDetails.Find(TrBatchDetails.TrBatchDetailsID);
                if (dbEntry != null)
                {
                    dbEntry.TrBatchID = TrBatchDetails.TrBatchID;
                    dbEntry.StaffInfoID = TrBatchDetails.StaffInfoID;           
                            
                }
            }
            context.SaveChanges();
        }

        public TrBatchDetails DeleteTrBatchDetails(int TrBatchDetailsID)
        {
            TrBatchDetails dbEntry = context.TrBatchDetails.Find(TrBatchDetailsID);
            if (dbEntry != null)
            {
                context.TrBatchDetails.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
