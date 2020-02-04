using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Abstract
{
    public interface IStaffInfoRepo
    {
        IEnumerable<StaffInfo> StaffInfo { get; }
        void SaveStaffInfo(StaffInfo StaffInfo);
        StaffInfo DeleteStaffInfo(int StaffInfoId);
    }
}
