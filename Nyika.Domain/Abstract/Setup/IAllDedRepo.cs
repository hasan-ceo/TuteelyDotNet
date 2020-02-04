using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Setup
{
    public interface IAllDedRepo
    {
        IEnumerable<AllDed> AllDed(string InstanceID);
        AllDed Single(string InstanceID, long ID);
        void SaveAllDed(AllDed AllDed);
        AllDed DeleteAllDed(long AllDedID);
        int IsExists(long DepartmentID);
    }
}
