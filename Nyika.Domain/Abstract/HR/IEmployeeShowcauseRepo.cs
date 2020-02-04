using Nyika.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.HR
{
    public interface IEmployeeShowcauseRepo
    {
        IEnumerable<EmployeeShowcause> EmployeeShowcause(string InstanceID);
        EmployeeShowcause Single(string InstanceID, long ID);
        IEnumerable<EmployeeShowcause> Search(string InstanceID, string txtSearch);
        void SaveEmployeeShowcause(EmployeeShowcause EmployeeShowcause);
        EmployeeShowcause DeleteEmployeeShowcause(long EmployeeShowcauseId);
    }
}
