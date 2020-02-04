using Nyika.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.HR
{
    public interface IEmployeeResignRepo
    {
        IEnumerable<EmployeeResign> EmployeeResign(string InstanceID);
        EmployeeResign Single(string InstanceID, long ID);
        IEnumerable<EmployeeResign> Search(string InstanceID, string txtSearch);
        void SaveEmployeeResign(EmployeeResign EmployeeResign);
        EmployeeResign DeleteEmployeeResign(long EmployeeResignId);
        
    }
}
