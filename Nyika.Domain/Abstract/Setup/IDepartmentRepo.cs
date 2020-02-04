using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Setup
{
    public interface IDepartmentRepo
    {
        IEnumerable<Department> Department(string InstanceID);
        Department Single(string InstanceID, long ID);
        void SaveDepartment(Department Department);
        Department DeleteDepartment(long DepartmentID);
        int IsExists(long DepartmentID);
    }
}
