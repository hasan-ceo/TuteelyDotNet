using System.Collections.Generic;
using Nyika.Domain.Entities.MF;

namespace Nyika.Domain.Abstract.MF
{
    public interface IProjectRepo
    {
        IEnumerable<Project> Project(string InstanceID);
        Project Single(string InstanceID, long ID);
        void SaveProject(Project Project);
        Project DeleteProject(long ProjectID);
        int IsExists(long ProjectID);
    }
}
