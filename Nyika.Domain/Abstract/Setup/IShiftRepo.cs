using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Setup
{
    public interface IShiftRepo
    {
        IEnumerable<Shift> Shift(string InstanceID);
        Shift Single(string InstanceID, long ID);
        void SaveShift(Shift Shift);
        Shift DeleteShift(long ShiftID);
        int IsExists(long ShiftID);
    }
}
