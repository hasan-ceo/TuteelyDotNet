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
    public class EFShopPageRepo : IShopPageRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<ShopPage> ShopPage
        {
            get { return context.ShopPage; }
        }

        public void SaveShopPage(ShopPage ShopPage)
        {

            if (ShopPage.ShopPageID == 0)
            {
                context.ShopPage.Add(ShopPage);
            }
            else
            {
                ShopPage dbEntry = context.ShopPage.Find(ShopPage.ShopPageID);
                if (dbEntry != null)
                {
                    dbEntry.ShopPageID = ShopPage.ShopPageID;
                    dbEntry.Description = ShopPage.Description;
                    dbEntry.DescriptionLocal = ShopPage.DescriptionLocal;
                    dbEntry.Slug = ShopPage.Slug;
                }
            }
            context.SaveChanges();
        }

        public ShopPage DeleteShopPage(long ShopPageID)
        {
            ShopPage dbEntry = context.ShopPage.Find(ShopPageID);
            if (dbEntry != null)
            {
                context.ShopPage.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

    }
}
