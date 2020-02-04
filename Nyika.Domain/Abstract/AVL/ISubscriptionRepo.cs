using Nyika.Domain.Entities.AVL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.AVL
{
    public interface ISubscriptionRepo
    {
        IEnumerable<Subscription> Subscription { get; }
        void SaveSubscription(Subscription Subscription);
        Subscription DeleteSubscription(long SubscriptionID);

    }
}
