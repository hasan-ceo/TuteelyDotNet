using Nyika.Domain.Entities.Setup;
using Nyika.Domain.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.Stock
{
    public interface IBrandRepo
    {
        IEnumerable<Brand> Brand { get; }
        void SaveBrand(Brand Brand);
        Brand DeleteBrand(long BrandId);
        Brand Single(long BrandId);
        int IsExists(long BrandID);
    }
}
