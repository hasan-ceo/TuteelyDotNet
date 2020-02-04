using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Abstract
{
    public interface ITrBatchDetailsRepo
    {
        IEnumerable<TrBatchDetails> TrBatchDetails { get; }
        void SaveTrBatchDetails(TrBatchDetails TrBatchDetails);
        TrBatchDetails DeleteTrBatchDetails(int TrBatchDetailsId);
    }
}
