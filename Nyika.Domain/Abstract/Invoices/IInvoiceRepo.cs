using System.Collections.Generic;
using Nyika.Domain.Entities.Invoices;

namespace Nyika.Domain.Abstract.Invoices
{
    public interface IInvoiceRepo
    {
        IEnumerable<Invoice> Invoice(string InstanceID);
        IEnumerable<Invoice> InvoiceUser(string InstanceID, string entryby);
        Invoice Single(string InstanceID, long ID);
        IEnumerable<Invoice> Search(string InstanceID, string txtSearch);
        long SaveInvoice(Invoice Invoice);
        void ApproveInvoice(long InvoiceID, string ApprovedBy);
        Invoice DeleteInvoice(long InvoiceID);
        int DeleteStatus(long InvoiceID);
        int IsExists(string InvoiceNumber);
    }
}
