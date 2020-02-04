using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Setup
{
    public interface ICompanyRepo
    {
        IEnumerable<Company> Company(string InstanceID);
        Company Single(string InstanceID, long ID);
        void SaveCompany(Company Company);
        void Seed(string InstanceID);
        Company DeleteCompany(long CompanyId);
    }
}
