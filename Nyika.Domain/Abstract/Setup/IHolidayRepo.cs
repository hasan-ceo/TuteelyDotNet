using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Setup
{
    public interface IHolidayRepo
    {
        IEnumerable<Holiday> Holiday(string InstanceID);
        Holiday Single(string InstanceID, long ID);
        void SaveHoliday(Holiday Holiday);
        Holiday DeleteHoliday(long HolidayID);
        int IsExists(long HolidayID);
    }
}
