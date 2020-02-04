using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Entities.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.Accounts
{
    public class EFPartyRepo : IPartyRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Party> Party(string InstanceID)
        {
             return context.Party.Where(p => p.InstanceID==InstanceID); 
        }

        public Party Single(string InstanceID, long ID)
        {
            return context.Party.Where(a => a.InstanceID == InstanceID && a.PartyID == ID).FirstOrDefault();
        }

        public void SaveParty(Party Party)
        {

            if (Party.PartyID == 0)
            {
                context.Party.Add(Party);
            }
            else
            {
                Party dbEntry = context.Party.Find(Party.PartyID);
                if (dbEntry != null)
                {
                    //dbEntry.PartyID = Party.PartyID;
                    dbEntry.PartyName = Party.PartyName;
                    dbEntry.Email = Party.Email;
                    dbEntry.ContactNumber = Party.ContactNumber;
                    dbEntry.Address = Party.Address;
                    dbEntry.ZIPCode = Party.ZIPCode;
                }
            }
            context.SaveChanges();
        }

        public Party DeleteParty(long PartyID)
        {
            Party dbEntry = context.Party.Find(PartyID);
            var count = context.AccountGL.Where(e => e.PartyID == PartyID).Count();
            if (dbEntry != null && count==0)
            {
                context.Party.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long PartyID)
        {
            return context.AccountGL.Where(e => e.PartyID == PartyID).Count();

        }
    }
}
