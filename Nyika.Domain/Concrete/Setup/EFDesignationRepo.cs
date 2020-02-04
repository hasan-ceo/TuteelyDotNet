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
    public class EFDesignationRepo : IDesignationRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Designation> Designation(string instanceId)
        {
             return context.Designation.Where(d => d.InstanceID== instanceId); 
        }

        public Designation Single(string InstanceID, long ID)
        {
            return context.Designation.Where(a => a.InstanceID == InstanceID && a.DesignationID == ID).FirstOrDefault();
        }

        public void SaveDesignation(Designation Designation)
        {

            if (Designation.DesignationID == 0)
            {
                context.Designation.Add(Designation);
            }
            else
            {
                Designation dbEntry = context.Designation.Find(Designation.DesignationID);
                if (dbEntry != null)
                {
                    dbEntry.DesignationName = Designation.DesignationName;
                    //dbEntry.CountryID = Designation.CountryID;                   
                }
            }
            context.SaveChanges();
        }

        public Designation DeleteDesignation(long DesignationID)
        {
            Designation dbEntry = context.Designation.Find(DesignationID);
            var count = context.Employee.Where(e => e.DesignationID == DesignationID).Count();
            if (dbEntry != null && count==0)
            {
                context.Designation.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long DesignationID)
        {
            return context.Employee.Where(e => e.DesignationID == DesignationID).Count();
        }
    }
}
