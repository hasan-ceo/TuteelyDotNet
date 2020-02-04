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
    public class EFSubscriptionRepo : ISubscriptionRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Subscription> Subscription
        {
            get { return context.Subscription; }
        }

        public void SaveSubscription(Subscription Subscription)
        {

            if (Subscription.SubscriptionID == 0)
            {
                context.Subscription.Add(Subscription);
            }
            else
            {
                Subscription dbEntry = context.Subscription.Find(Subscription.SubscriptionID);
                if (dbEntry != null)
                {
                    dbEntry.SubscriptionID = Subscription.SubscriptionID;
                    dbEntry.UserEmail = Subscription.UserEmail;
                    dbEntry.PayerEmail = Subscription.PayerEmail;
                    dbEntry.PayerName = Subscription.PayerName;
                    dbEntry.SubscriptionType = Subscription.SubscriptionType;
                    dbEntry.SubscriptionDate = Subscription.SubscriptionDate;
                    dbEntry.ExpairDate = Subscription.ExpairDate;
                    dbEntry.PaypalTransactionID = Subscription.PaypalTransactionID;
                    dbEntry.Amount = Subscription.Amount;
                }
            }
            context.SaveChanges();
        }

        public Subscription DeleteSubscription(long SubscriptionID)
        {
            Subscription dbEntry = context.Subscription.Find(SubscriptionID);
            if (dbEntry != null)
            {
                context.Subscription.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

    }
}
