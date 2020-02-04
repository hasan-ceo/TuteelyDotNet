using HRMSMvc.Domain.Abstract;
using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Concrete
{
    public class EFSalaryGradeRepo : ISalaryGradeRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<SalaryGrade> SalaryGrade
        {
            get { return context.SalaryGrade; }
        }

        public void SaveSalaryGrade(SalaryGrade SalaryGrade)
        {

            if (SalaryGrade.SalaryGradeID == 0)
            {
                context.SalaryGrade.Add(SalaryGrade);
            }
            else
            {
                SalaryGrade dbEntry = context.SalaryGrade.Find(SalaryGrade.SalaryGradeID);
                if (dbEntry != null)
                {
                    dbEntry.SalaryGradeName = SalaryGrade.SalaryGradeName;
                    //dbEntry.CountryID = SalaryGrade.CountryID;                   
                }
            }
            context.SaveChanges();
        }

        public SalaryGrade DeleteSalaryGrade(int SalaryGradeID)
        {
            SalaryGrade dbEntry = context.SalaryGrade.Find(SalaryGradeID);
            if (dbEntry != null)
            {
                context.SalaryGrade.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
