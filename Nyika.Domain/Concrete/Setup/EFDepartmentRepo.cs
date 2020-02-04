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
    public class EFDepartmentRepo : IDepartmentRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Department> Department(string InstanceID)
        {
            return context.Department.Where(d => d.InstanceID == InstanceID); 
        }

        public Department Single(string InstanceID, long ID)
        {
            return context.Department.Where(a => a.InstanceID == InstanceID && a.DepartmentID == ID).FirstOrDefault();
        }

        public void SaveDepartment(Department Department)
        {

            if (Department.DepartmentID == 0)
            {
                context.Department.Add(Department);
            }
            else
            {
                Department dbEntry = context.Department.Find(Department.DepartmentID);
                if (dbEntry != null)
                {
                    dbEntry.DepartmentName = Department.DepartmentName;
                    //dbEntry.CountryID = Department.CountryID;                   
                }
            }
            context.SaveChanges();
        }

        public Department DeleteDepartment(long DepartmentID)
        {
            Department dbEntry = context.Department.Find(DepartmentID);
            var count = context.Employee.Where(e => e.DepartmentID == DepartmentID).Count();
            if (dbEntry != null && count==0)
            {
                context.Department.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long DepartmentID)
        {
            return context.Employee.Where(e => e.DepartmentID == DepartmentID).Count();
        }
    }
}
