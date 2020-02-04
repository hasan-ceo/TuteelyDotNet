using Nyika.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.HR
{
    public interface IEmployeeTransferRepo
    {
        IEnumerable<EmployeeTransfer> EmployeeTransfer(string InstanceID);
        EmployeeTransfer Single(string InstanceID,long ID);
        IEnumerable<EmployeeTransfer> Search(string InstanceID, string txtSearch);
        void SaveEmployeeTransfer(EmployeeTransfer EmployeeTransfer);
        EmployeeTransfer DeleteEmployeeTransfer(long EmployeeTransferId);
        
    }
}
