using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Setup
{
    public interface IEducationRepo
    {
        IEnumerable<Education> Education(string InstanceID);
        Education Single(string InstanceID, long ID);
        void SaveEducation(Education Education);
        Education DeleteEducation(long EducationID);
        int IsExists(long EducationID);
    }
}
