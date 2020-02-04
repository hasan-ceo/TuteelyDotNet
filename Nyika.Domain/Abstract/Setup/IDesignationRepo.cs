using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Setup
{
    public interface IDesignationRepo
    {
        IEnumerable<Designation> Designation(string InstanceID);
        Designation Single(string InstanceID, long ID);
        void SaveDesignation(Designation Designation);
        Designation DeleteDesignation(long DesignationID);
        int IsExists(long DesignationID);
    }
}
