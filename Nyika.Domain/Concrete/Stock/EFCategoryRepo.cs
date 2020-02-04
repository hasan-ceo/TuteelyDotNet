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
    public class EFCategoryRepo : ICategoryRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Category> Category
        {
            get { return context.Category; }
        }

        public Category Single(long CategoryID)
        {
            return context.Category.Where(a => a.CategoryID == CategoryID).FirstOrDefault();
        }

        public void SaveCategory(Category Category)
        {

            if (Category.CategoryID == 0)
            {
                context.Category.Add(Category);
            }
            else
            {
                Category dbEntry = context.Category.Find(Category.CategoryID);
                if (dbEntry != null)
                {
                    dbEntry.CategoryName = Category.CategoryName;
                    dbEntry.CategoryNameLocal = Category.CategoryNameLocal;
                    dbEntry.UrlLink = Category.UrlLink;
                    dbEntry.HeaderUrl = Category.HeaderUrl;
                    dbEntry.LogoUrl = Category.LogoUrl;
                }
            }
            context.SaveChanges();
        }

        public Category DeleteCategory(long CategoryID)
        {
            Category dbEntry = context.Category.Find(CategoryID);
            var count = context.CategorySub.Where(e => e.CategoryID == CategoryID).Count();
            if (dbEntry != null && count==0)
            {
                context.Category.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long CategoryID)
        {
            return context.CategorySub.Where(e => e.CategoryID == CategoryID).Count();
        }
    }
}
