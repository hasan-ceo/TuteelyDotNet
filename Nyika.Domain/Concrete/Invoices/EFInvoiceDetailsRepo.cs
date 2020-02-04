using Nyika.Domain.Abstract.Invoices;
using Nyika.Domain.Entities.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.Invoices
{
    public class EFInvoiceDetailsRepo : IInvoiceDetailsRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<InvoiceDetails> Single(string InstanceID, long ID)
        {
            return context.InvoiceDetails.Where(a => a.InvoiceID == ID).ToList();
        }


        public void SaveInvoiceDetails(InvoiceDetails InvoiceDetails)
        {
            var found = context.InvoiceDetails.Where(i => i.InvoiceID == InvoiceDetails.InvoiceID && i.Item == InvoiceDetails.Item && i.Description == InvoiceDetails.Description && i.Cost == InvoiceDetails.Cost && i.Quantity == InvoiceDetails.Quantity).FirstOrDefault();
            if (found == null)
            {
                context.InvoiceDetails.Add(InvoiceDetails);
            }
            context.SaveChanges();
           
        }

        public void DeleteInvoiceDetails(long InvoiceID)
        {
            context.InvoiceDetails.RemoveRange(context.InvoiceDetails.Where(c => c.InvoiceID == InvoiceID));
        }
    }
}
