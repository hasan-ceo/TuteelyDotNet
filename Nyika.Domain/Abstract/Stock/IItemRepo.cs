using Nyika.Domain.Entities.Setup;
using Nyika.Domain.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Stock
{
    public interface IItemRepo
    {
        IEnumerable<Item> Item { get; }
        void SaveItem(Item Item);
        Item DeleteItem(long ItemId);
        Item Single(long ItemId);
        int IsExists(long ItemID);
        IEnumerable<Item> Search(string txtSearch);
    }
}
