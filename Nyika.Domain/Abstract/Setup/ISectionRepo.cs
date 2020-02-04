using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Setup
{
    public interface ISectionRepo
    {
        IEnumerable<Section> Section(string InstanceID);
        Section Single(string InstanceID, long ID);
        void SaveSection(Section Section);
        Section DeleteSection(long SectionID);
        int IsExists(long SectionID);
    }
}
