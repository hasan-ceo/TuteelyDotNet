using System.Collections.Generic;
using Nyika.Domain.Entities.Accounts;

namespace Nyika.Domain.Abstract.Accounts
{
    public interface IPartyRepo
    {
        IEnumerable<Party> Party(string InstanceID);
        Party Single(string InstanceID, long ID);
        void SaveParty(Party Party);
        Party DeleteParty(long PartyID);
        int IsExists(long PartyID);
    }
}
