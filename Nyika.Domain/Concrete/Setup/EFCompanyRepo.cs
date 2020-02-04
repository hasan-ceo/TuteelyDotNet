using Nyika.Domain.Abstract.Setup;
using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.Setup
{
    public class EFCompanyRepo : ICompanyRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Company> Company(string InstanceID)
        {
           return context.Company.Where(c =>c.InstanceID==InstanceID); 
        }

        public Company Single(string InstanceID, long ID)
        {
            return context.Company.Where(a => a.InstanceID == InstanceID && a.CompanyID == ID).FirstOrDefault();
        }

        public void SaveCompany(Company Company)
        {

            if (Company.CompanyID == 0)
            {
                context.Company.Add(Company);
            }
            else
            {
                Company dbEntry = context.Company.Find(Company.CompanyID);
                if (dbEntry != null)
                {
                    //dbEntry.CompanyID = Company.CompanyID;
                    dbEntry.CompanyName = Company.CompanyName;
                    dbEntry.Email = Company.Email;
                    dbEntry.ContactNumber = Company.ContactNumber;
                    dbEntry.WebAddress = Company.WebAddress;
                    dbEntry.Address = Company.Address;
                    dbEntry.TIN = Company.TIN;
                    dbEntry.VAT = Company.VAT;
                    dbEntry.Currency = Company.Currency;
                    dbEntry.Stamp = Company.Stamp;
                    dbEntry.PaymentTerms = Company.PaymentTerms;
                    dbEntry.ClientNotes = Company.ClientNotes;
                    dbEntry.WeekOff1 = Company.WeekOff1;
                    dbEntry.WeekOff2 = Company.WeekOff2;
                    dbEntry.SDL = Company.SDL;
                    dbEntry.NSSFPPF = Company.NSSFPPF;
                    dbEntry.HigherStudyLoan = Company.HigherStudyLoan;
                    dbEntry.NHIF = Company.NHIF;
                    dbEntry.PPFEmployerNumber = Company.PPFEmployerNumber;
                    dbEntry.NSSFEmployerNumber = Company.NSSFEmployerNumber;
                    dbEntry.NHIFNumber = Company.NHIFNumber;
                    dbEntry.WCFNumber = Company.WCFNumber;
                    if (Company.ImageUrl != null)
                    {
                        dbEntry.ImageUrl = Company.ImageUrl;
                    }
                }
            }
            context.SaveChanges();
        }


        public void Seed(string InstanceID)
        {
            context.Database.ExecuteSqlCommand("exec SeupSeed @InstanceID={0}", InstanceID);
        }

        public Company DeleteCompany(long CompanyID)
        {
            Company dbEntry = context.Company.Find(CompanyID);
            if (dbEntry != null)
            {
                context.Company.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
