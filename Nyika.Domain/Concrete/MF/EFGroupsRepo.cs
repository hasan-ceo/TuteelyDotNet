using Nyika.Domain.Abstract.MF;
using Nyika.Domain.Entities.MF;
using Nyika.Domain.Entities.Setup;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.MF
{
    public class EFGroupsRepo : IGroupsRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Groups> Groups(string InstanceID)
        {
            return context.Groups.Include(b => b.Project).Where(b => b.InstanceID == InstanceID).OrderByDescending(b => b.GroupsID);
        }

        public IEnumerable<Groups> GroupsToday(string InstanceID,long ProjectID)
        {
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == InstanceID).FirstOrDefault().WorkDate;
            int d = (int)wd.DayOfWeek+1;
            return context.Groups.Include(b => b.Project).Where(b => b.InstanceID == InstanceID && b.ProjectID == ProjectID && b.Inactive == false && (int)b.ColDay==d).OrderBy(b => b.GroupsID);
        }

        public Groups Single(string InstanceID, long ID)
        {
            return context.Groups.Include(b => b.Project).Where(a => a.InstanceID == InstanceID && a.GroupsID == ID).FirstOrDefault();
        }

        public void SaveGroups(Groups Groups)
        {

            if (Groups.GroupsID == 0)
            {
                context.Groups.Add(Groups);
            }
            else
            {
                Groups dbEntry = context.Groups.Find(Groups.GroupsID);
                if (dbEntry != null)
                {
                    if (context.Loan.Where(l => l.Member.Groups.GroupsID == Groups.GroupsID).Count()==0)
                    {
                        dbEntry.CreateDate = Groups.CreateDate;
                        dbEntry.ColDay = Groups.ColDay;
                        dbEntry.ColStartDate = Groups.ColStartDate;
                        dbEntry.Frequency = Groups.Frequency;
                        //dbEntry.GroupsStatus = Groups.GroupsStatus;
                    }
                    //dbEntry.GroupsID = Groups.GroupsID;
                    dbEntry.GroupsName = Groups.GroupsName;
                    dbEntry.ProjectID = Groups.ProjectID;
                    dbEntry.Gender = Groups.Gender;
                    dbEntry.EmployeeIdFC = Groups.EmployeeIdFC;
                    dbEntry.EmployeeIdCC = Groups.EmployeeIdCC;
                }
            }
            context.SaveChanges();
        }

        public Groups DeleteGroups(long GroupsID)
        {
            Groups dbEntry = context.Groups.Find(GroupsID);
            var count = context.AccountGL.Where(e => e.GroupsID == GroupsID).Count();
            if (dbEntry != null && count == 0)
            {
                context.Groups.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long GroupsID)
        {
            return context.Member.Where(e => e.GroupsID == GroupsID).Count();

        }

        public int IsCollectionDay(string InstanceID, Weekdays ColDay)
        {
            return context.Company.Where(c => c.WeekOff1 == ColDay || c.WeekOff2 == ColDay).Count();
        }
    }
}
