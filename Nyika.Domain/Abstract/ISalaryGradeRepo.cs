using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Abstract
{
    public interface ISalaryGradeRepo
    {
        IEnumerable<SalaryGrade> SalaryGrade { get; }
        void SaveSalaryGrade(SalaryGrade SalaryGrade);
        SalaryGrade DeleteSalaryGrade(int SalaryGradeId);
    }
}
