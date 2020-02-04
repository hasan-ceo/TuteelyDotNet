using Nyika.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.HR
{
    public interface IEmployeeIncrementRepo
    {
        IEnumerable<EmployeeIncrement> EmployeeIncrement(string InstanceID);
        EmployeeIncrement Single(string InstanceID,long ID);
        IEnumerable<EmployeeIncrement> Search(string InstanceID, string txtSearch);
        void SaveEmployeeIncrement(EmployeeIncrement EmployeeIncrement);
        EmployeeIncrement DeleteEmployeeIncrement(long EmployeeIncrementId);
        
    }
}
