using Nyika.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.HR
{
    public interface IEmployeeRepo
    {
        IEnumerable<Employee> Employee(string InstanceID);
        IEnumerable<Employee> EmployeeActive(string InstanceID);
        Employee Single(string InstanceID, long ID);
        IEnumerable<Employee> Search(string InstanceID, string txtSearch);
        void SaveEmployee(Employee Employee);
        Employee DeleteEmployee(long EmployeeId);
        bool isEmailExists(string InstanceID, string email);


    }
}
