using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Nyika.Domain.Entities.Stock;

namespace Nyika.WebUI.Models
{
    public class ShopBag
    {
        private List<ShopBagLine> lineCollection = new List<ShopBagLine>();

        public void AddItem(Item item, string itemType, string color, string size, long quantity)
        {
            ShopBagLine line = lineCollection
                .Where(p => p.Item.ItemID == item.ItemID)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new ShopBagLine
                {
                    Item = item,
                    ItemType = itemType,
                    Color = color,
                    Size = size,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void IncreaseItem(Item item)
        {
            ShopBagLine line = lineCollection
                .Where(p => p.Item.ItemID == item.ItemID)
                .FirstOrDefault();

            if (line != null)
            {
                line.Quantity += 1;
            }
        }

        public void DecreaseItem(Item item)
        {
            ShopBagLine line = lineCollection
                .Where(p => p.Item.ItemID == item.ItemID)
                .FirstOrDefault();

            if (line != null)
            {
                if (line.Quantity >= 2)
                    line.Quantity -= 1;
            }
        }

        public void RemoveLine(Item item)
        {
            lineCollection.RemoveAll(l => l.Item.ItemID == item.ItemID);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Item.NewPrice * e.Quantity);

        }

        public decimal ComputeTotalQty()
        {
            return lineCollection.Sum(e => e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<ShopBagLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class ShopBagLine
    {
        public Item Item { get; set; }
        public string ItemType { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public long Quantity { get; set; }
    }
}