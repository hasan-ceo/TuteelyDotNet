using System.Collections.Generic;
using Nyika.Domain.Entities.MF;

namespace Nyika.Domain.Abstract.MF
{
    public interface IMemberRepo
    {
        IEnumerable<Member> Member(string InstanceID);
        Member Single(string InstanceID, long ID);
        IEnumerable<Member> Search(string InstanceID, string txtSearch, bool loan);
        void SaveMember(Member Member);
        Member DeleteMember(long MemberID);
        int IsExists(long MemberID);
        long NewMemberNo(long GroupsID);
        //IEnumerable<Member> SingleToList(string InstanceID, long ID);
    }
}
