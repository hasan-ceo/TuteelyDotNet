using Nyika.Domain.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Accounts
{
    public interface IBusinessDayRepo
    {
        IEnumerable<BusinessDay> BusinessDay(string InstanceID);
        BusinessDay Single(string InstanceID, long ID);
        void SaveBusinessDay(BusinessDay BusinessDay);
        BusinessDay DeleteBusinessDay(long BusinessDayId);
        void DayClose(string InstanceID);
        void DayOpen(string InstanceID, string entryby);
        DateTime WorkDate(string InstanceID);
        void MonthClose(string InstanceID);
    }
}
