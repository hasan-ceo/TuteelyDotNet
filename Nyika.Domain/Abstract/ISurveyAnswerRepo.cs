using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Abstract
{
    public interface ISurveyAnswerRepo
    {
        IEnumerable<SurveyAnswer> SurveyAnswer { get; }
        void SaveSurveyAnswer(SurveyAnswer SurveyAnswer);
        SurveyAnswer DeleteSurveyAnswer(int SurveyAnswerId);
    }
}
