using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Abstract
{
    public interface ITrBatchRepo
    {
        IEnumerable<TrBatch> TrBatch { get; }
        void SaveTrBatch(TrBatch TrBatch);
        TrBatch DeleteTrBatch(int TrBatchId);
    }
}
