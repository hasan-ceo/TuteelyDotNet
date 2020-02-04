using HRMSMvc.Domain.Abstract;
using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Concrete
{
    public class EFSurveyQuestionRepo : ISurveyQuestionRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<SurveyQuestion> SurveyQuestion
        {
            get { return context.SurveyQuestion; }
        }

        public void SaveSurveyQuestion(SurveyQuestion SurveyQuestion)
        {

            if (SurveyQuestion.SurveyQuestionID == 0)
            {
                context.SurveyQuestion.Add(SurveyQuestion);
            }
            else
            {
                SurveyQuestion dbEntry = context.SurveyQuestion.Find(SurveyQuestion.SurveyQuestionID);
                if (dbEntry != null)
                {
                    dbEntry.SurveyID = SurveyQuestion.SurveyID;
                    dbEntry.Question = SurveyQuestion.Question;
                    dbEntry.Option01 = SurveyQuestion.Option01;
                    dbEntry.Option02 = SurveyQuestion.Option02;
                    dbEntry.Option03 = SurveyQuestion.Option03;
                    dbEntry.Option04 = SurveyQuestion.Option04;
                    dbEntry.Option05 = SurveyQuestion.Option05;
                    dbEntry.Option06 = SurveyQuestion.Option06;
                    dbEntry.Option07 = SurveyQuestion.Option07;
                    dbEntry.Option08 = SurveyQuestion.Option08;
                    dbEntry.Option09 = SurveyQuestion.Option09;
                    dbEntry.Option10 = SurveyQuestion.Option10;
                }
            }
            context.SaveChanges();
        }

        public SurveyQuestion DeleteSurveyQuestion(int SurveyQuestionID)
        {
            SurveyQuestion dbEntry = context.SurveyQuestion.Find(SurveyQuestionID);
            if (dbEntry != null)
            {
                context.SurveyQuestion.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
