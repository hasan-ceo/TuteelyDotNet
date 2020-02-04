using Nyika.Domain.Abstract.MF;
using Nyika.Domain.Entities.MF;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.MF
{
    public class EFProductRepo : IProductRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Product(string InstanceID)
        {
            return context.Product.Include(b => b.Project).Where(b => b.InstanceID == InstanceID);
        }

        public IEnumerable<Product> ProductProjectWise(string InstanceID, long ProjectID)
        {
            return context.Product.Include(b => b.Project).Where(b => b.InstanceID == InstanceID && b.ProjectID == ProjectID);
        }

        public Product Single(string InstanceID, long ID)
        {
            return context.Product.Include(b => b.Project).Where(a => a.InstanceID == InstanceID && a.ProductID == ID).FirstOrDefault();
        }

        public void SaveProduct(Product Product)
        {

            if (Product.ProductID == 0)
            {
                context.Product.Add(Product);
            }
            else
            {
                Product dbEntry = context.Product.Find(Product.ProductID);
                if (dbEntry != null)
                {
                    //dbEntry.ProductID = Product.ProductID;
                    dbEntry.ProductName = Product.ProductName;
                    dbEntry.ProjectID = Product.ProjectID;
                    dbEntry.InterestRate = Product.InterestRate;
                    dbEntry.InterestRateType = Product.InterestRateType;
                    dbEntry.IntFactor = Product.IntFactor;
                    dbEntry.Duration = Product.Duration;
                    dbEntry.NoOfInstallment = Product.NoOfInstallment;
                    dbEntry.Inactive = Product.Inactive;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(long ProductID)
        {
            Product dbEntry = context.Product.Find(ProductID);
            var count = context.Loan.Where(e => e.ProductID == ProductID).Count();
            if (dbEntry != null && count == 0)
            {
                context.Product.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long ProductID)
        {
            return context.Loan.Where(e => e.ProductID == ProductID).Count();

        }

        public void ProductChange(long SelectedLoan, long SelectedProduct)
        {
            context.Database.ExecuteSqlCommand("exec pMFLoanProductChange @LoanId={0},@ProductId={1}", SelectedLoan, SelectedProduct);
        }
    }
}
