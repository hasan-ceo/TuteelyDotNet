using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Setup
{
    public interface IResignReasonRepo
    {
        IEnumerable<ResignReason> ResignReason(string InstanceID);
        ResignReason Single(string InstanceID, long ID);
        void SaveResignReason(ResignReason ResignReason);
        ResignReason DeleteResignReason(long ResignReasonID);
        int IsExists(long ResignReasonID);
    }
}
