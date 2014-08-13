using RepositoryPattern.Model.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace RepositoryPattern.Data
{
    public class ProductDataMapper : AbstractDataMapper<Product>, IProductRepository
    {

        protected override string TableName
        {
            get { return "Products"; }
        }

        public override Product Map(dynamic result)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByCategory(string category)
        {
            Product prd = new Product();
            using (IDbConnection cn = Connection)
            {

            }
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

        public override Product Map(dynamic result)
        {
            var product = new Product()
            {
                ID = result.prd_id,
                Name = result.prd_name,
                Description = result.prd_description,
                Price = result.prd_price,
                Category = result.prd_category,
                DeleteFlag = true,
                AddDate = DateTime.Now,
                AddUser = "Jarheghan"
            };
            return product;
        }

        private IEnumerable<Product> GetProductData(string sql, dynamic param = null)
        {
            List<Product> product = null;
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                using (var multi = cn.QueryMultiple(sql, (object)param))
                {
                    product = multi.Read<dynamic, dynamic, Product>((prd, user) =>
                        {
                            Product prod = Map(prd);
                            return prod;
                        }).ToList();
                }
            }
            return product;
        }
    }
}
