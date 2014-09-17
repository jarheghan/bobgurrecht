using RepositoryPattern.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlServerCe;

namespace RepositoryPattern.Data
{
    public class ProductDataMapper : AbstractDataMapper<Product>, IProductRepository
    {

        protected override string TableName
        {
            get { return "Products"; }
        }

       
        public IEnumerable<Product> GetProductByCategory(int prd_category)
        {
            using (SqlCeConnection cn = Connection2)
            {
                var products = GetProductDataNoJoin(
                            @"SELECT * from Products WHERE prd_category = @prd_category", new { prd_category });
                return products;
            }
            //throw new NotImplementedException();
        }

        public void Add(Product item)
        {
            //using (IDbConnection cn = Connection)
            using (SqlCeConnection cn = Connection2)
            {
                var parameter = new
                {
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    ShortDescription = item.ShortDescription,
                    SKU = item.SKU,
                    ManufacturePartNo = item.ManufacturePartNo,
                    StockQuantity = item.StockQuantity,
                    AddUser = "jarheghan",
                    AddDate = DateTime.Now,
                    DeleteFlag = false
                };
                cn.Open();
                var i = cn.Query<int>(@"INSERT INTO Products (prd_name,prd_description,prd_price,prd_short_description,
                                            prd_sku,prd_manufacturepart_no,prd_stock_quantity
                                           ,prd_add_user,prd_add_date,prd_delete_flag)  
                                        VALUES(@Name,@Description,@Price,@ShortDescription
                                                ,@SKU,@ManufacturePartNo,@StockQuantity,@AddUser,@AddDate,@DeleteFlag)", parameter);
            }
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
                ShortDescription = result.prd_short_description,
                ShowOnHomePage = result.prd_showonhomepage,
                MetaKeyword = result.prd_meta_keyword,
                MetaDescrption = result.prd_meta_description,
                MetaTitle = result.prd_meta_title,
                SKU = result.prd_sku,
                ManufacturePartNo = result.prd_manufacturepart_no,
                StockQuantity = result.prd_stock_quantity,
                DisplayStockAvaliable = result.prd_display_stock_avaliable,
                DisplayStockQuantity = result.prd_display_stock_quatity,
                CallForPrice = result.prd_call_for_price,
                OldPrice = result.prd_old_price,
                Weight = result.prd_weight,
                Width = result.prd_width,
                Length = result.prd_length,
                Height = result.prd_height,
                ProductGuid = result.prd_guid,
                DeleteFlag = true,
                AddDate = DateTime.Now,
                AddUser = "Jarheghan"
            };
            return product;
        }
        


        private IEnumerable<Product> GetProductData(string sql, dynamic param = null)
        {
            List<Product> product = null;
            //using (IDbConnection cn = Connection)
            using (SqlCeConnection cn = Connection2)
            {
                cn.Open();
                using (var multi = cn.QueryMultiple(sql, (object)param))
                {
                    product = multi.Read<dynamic, dynamic, Product>((prd,user) =>
                        {
                            Product prod = Map(prd);
                            //prod.CreatedBy = new UserDataMapper().Map(user);
                            return prod;
                        },splitOn: "UserID").ToList();
                }
            }
            return product;
        }

        private IEnumerable<Product> GetProductDataNoJoin(string sql, dynamic param = null)
        {
            List<Product> product = new List<Product>();
            //using (IDbConnection cn = Connection)
            using (SqlCeConnection cn = Connection2)
            {
                cn.Open();
                var prd  = cn.Query<dynamic>(sql, (object)param);
                foreach (var p in prd)
                {
                    Product prds = Map(p);
                    product.Add(prds);
                }
                return product.AsEnumerable();
            }
           
        }
         


        public Product GetProductByID(int productID)
        {
           
            using (SqlCeConnection cn = Connection2)
            {
                cn.Open();
                var product = cn.Query<dynamic>("select * from products where prd_id = @ProductID", new { ProductID = productID }).FirstOrDefault();
                Product prd = Map(product);
                return prd;
            }
        }

        public IEnumerable<Product> GetProductByPriceValue(int price)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Product> GetAllProduct()
        {
            var sql = @"select * from products";
            return GetProductDataNoJoin(sql);
            
        }


        public Product GetProductByGuid(Guid prductGuid)
        {
            using (SqlCeConnection cn = Connection2)
            {
                cn.Open();
                try
                {
                    var product = cn.Query<Product>("select * from products where prd_guid = @prductGuid", new { prductGuid = prductGuid }).FirstOrDefault();
                    return product;
                }
                catch { return null; }


            }

        }
    }
}
