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
    public class EFAttenStatusRepo : IAttenStatusRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<AttenStatus> AttenStatus
        {
            get { return context.AttenStatus; }
        }

        public AttenStatus Single(long ID)
        {
            return context.AttenStatus.Where(a => a.AttenStatusID == ID).FirstOrDefault();
        }

        public void SaveAttenStatus(AttenStatus AttenStatus)
        {

            if (AttenStatus.AttenStatusID == 0)
            {
                context.AttenStatus.Add(AttenStatus);
            }
            else
            {
                AttenStatus dbEntry = context.AttenStatus.Find(AttenStatus.AttenStatusID);
                if (dbEntry != null)
                {
                    //dbEntry.AttenStatusID = AttenStatus.AttenStatusID;
                    dbEntry.AttenStatusName = AttenStatus.AttenStatusName;
                    dbEntry.StatusCode = AttenStatus.StatusCode;
                }
            }
            context.SaveChanges();
        }


        public AttenStatus DeleteAttenStatus(long AttenStatusID)
        {
            AttenStatus dbEntry = context.AttenStatus.Find(AttenStatusID);
            if (dbEntry != null)
            {
                context.AttenStatus.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long AttenStatusID)
        {
            return 0;// context.Employee.Where(e => e.DepartmentID == DepartmentID).Count();
        }
    }
}
