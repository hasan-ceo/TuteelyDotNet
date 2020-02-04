using HRMSMvc.Domain.Abstract;
using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Concrete
{
    public class EFSurveyRepo : ISurveyRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Survey> Survey
        {
            get { return context.Survey; }
        }

        public void SaveSurvey(Survey Survey)
        {

            if (Survey.SurveyID == 0)
            {
                context.Survey.Add(Survey);
            }
            else
            {
                Survey dbEntry = context.Survey.Find(Survey.SurveyID);
                if (dbEntry != null)
                {
                    dbEntry.SurveyName = Survey.SurveyName;
                    dbEntry.PublicLink = Survey.PublicLink;
                }
            }
            context.SaveChanges();
        }

        public Survey DeleteSurvey(int SurveyID)
        {
            Survey dbEntry = context.Survey.Find(SurveyID);
            if (dbEntry != null)
            {
                context.Survey.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
