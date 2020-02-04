using Nyika.Domain.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Setup
{
    public interface IShopPageRepo
    {
        IEnumerable<ShopPage> ShopPage { get; }
        void SaveShopPage(ShopPage ShopPage);
        ShopPage DeleteShopPage(long ShopPageID);

    }
}
