using Nyika.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.HR
{
    public interface IEmployeeAllDedRepo
    {
        IEnumerable<EmployeeAllDed> EmployeeAllDed(string InstanceID);
        EmployeeAllDed Single(string InstanceID,long ID);
        IEnumerable<EmployeeAllDed> Search(string InstanceID, string txtSearch);
        void SaveEmployeeAllDed(EmployeeAllDed EmployeeAllDed);
        EmployeeAllDed DeleteEmployeeAllDed(long EmployeeAllDedId);
        
    }
}
