using Nyika.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.HR
{
    public interface IEmployeeTourRepo
    {
        IEnumerable<EmployeeTour> EmployeeTour(string InstanceID);
        EmployeeTour Single(string InstanceID,long ID);
        IEnumerable<EmployeeTour> Search(string InstanceID, string txtSearch);
        void SaveEmployeeTour(EmployeeTour EmployeeTour);
        EmployeeTour DeleteEmployeeTour(long EmployeeTourId);
        
    }
}
