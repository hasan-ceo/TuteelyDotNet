using Nyika.Domain.Abstract.MF;
using Nyika.Domain.Entities.MF;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.MF
{
    public class EFSchemeRepo : ISchemeRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Scheme> Scheme(string InstanceID)
        {
             return context.Scheme.Where(b => b.InstanceID==InstanceID); 
        }

        public Scheme Single(string InstanceID, long ID)
        {
            return context.Scheme.Where(a => a.InstanceID == InstanceID && a.SchemeID == ID).FirstOrDefault();
        }

        public void SaveScheme(Scheme Scheme)
        {

            if (Scheme.SchemeID == 0)
            {
                context.Scheme.Add(Scheme);
            }
            else
            {
                Scheme dbEntry = context.Scheme.Find(Scheme.SchemeID);
                if (dbEntry != null)
                {
                    //dbEntry.SchemeID = Scheme.SchemeID;
                    dbEntry.SchemeName = Scheme.SchemeName;
                   
                }
            }
            context.SaveChanges();
        }

        public Scheme DeleteScheme(long SchemeID)
        {
            Scheme dbEntry = context.Scheme.Find(SchemeID);
            var count = context.Loan.Where(e => e.SchemeID == SchemeID).Count();
            if (dbEntry != null && count == 0)
            {
                context.Scheme.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long SchemeID)
        {
            return context.Loan.Where(e => e.SchemeID == SchemeID).Count();

        }
    }
}
