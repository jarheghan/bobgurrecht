using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryPattern.Infrastructure.Domain;

namespace RepositoryPattern.Model.Product
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductByCategory(int category);
    }
}
