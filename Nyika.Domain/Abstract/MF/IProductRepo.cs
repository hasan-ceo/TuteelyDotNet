using System.Collections.Generic;
using Nyika.Domain.Entities.MF;

namespace Nyika.Domain.Abstract.MF
{
    public interface IProductRepo
    {
        IEnumerable<Product> Product(string InstanceID);
        IEnumerable<Product> ProductProjectWise(string InstanceID,long ProjectID);
        Product Single(string InstanceID, long ID);
        void SaveProduct(Product Product);
        Product DeleteProduct(long ProductID);
        int IsExists(long ProductID);
        void ProductChange(long SelectedLoan, long SelectedProduct);
    }
}
