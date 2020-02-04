using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Abstract
{
    public interface ITrainingRepo
    {
        IEnumerable<Training> Training { get; }
        void SaveTraining(Training Training);
        Training DeleteTraining(int TrainingId);
    }
}
