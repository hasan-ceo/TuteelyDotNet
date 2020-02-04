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
    public class EFSectionRepo : ISectionRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Section> Section(string instanceId)
        {
             return context.Section.Where(s => s.InstanceID == instanceId); 
        }

        public Section Single(string InstanceID, long ID)
        {
            return context.Section.Where(a => a.InstanceID == InstanceID && a.SectionID == ID).FirstOrDefault();
        }

        public void SaveSection(Section Section)
        {

            if (Section.SectionID == 0)
            {
                context.Section.Add(Section);
            }
            else
            {
                Section dbEntry = context.Section.Find(Section.SectionID);
                if (dbEntry != null)
                {
                    dbEntry.SectionName = Section.SectionName;
                    //dbEntry.CountryID = Section.CountryID;                   
                }
            }
            context.SaveChanges();
        }

        public Section DeleteSection(long SectionID)
        {
            Section dbEntry = context.Section.Find(SectionID);
            var count = context.Employee.Where(e => e.SectionID == SectionID).Count();

            if (dbEntry != null && count==0)
            {
                context.Section.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long SectionID)
        {
            return context.Employee.Where(e => e.SectionID == SectionID).Count();
        }
    }
}
