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
    public class EFAllDedRepo : IAllDedRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<AllDed> AllDed(string InstanceID)
        {
            return context.AllDed.Where(a => a.InstanceID==InstanceID); 
        }


        public AllDed Single(string InstanceID, long ID)
        {
            return context.AllDed.Where(a => a.InstanceID == InstanceID && a.AllDedID == ID).FirstOrDefault();
        }

        public void SaveAllDed(AllDed AllDed)
        {

            if (AllDed.AllDedID == 0)
            {
                context.AllDed.Add(AllDed);
            }
            else
            {
                AllDed dbEntry = context.AllDed.Find(AllDed.AllDedID);
                if (dbEntry != null)
                {
                    //dbEntry.AllDedID = AllDed.AllDedID;
                    dbEntry.AllDedName = AllDed.AllDedName;
                    dbEntry.ADType = AllDed.ADType;
                }
            }
            context.SaveChanges();
        }


        public AllDed DeleteAllDed(long AllDedID)
        {
            AllDed dbEntry = context.AllDed.Find(AllDedID);
            if (dbEntry != null)
            {
                context.AllDed.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long AllDedID)
        {
            return 0;// context.Employee.Where(e => e.DepartmentID == DepartmentID).Count();
        }
    }
}
