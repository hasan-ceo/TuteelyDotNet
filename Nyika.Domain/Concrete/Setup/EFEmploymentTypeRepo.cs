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
    public class EFEmploymentTypeRepo : IEmploymentTypeRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<EmploymentType> EmploymentType(string instanceId)
        {
            return context.EmploymentType.Where(e => e.InstanceID == instanceId);
        }

        public EmploymentType Single(string InstanceID, long ID)
        {
            return context.EmploymentType.Where(a => a.InstanceID == InstanceID && a.EmploymentTypeID == ID).FirstOrDefault();
        }

        public void SaveEmploymentType(EmploymentType EmploymentType)
        {

            if (EmploymentType.EmploymentTypeID == 0)
            {
                context.EmploymentType.Add(EmploymentType);
            }
            else
            {
                EmploymentType dbEntry = context.EmploymentType.Find(EmploymentType.EmploymentTypeID);
                if (dbEntry != null)
                {
                    dbEntry.EmploymentTypeName = EmploymentType.EmploymentTypeName;
                    //dbEntry.EmploymentTypeName = EmploymentType.EmploymentTypeName;
                }
            }
            context.SaveChanges();
        }

        public EmploymentType DeleteEmploymentType(long EmploymentTypeID)
        {
            EmploymentType dbEntry = context.EmploymentType.Find(EmploymentTypeID);
            var count = context.Employee.Where(e => e.EmploymentTypeID == EmploymentTypeID).Count();
            if (dbEntry != null && count==0)
            {
                context.EmploymentType.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long EmploymentTypeID)
        {
            return context.Employee.Where(e => e.EmploymentTypeID == EmploymentTypeID).Count();
        }
    }
}
