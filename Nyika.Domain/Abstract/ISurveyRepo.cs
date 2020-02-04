using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Abstract
{
    public interface ISurveyRepo
    {
        IEnumerable<Survey> Survey { get; }
        void SaveSurvey(Survey Survey);
        Survey DeleteSurvey(int SurveyId);
    }
}
