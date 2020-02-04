using Nyika.Domain.Entities.Setup;
using Nyika.Domain.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Stock
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> Category { get; }
        void SaveCategory(Category Category);
        Category DeleteCategory(long CategoryId);
        Category Single(long CategoryId);
        int IsExists(long CategoryID);
    }
}
