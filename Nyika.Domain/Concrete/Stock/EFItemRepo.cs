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
    public class EFItemRepo : IItemRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Item> Item
        {
            get { return context.Item.Include(a => a.CategorySub); }
        }

        public Item Single(long ItemID)
        {
            return context.Item.Include(a => a.CategorySub).Where(a => a.ItemID == ItemID).FirstOrDefault();
        }

        public IEnumerable<Item> Search(string txtSearch)
        {

                return context.Item.Include(g => g.CategorySub).Where(g => g.ItemNo.Contains(txtSearch) || g.ItemName.Contains(txtSearch) || g.ItemNameLocal.Contains(txtSearch) || g.Keywords.Contains(txtSearch) || g.Description.Contains(txtSearch) || g.DescriptionLocal.Contains(txtSearch) || g.CategorySub.CategorySubName.Contains(txtSearch) || g.CategorySub.CategorySubNameLocal.Contains(txtSearch) || g.CategorySub.Category.CategoryName.Contains(txtSearch) || g.CategorySub.Category.CategoryNameLocal.Contains(txtSearch));
        }

        public void SaveItem(Item Item)
        {

            if (Item.ItemID == 0)
            {
                context.Item.Add(Item);
            }
            else
            {
                Item dbEntry = context.Item.Find(Item.ItemID);
                if (dbEntry != null)
                {
                    dbEntry.ItemNo = Item.ItemNo;
                    dbEntry.ItemName = Item.ItemName;
                    dbEntry.ItemNameLocal = Item.ItemNameLocal;
                    dbEntry.Description = Item.Description;
                    dbEntry.DescriptionLocal = Item.DescriptionLocal;
                    dbEntry.CategorySubID = Item.CategorySubID;
                    dbEntry.BrandID = Item.BrandID;
                    dbEntry.ItemType = Item.ItemType;
                    dbEntry.ItemTypeLocal = Item.ItemTypeLocal;
                    dbEntry.Color = Item.Color;
                    dbEntry.ColorLocal = Item.ColorLocal;
                    dbEntry.Size = Item.Size;
                    dbEntry.SizeLocal = Item.SizeLocal;
                    dbEntry.Thumbnail = Item.Thumbnail;
                    dbEntry.FrontUrl = Item.FrontUrl;
                    dbEntry.BackUrl = Item.BackUrl;
                    dbEntry.SideUrl = Item.SideUrl;
                    dbEntry.OldPrice = Item.OldPrice;
                    dbEntry.NewPrice = Item.NewPrice;
                    dbEntry.UrlLink = Item.UrlLink;
                    dbEntry.Inactive = Item.Inactive;
                    dbEntry.Keywords = Item.Keywords;
                }
            }
            context.SaveChanges();
        }

        public Item DeleteItem(long ItemID)
        {
            Item dbEntry = context.Item.Find(ItemID);
            var count =0;
            if (dbEntry != null && count==0)
            {
                context.Item.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int IsExists(long ItemID)
        {
            return 0;
        }
    }
}
