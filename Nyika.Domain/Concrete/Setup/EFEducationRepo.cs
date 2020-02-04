using Nyika.Domain.Abstract.Setup;
using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.Setup
{
    public class EFEducationRepo : IEducationRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Education> Education(string instanceId)
        {
            return context.Education.Where(e => e.InstanceID == instanceId); 
        }

        public Education Single(string InstanceID, long ID)
        {
            return context.Education.Where(a => a.InstanceID == InstanceID && a.EducationID == ID).FirstOrDefault();
        }

        public void SaveEducation(Education Education)
        {

            if (Education.EducationID == 0)
            {
                context.Education.Add(Education);
            }
            else
            {
                Education dbEntry = context.Education.Find(Education.EducationID);
                if (dbEntry != null)
                {
                    dbEntry.EducationName = Education.EducationName;
                    //dbEntry.CountryID = Education.CountryID;                   
                }
            }
            context.SaveChanges();
        }

        public Education DeleteEducation(long EducationID)
        {
            Education dbEntry = context.Education.Find(EducationID);
            var count = context.Employee.Where(e => e.EducationID == EducationID).Count();
            if (dbEntry != null && count == 0)
            {
                context.Education.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long EducationID)
        {
            return context.Employee.Where(e => e.EducationID == EducationID).Count();
        }
    }
}
