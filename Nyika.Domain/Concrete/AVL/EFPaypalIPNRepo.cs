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
    public class EFPaypalIPNRepo : IPaypalIPNRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<PaypalIPN> PaypalIPN
        {
            get { return context.PaypalIPN; }
        }


        public void CSActive(string Custom, string TXNID,string ItemName)
        {
            context.Database.ExecuteSqlCommand("exec pAVLCompanyStatusActive @Custom={0}, @TXN_ID={1}, @ItemName={2}", Custom, TXNID, ItemName);
        }

        public void SavePaypalIPN(PaypalIPN PaypalIPN)
        {

            if (PaypalIPN.PaypalIPNID == 0)
            {
                context.PaypalIPN.Add(PaypalIPN);
            }
            else
            {
                PaypalIPN dbEntry = context.PaypalIPN.Find(PaypalIPN.PaypalIPNID);
                if (dbEntry != null)
                {
                    dbEntry.PaypalIPNID = PaypalIPN.PaypalIPNID;
                    dbEntry.PaymentType = PaypalIPN.PaymentType;
                    dbEntry.PaymentDate = PaypalIPN.PaymentDate;
                    dbEntry.PaymentStatus = PaypalIPN.PaymentStatus;
                    dbEntry.PendingReason = PaypalIPN.PendingReason;
                    dbEntry.PayerAddressStatus = PaypalIPN.PayerAddressStatus;
                    dbEntry.PayerStatus = PaypalIPN.PayerStatus;
                    dbEntry.PayerFirstName = PaypalIPN.PayerFirstName;
                    dbEntry.PayerLastName = PaypalIPN.PayerLastName;
                    dbEntry.PayerEmail = PaypalIPN.PayerEmail;
                    dbEntry.PayerID = PaypalIPN.PayerID;
                    dbEntry.PayerCountry = PaypalIPN.PayerCountry;
                    dbEntry.PayerCountryCode = PaypalIPN.PayerCountryCode;
                    dbEntry.PayerZipCode = PaypalIPN.PayerZipCode;
                    dbEntry.PayerState = PaypalIPN.PayerState;
                    dbEntry.PayerCity = PaypalIPN.PayerCity;
                    dbEntry.PayerStreet = PaypalIPN.PayerStreet;
                    dbEntry.Business = PaypalIPN.Business;
                    dbEntry.ReceiverEmail = PaypalIPN.ReceiverEmail;
                    dbEntry.ReceiverID = PaypalIPN.ReceiverID;
                    dbEntry.ItemName = PaypalIPN.ItemName;
                    dbEntry.ItemNumber = PaypalIPN.ItemNumber;
                    dbEntry.Quantity = PaypalIPN.Quantity;
                    dbEntry.Shipping = PaypalIPN.Shipping;
                    dbEntry.Tax = PaypalIPN.Tax;
                    dbEntry.Currency = PaypalIPN.Currency;
                    dbEntry.PaymentFee = PaypalIPN.PaymentFee;
                    dbEntry.PaymentGross = PaypalIPN.PaymentGross;
                    dbEntry.TXN_ID = PaypalIPN.TXN_ID;
                    dbEntry.TXN_Type = PaypalIPN.TXN_Type;
                    dbEntry.NotifyVersion = PaypalIPN.NotifyVersion;
                    dbEntry.Custom = PaypalIPN.Custom;
                    dbEntry.Invoice = PaypalIPN.Invoice;
                    dbEntry.VerifySign = PaypalIPN.VerifySign;
                    dbEntry.QuantityCartItems = PaypalIPN.QuantityCartItems;

                }
            }
            context.SaveChanges();
        }

        public PaypalIPN DeletePaypalIPN(long PaypalIPNID)
        {
            PaypalIPN dbEntry = context.PaypalIPN.Find(PaypalIPNID);
            if (dbEntry != null)
            {
                context.PaypalIPN.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
