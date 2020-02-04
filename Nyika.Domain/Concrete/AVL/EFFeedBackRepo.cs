using Nyika.Domain.Abstract.AVL;
using Nyika.Domain.Entities.AVL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.AVL
{
    public class EFFeedbackRepo : IFeedbackRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Feedback> Feedback
        {
            get { return context.Feedback; }
        }

        public void SaveFeedback(Feedback Feedback)
        {

            if (Feedback.FeedbackID == 0)
            {
                context.Feedback.Add(Feedback);
            }
            else
            {
                Feedback dbEntry = context.Feedback.Find(Feedback.FeedbackID);
                if (dbEntry != null)
                {
                    dbEntry.Email = Feedback.Email;
                    dbEntry.TopicName = Feedback.TopicName;
                    dbEntry.Detailsoferror = Feedback.Detailsoferror;
                    dbEntry.WebsiteLike = Feedback.WebsiteLike;
                    dbEntry.WebsiteNotLike = Feedback.WebsiteNotLike;
                    dbEntry.Suggestions = Feedback.Suggestions;
                    dbEntry.Comments = Feedback.Comments;
                    dbEntry.Purposes = Feedback.Purposes;
                    dbEntry.UserType = Feedback.UserType;
                    dbEntry.VisitFrequency = Feedback.VisitFrequency;
                    dbEntry.EntryDate = Feedback.EntryDate;
                }
            }
            context.SaveChanges();
        }

        public Feedback DeleteFeedback(long FeedbackID)
        {
            Feedback dbEntry = context.Feedback.Find(FeedbackID);
            if (dbEntry != null)
            {
                context.Feedback.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

    }
}
