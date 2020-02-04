using Nyika.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.HR
{
    public interface IEmployeePayrollRepo
    {
        IEnumerable<EmployeePayroll> EmployeePayroll(string InstanceID);
        EmployeePayroll Single(string InstanceID,long ID);
        IEnumerable<EmployeePayroll> Search(string InstanceID, string txtSearch);
        void SaveEmployeePayroll(EmployeePayroll EmployeePayroll);
        EmployeePayroll DeleteEmployeePayroll(long EmployeePayrollId);
        
    }
}
