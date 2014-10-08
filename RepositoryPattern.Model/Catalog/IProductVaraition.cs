using RepositoryPattern.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Catalog
{
    public interface IProductVaraition : IRepository<ProductVariation>
    {
        void InsertProductVariation(ProductVariation prdVariation);
        void DeleteProductVariation(int Id);
        void UpdatePrductVariation(ProductVariation prdVariation);
        IEnumerable<ProductVariation> GetAllProductVariation(int productId);
    }
}
