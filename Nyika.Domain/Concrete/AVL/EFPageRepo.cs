using Nyika.Domain.Abstract.AVL;
using Nyika.Domain.Entities.AVL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Nyika.Domain.Concrete.AVL
{
    public class EFPageRepo : IPageRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Page> Page
        {
            get { return context.Page; }
        }

        public void SavePage(Page Page)
        {

            if (Page.PageID == 0)
            {
                context.Page.Add(Page);
            }
            else
            {
                Page dbEntry = context.Page.Find(Page.PageID);
                if (dbEntry != null)
                {
                    dbEntry.PageID = Page.PageID;
                    dbEntry.Description = Page.Description;
                    dbEntry.Slug = Page.Slug;
                }
            }
            context.SaveChanges();
        }

        public Page DeletePage(long PageID)
        {
            Page dbEntry = context.Page.Find(PageID);
            if (dbEntry != null)
            {
                context.Page.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

    }
}
