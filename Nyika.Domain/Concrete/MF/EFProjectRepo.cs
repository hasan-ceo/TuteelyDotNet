using Nyika.Domain.Abstract.MF;
using Nyika.Domain.Entities.MF;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.MF
{
    public class EFProjectRepo : IProjectRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Project> Project(string InstanceID)
        {
             return context.Project.Where(b => b.InstanceID==InstanceID); 
        }

        public Project Single(string InstanceID, long ID)
        {
            return context.Project.Where(a => a.InstanceID == InstanceID && a.ProjectID == ID).FirstOrDefault();
        }

        public void SaveProject(Project Project)
        {

            if (Project.ProjectID == 0)
            {
                context.Project.Add(Project);
            }
            else
            {
                Project dbEntry = context.Project.Find(Project.ProjectID);
                if (dbEntry != null)
                {
                    dbEntry.ProjectName = Project.ProjectName;
                    var count = context.Loan.Where(e => e.Member.Groups.Project.ProjectID == dbEntry.ProjectID && e.InstanceID == dbEntry.InstanceID).Count();
                    if (count == 0)
                    {
                        dbEntry.StartDate = Project.StartDate;
                        dbEntry.FirstDisbursementDate = Project.FirstDisbursementDate;
                    }
                    var countNotClose = context.Loan.Where(e => e.Member.Groups.Project.ProjectID == dbEntry.ProjectID && e.InstanceID == dbEntry.InstanceID && e.LoanStatus!="Close").Count();
                    if (countNotClose == 0)
                    {
                        dbEntry.Inactive = Project.Inactive;
                    }

                    var c=context.LoanSchedule.Where(e => e.Loan.Member.Groups.Project.ProjectID == dbEntry.ProjectID && e.InstanceID == dbEntry.InstanceID && e.Loan.LoanStatus != "Close").Count();
                    if (c == 0)
                    {
                        dbEntry.EndDate = Project.EndDate;
                    }
                    else
                    {
                        var MaxScheduleDate = context.LoanSchedule.Where(e => e.Loan.Member.Groups.Project.ProjectID == dbEntry.ProjectID && e.InstanceID == dbEntry.InstanceID && e.Loan.LoanStatus != "Close").Max(e => e.DueDate);
                        if (Project.EndDate >= MaxScheduleDate)
                        {
                            dbEntry.EndDate = Project.EndDate;
                        }
                    }
                }
            }
            context.SaveChanges();
        }

        public Project DeleteProject(long ProjectID)
        {
            Project dbEntry = context.Project.Find(ProjectID);
            var count = context.Groups.Where(e => e.ProjectID == ProjectID && e.InstanceID==dbEntry.InstanceID).Count();
            if (count == 0)
            {
                count = context.Product.Where(e => e.ProjectID == ProjectID && e.InstanceID == dbEntry.InstanceID).Count();
                if (dbEntry != null && count == 0)
                {
                context.Project.Remove(dbEntry);
                context.SaveChanges();
                }
            }
            return dbEntry;
        }

        public int IsExists(long ProjectID)
        {
            Project dbEntry = context.Project.Find(ProjectID);
            var count = context.Groups.Where(e => e.ProjectID == ProjectID && e.InstanceID == dbEntry.InstanceID).Count();
            if (count == 0)
            {
                count = context.Product.Where(e => e.ProjectID == ProjectID && e.InstanceID == dbEntry.InstanceID).Count();
            }
            return count;

        }
    }
}
