using System.Collections.Generic;
using Nyika.Domain.Entities.MF;

namespace Nyika.Domain.Abstract.MF
{
    public interface ISchemeRepo
    {
        IEnumerable<Scheme> Scheme(string InstanceID);
        Scheme Single(string InstanceID, long ID);
        void SaveScheme(Scheme Scheme);
        Scheme DeleteScheme(long SchemeID);
        int IsExists(long SchemeID);
    }
}
