using Nyika.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.HR
{
    public interface IEmployeeAttendanceRepo
    {
        IEnumerable<EmployeeAttendance> EmployeeAttendance(string InstanceID);
        EmployeeAttendance Single(string InstanceID, long ID);
        IEnumerable<EmployeeAttendance> Search(string InstanceID, string txtSearch);
        void SaveEmployeeAttendance(long EmployeeID, long AttenStatusID, string Particulars, DateTime InTime, DateTime OutTime, DateTime FromDate, DateTime TillDate, string EntryBy, string instanceId);
        EmployeeAttendance DeleteEmployeeAttendance(long EmployeeAttendanceId);
        void ProcessAttendance(string InstanceID);
        void ProcessSalary(string InstanceID);
    }
}
