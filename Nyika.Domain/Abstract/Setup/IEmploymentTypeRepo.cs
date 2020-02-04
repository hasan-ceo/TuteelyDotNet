using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Setup
{
    public interface IEmploymentTypeRepo
    {
        IEnumerable<EmploymentType> EmploymentType(string InstanceID);
        EmploymentType Single(string InstanceID, long ID);
        void SaveEmploymentType(EmploymentType EmploymentType);
        EmploymentType DeleteEmploymentType(long EmploymentTypeID);
        int IsExists(long EmploymentTypeID);
    }
}
