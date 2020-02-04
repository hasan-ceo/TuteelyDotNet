using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Setup
{
    public interface IErrorLogRepo
    {
        IEnumerable<ErrorLog> ErrorLog(string InstanceID);
        ErrorLog Single(string InstanceID, long ID);
        void SaveErrorLog(ErrorLog ErrorLog);
        ErrorLog DeleteErrorLog(long ErrorLogID);
    }
}
