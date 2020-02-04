using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Setup
{
    public interface ILeaveRepo
    {
        IEnumerable<Leave> Leave(string InstanceID);
        Leave Single(string InstanceID, long ID);
        void SaveLeave(Leave Leave);
        Leave DeleteLeave(long LeaveID);
        int IsExists(long LeaveID);
    }
}
