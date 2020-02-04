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
    public class EFErrorLogRepo : IErrorLogRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<ErrorLog> ErrorLog(string instanceId)
        {
             return context.ErrorLog; 
        }

        public ErrorLog Single(string InstanceID, long ID)
        {
            return context.ErrorLog.Where(a => a.ErrorLogID == ID).FirstOrDefault();
        }

        public void SaveErrorLog(ErrorLog ErrorLog)
        {

            if (ErrorLog.ErrorLogID == 0)
            {
                context.ErrorLog.Add(ErrorLog);
            }
            else
            {
                ErrorLog dbEntry = context.ErrorLog.Find(ErrorLog.ErrorLogID);
                if (dbEntry != null)
                {
                    dbEntry.Message = ErrorLog.Message;
                    //dbEntry.CountryID = ErrorLog.CountryID;                   
                }
            }
            context.SaveChanges();
        }

        public ErrorLog DeleteErrorLog(long ErrorLogID)
        {
            ErrorLog dbEntry = context.ErrorLog.Find(ErrorLogID);
            if (dbEntry != null)
            {
                context.ErrorLog.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
