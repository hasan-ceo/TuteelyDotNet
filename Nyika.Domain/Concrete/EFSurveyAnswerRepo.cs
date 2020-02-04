using HRMSMvc.Domain.Abstract;
using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Concrete
{
    public class EFSurveyAnswerRepo : ISurveyAnswerRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<SurveyAnswer> SurveyAnswer
        {
            get { return context.SurveyAnswer; }
        }

        public void SaveSurveyAnswer(SurveyAnswer SurveyAnswer)
        {

            if (SurveyAnswer.SurveyAnswerID == 0)
            {
                context.SurveyAnswer.Add(SurveyAnswer);
            }
            else
            {
                SurveyAnswer dbEntry = context.SurveyAnswer.Find(SurveyAnswer.SurveyAnswerID);
                if (dbEntry != null)
                {
                    dbEntry.SurveyID = SurveyAnswer.SurveyID;
                    dbEntry.Question = SurveyAnswer.Question;
                    dbEntry.Answer = SurveyAnswer.Answer;
                    dbEntry.PIN = SurveyAnswer.PIN;
                }
            }
            context.SaveChanges();
        }

        public SurveyAnswer DeleteSurveyAnswer(int SurveyAnswerID)
        {
            SurveyAnswer dbEntry = context.SurveyAnswer.Find(SurveyAnswerID);
            if (dbEntry != null)
            {
                context.SurveyAnswer.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
