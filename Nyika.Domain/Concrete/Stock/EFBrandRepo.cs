using Nyika.Domain.Abstract.Stock;
using Nyika.Domain.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.Stock
{
    public class EFBrandRepo : IBrandRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Brand> Brand
        {
            get { return context.Brand; }
        }

        public Brand Single(long BrandID)
        {
            return context.Brand.Where(a => a.BrandID == BrandID).FirstOrDefault();
        }

        public void SaveBrand(Brand Brand)
        {

            if (Brand.BrandID == 0)
            {
                context.Brand.Add(Brand);
            }
            else
            {
                Brand dbEntry = context.Brand.Find(Brand.BrandID);
                if (dbEntry != null)
                {
                    dbEntry.BrandName = Brand.BrandName;
                    dbEntry.BrandNameLocal = Brand.BrandNameLocal;
                    //dbEntry.CountryID = Brand.CountryID;                   
                }
            }
            context.SaveChanges();
        }

        public Brand DeleteBrand(long BrandID)
        {
            Brand dbEntry = context.Brand.Find(BrandID);
            var count =0;
            if (dbEntry != null && count==0)
            {
                context.Brand.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long BrandID)
        {
            return 0;
        }
    }
}
