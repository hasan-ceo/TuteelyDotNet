using Nyika.Domain.Abstract.Invoices;
using Nyika.Domain.Entities.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.Invoices
{
    public class EFInvoiceRepo : IInvoiceRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Invoice> Invoice(string InstanceID)
        {
            return context.Invoice.Where(b => b.InstanceID == InstanceID);
        }

        public IEnumerable<Invoice> InvoiceUser(string InstanceID, string entryby)
        {
            return context.Invoice.Where(b => b.InstanceID == InstanceID && b.EntryBy == entryby);
        }

        public Invoice Single(string InstanceID, long ID)
        {
            return context.Invoice.Where(a => a.InstanceID == InstanceID && a.InvoiceID == ID).FirstOrDefault();
        }

        //public long NewInvoiceNumber(string InstanceID)
        //{
        //    return Convert.ToInt64(context.Invoice.Where(a => a.InstanceID == InstanceID).Max( a => a.InvoiceNumber))+1;
        //}

        public IEnumerable<Invoice> Search(string InstanceID, string txtSearch)
        {
            return context.Invoice.Where(e => (e.InvoiceNumber.Contains(txtSearch) || e.InvoiceNumber.Contains(txtSearch)) && e.InstanceID == InstanceID).OrderBy(e => e.WorkDate);
        }

        public long SaveInvoice(Invoice Invoice)
        {

            if (Invoice.InvoiceID == 0)
            {
                context.Invoice.Add(Invoice);
            }
            else
            {
                Invoice dbEntry = context.Invoice.Find(Invoice.InvoiceID);
                var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
                if (dbEntry != null && dbEntry.WorkDate == wd)
                {
                    //dbEntry.InvoiceID = Invoice.InvoiceID;
                    dbEntry.InvoiceNumber = Invoice.InvoiceNumber;
                    dbEntry.CompanyID = Invoice.CompanyID;
                    dbEntry.PartyID = Invoice.PartyID;
                    dbEntry.InvoiceDate = Invoice.InvoiceDate;
                    dbEntry.DueDate = Invoice.DueDate;
                    dbEntry.JobDescription = Invoice.JobDescription;
                    dbEntry.SubTotal = Invoice.SubTotal;
                    dbEntry.Discount = Invoice.Discount;
                    dbEntry.TaxPercent = Invoice.TaxPercent;
                    dbEntry.Tax = Invoice.Tax;
                    dbEntry.InvoiceTotal = Invoice.InvoiceTotal;
                    dbEntry.AmountPaid = Invoice.AmountPaid;
                    dbEntry.DueAmount = Invoice.DueAmount;
                    dbEntry.Currency = Invoice.Currency;
                    dbEntry.Stamp = Invoice.Stamp;
                    dbEntry.PaymentTerms = Invoice.PaymentTerms;
                    dbEntry.ClientNotes = Invoice.ClientNotes;
                    dbEntry.PaymentComplete = Invoice.PaymentComplete;
                    dbEntry.WorkDate = Invoice.WorkDate;
                    dbEntry.EntryBy = Invoice.EntryBy;
                    dbEntry.InstanceID = Invoice.InstanceID;
                }
            }
            context.SaveChanges();
            return Invoice.InvoiceID;
        }

        public void ApproveInvoice(long InvoiceID, string ApprovedBy)
        {

            if (InvoiceID != 0)
            {
                Invoice dbEntry = context.Invoice.Find(InvoiceID);
                var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
                if (dbEntry != null && dbEntry.PaymentComplete == false)
                {
                    //dbEntry.InvoiceID = Invoice.InvoiceID;
                    //dbEntry.InvoiceNumber = Invoice.InvoiceNumber;
                    //dbEntry.PartyID = Invoice.PartyID;
                    //dbEntry.InvoiceDate = Invoice.InvoiceDate;
                    //dbEntry.DueDate = Invoice.DueDate;
                    //dbEntry.JobDescription = Invoice.JobDescription;
                }
            }
            context.SaveChanges();
        }

        public Invoice DeleteInvoice(long InvoiceID)
        {
            Invoice dbEntry = context.Invoice.Find(InvoiceID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
            if (dbEntry != null && dbEntry.WorkDate == wd)
            {
                context.Invoice.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int DeleteStatus(long InvoiceID)
        {
            Invoice dbEntry = context.Invoice.Find(InvoiceID);
            var wd = context.BusinessDay.Where(b => b.DayClose == false && b.InstanceID == dbEntry.InstanceID).FirstOrDefault().WorkDate;
            if (dbEntry != null && dbEntry.WorkDate == wd && dbEntry.PaymentComplete == false)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int IsExists(string InvoiceNumber)
        {
            return context.Invoice.Where(e => e.InvoiceNumber == InvoiceNumber).Count();
        }
    }
}
