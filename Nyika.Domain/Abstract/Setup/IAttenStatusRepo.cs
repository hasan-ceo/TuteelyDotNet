using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Setup
{
    public interface IAttenStatusRepo
    {
        IEnumerable<AttenStatus> AttenStatus { get; }
        AttenStatus Single( long ID);
        void SaveAttenStatus(AttenStatus AttenStatus);
        AttenStatus DeleteAttenStatus(long AttenStatusID);
        int IsExists(long DepartmentID);
    }
}
