using HRMSMvc.Domain.Abstract;
using HRMSMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSMvc.Domain.Concrete
{
    public class EFTrainingRepo : ITrainingRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Training> Training
        {
            get { return context.Training; }
        }

        public void SaveTraining(Training Training)
        {

            if (Training.TrainingID == 0)
            {
                context.Training.Add(Training);
            }
            else
            {
                Training dbEntry = context.Training.Find(Training.TrainingID);
                if (dbEntry != null)
                {
                    dbEntry.TrainingName = Training.TrainingName;
                    dbEntry.TrainingType = Training.TrainingType;
                    //dbEntry.CountryID = Training.CountryID;                   
                }
            }
            context.SaveChanges();
        }

        public Training DeleteTraining(int TrainingID)
        {
            Training dbEntry = context.Training.Find(TrainingID);
            if (dbEntry != null)
            {
                context.Training.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
