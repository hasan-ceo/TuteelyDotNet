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
    public class EFCategorySubRepo : ICategorySubRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<CategorySub> CategorySub
        {
            get { return context.CategorySub.Include(a => a.Category); }
        }

        public CategorySub Single(long CategorySubID)
        {
            return context.CategorySub.Include(a => a.Category).Where(a => a.CategorySubID == CategorySubID).FirstOrDefault();
        }
        
        public void SaveCategorySub(CategorySub CategorySub)
        {

            if (CategorySub.CategorySubID == 0)
            {
                context.CategorySub.Add(CategorySub);
            }
            else
            {
                CategorySub dbEntry = context.CategorySub.Find(CategorySub.CategorySubID);
                if (dbEntry != null)
                {
                    dbEntry.CategorySubName = CategorySub.CategorySubName;
                    dbEntry.CategorySubNameLocal = CategorySub.CategorySubNameLocal;
                    dbEntry.UrlLink = CategorySub.UrlLink;
                    dbEntry.CategoryID = CategorySub.CategoryID;
                    dbEntry.HeaderUrl = CategorySub.HeaderUrl;
                    dbEntry.LogoUrl = CategorySub.LogoUrl;
                }
            }
            context.SaveChanges();
        }

        public CategorySub DeleteCategorySub(long CategorySubID)
        {
            CategorySub dbEntry = context.CategorySub.Find(CategorySubID);
            var count = context.Item.Where(e => e.CategorySubID == CategorySubID).Count();
            if (dbEntry != null && count==0)
            {
                context.CategorySub.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long CategorySubID)
        {
            return context.Item.Where(e => e.CategorySubID == CategorySubID).Count();
        }
    }
}
