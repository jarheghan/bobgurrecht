using RepositoryPattern.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Data
{
    public class ProductDataMapper : AbstractDataMapper<Product>, IProductRepository
    {

        protected override string TableName
        {
            get { return "Users"; }
        }

        public override Product Map(dynamic result)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public void Add(Product item)
        {
            throw new NotImplementedException();
        }

        public void Remove(Product item)
        {
            throw new NotImplementedException();
        }

        public void Update(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
