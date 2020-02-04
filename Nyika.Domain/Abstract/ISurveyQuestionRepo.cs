using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Abstract
{
    public interface ISurveyQuestionRepo
    {
        IEnumerable<SurveyQuestion> SurveyQuestion { get; }
        void SaveSurveyQuestion(SurveyQuestion SurveyQuestion);
        SurveyQuestion DeleteSurveyQuestion(int SurveyQuestionId);
    }
}
