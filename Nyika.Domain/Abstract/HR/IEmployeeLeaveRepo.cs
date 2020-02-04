using Nyika.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.HR
{
    public interface IEmployeeLeaveRepo
    {
        IEnumerable<EmployeeLeave> EmployeeLeave(string InstanceID);
        EmployeeLeave Single(string InstanceID,long ID);
        IEnumerable<EmployeeLeave> Search(string InstanceID, string txtSearch);
        void SaveEmployeeLeave(EmployeeLeave EmployeeLeave);
        EmployeeLeave DeleteEmployeeLeave(long EmployeeLeaveId);
        
    }
}
