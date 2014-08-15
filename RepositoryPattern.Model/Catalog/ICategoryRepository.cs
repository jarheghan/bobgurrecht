using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryPattern.Infrastructure.Domain;

namespace RepositoryPattern.Model.Catalog
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetProductByCategory();
    }
}
