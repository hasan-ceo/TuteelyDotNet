using Nyika.Domain.Entities.Setup;
using Nyika.Domain.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Stock
{
    public interface ICategorySubRepo
    {
        IEnumerable<CategorySub> CategorySub { get; }
        void SaveCategorySub(CategorySub CategorySub);
        CategorySub DeleteCategorySub(long CategorySubId);
        CategorySub Single(long CategorySubId);
        int IsExists(long CategorySubID);
    }
}
