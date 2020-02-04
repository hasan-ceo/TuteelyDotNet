using System.Collections.Generic;
using Nyika.Domain.Entities.Invoices;

namespace Nyika.Domain.Abstract.Invoices
{
    public interface IInvoiceDetailsRepo
    {
        IEnumerable<InvoiceDetails> Single(string InstanceID, long ID);
        void SaveInvoiceDetails(InvoiceDetails InvoiceDetails);
        void DeleteInvoiceDetails(long InvoiceID);
    }
}
