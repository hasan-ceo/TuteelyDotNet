using Nyika.Domain.Entities.AVL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.AVL
{
    public interface IFeedbackRepo
    {
        IEnumerable<Feedback> Feedback { get; }
        void SaveFeedback(Feedback Feedback);
        Feedback DeleteFeedback(long FeedbackID);

    }
}
