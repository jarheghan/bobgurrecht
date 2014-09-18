using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryPattern.Infrastructure.Domain;

namespace RepositoryPattern.Model.Catalog
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductByCategory(int category);
        Product GetProductByID(int productID);
        IEnumerable<Product> GetProductByPriceValue(int price);
        IEnumerable<Product> GetAllProduct();
        Product GetProductByGuid(Guid prductGuid);
        int InsertProductPicture(ProductPicture item);
        ProductPicture GetProductPictureByID(int productID);
    }
}
