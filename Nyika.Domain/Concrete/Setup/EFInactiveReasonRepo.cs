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
    public class EFResignReasonRepo : IResignReasonRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<ResignReason> ResignReason(string instanceId)
        {
            return context.ResignReason.Where(r => r.InstanceID==instanceId); 
        }

        public ResignReason Single(string InstanceID, long ID)
        {
            return context.ResignReason.Where(a => a.InstanceID == InstanceID && a.ResignReasonID == ID).FirstOrDefault();
        }

        public void SaveResignReason(ResignReason ResignReason)
        {

            if (ResignReason.ResignReasonID == 0)
            {
                context.ResignReason.Add(ResignReason);
            }
            else
            {
                ResignReason dbEntry = context.ResignReason.Find(ResignReason.ResignReasonID);
                if (dbEntry != null)
                {
                    dbEntry.ResignReasonName = ResignReason.ResignReasonName;
                    //dbEntry.userId = ResignReason.CountryID;                   
                }
            }
            context.SaveChanges();
        }

        public ResignReason DeleteResignReason(long ResignReasonID)
        {
            ResignReason dbEntry = context.ResignReason.Find(ResignReasonID);
            var count = context.EmployeeResign.Where(e => e.ResignReasonID == ResignReasonID).Count();
            if (dbEntry != null && count==0)
            {
                context.ResignReason.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long ResignReasonID)
        {
            return context.EmployeeResign.Where(e => e.ResignReasonID == ResignReasonID).Count();

        }
    }
}
