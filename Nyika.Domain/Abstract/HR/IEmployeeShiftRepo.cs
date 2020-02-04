using Nyika.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.HR
{
    public interface IEmployeeShiftRepo
    {
        IEnumerable<EmployeeShift> EmployeeShift(string InstanceID);
        EmployeeShift Single(string InstanceID,long ID);
        IEnumerable<EmployeeShift> Search(string InstanceID, string txtSearch);
        void SaveEmployeeShift(EmployeeShift EmployeeShift);
        EmployeeShift DeleteEmployeeShift(long EmployeeShiftId);
        
    }
}
