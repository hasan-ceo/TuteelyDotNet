using System.Collections.Generic;
using Nyika.Domain.Entities.MF;
using Nyika.Domain.Entities.Setup;

namespace Nyika.Domain.Abstract.MF
{
    public interface IGroupsRepo
    {
        IEnumerable<Groups> Groups(string InstanceID);
        Groups Single(string InstanceID, long ID);
        void SaveGroups(Groups Groups);
        Groups DeleteGroups(long GroupsID);
        int IsExists(long GroupsID);
        int IsCollectionDay(string InstanceID, Weekdays ColDay);
        IEnumerable<Groups> GroupsToday(string InstanceID, long ProjectID);
    }
}
