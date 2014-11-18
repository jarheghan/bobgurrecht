using RepositoryPattern.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using log4net;
//[assembly:log4net.Config.XmlConfigurator(Watch=true)]

namespace RepositoryPattern.Data
{
    public class ProductDataMapper : AbstractDataMapper<Product>, IProductRepository
    {

        protected override string TableName
        {
            get { return "Products"; }
        }

        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ILog log = LogManager.GetLogger(typeof(ProductDataMapper));
       
        public IEnumerable<Product> GetProductByCategory(int prd_category)
        {
            using (IDbConnection cn = Connection)
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
            using (IDbConnection cn = Connection)
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
                    ProductGuid = item.ProductGuid,
                    ShowOnHomePage = item.ShowOnHomePage,
                    AddUser = Environment.UserName,
                    AddDate = DateTime.Now,
                    DeleteFlag = false
                };
                cn.Open();
                try
                {
                    var i = cn.Query<int>(@"INSERT INTO Products (prd_name,prd_description,prd_price,prd_short_description,
                                            prd_sku,prd_manufacturepart_no,prd_stock_quatity, prd_guid,prd_showonhomepage
                                           ,prd_add_user,prd_add_date,prd_delete_flag)  
                                        VALUES(@Name,@Description,@Price,@ShortDescription
                                                ,@SKU,@ManufacturePartNo,@StockQuantity,@ProductGuid,@ShowOnHomePage,@AddUser,@AddDate,@DeleteFlag)", parameter);
                }
                catch { }
            }
        }

        public void Remove(Product item)
        {
            throw new NotImplementedException();
        }

        public void Update(Product item)
        {
            var param = new
            {
                ID = item.ID,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                ShortDescription = item.ShortDescription,
                SKU = item.SKU,
                ManufacturePartNo = item.ManufacturePartNo,
                StockQuantity = item.StockQuantity,
                ShowOnHomePage = item.ShowOnHomePage,
                ChangeUser = Environment.UserName,
                ChangeDate = DateTime.Now
            };

            using (IDbConnection cn = Connection)
            {
                try
                {
                    var i = cn.Execute(@"update products
                               set prd_name = @Name,
                                   prd_description = @Description,
                                   prd_price = @Price,
                                   prd_short_description = @ShortDescription,
                                   prd_sku = @SKU,
                                   prd_manufacturepart_no = @ManufacturePartNo,
                                   prd_stock_quatity = @StockQuantity,
                                   prd_showonhomepage = @ShowOnHomePage,
                                   prd_change_user = @ChangeUser,
                                   prd_change_date = @Changedate
                                where prd_id = @ID", param);
                }
                catch { }
            }
           
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
                StockQuantity = result.prd_stock_quatity,
                DisplayStockAvaliable = result.prd_display_stock_avaliable,
                DisplayStockQuantity = result.prd_display_stock_quatity,
                CallForPrice = result.prd_call_for_price,
                OldPrice = result.prd_old_price,
                Weight = result.prd_weight,
                Width = result.prd_width,
                Length = result.prd_length,
                Height = result.prd_height,
                ProductGuid = result.prd_guid == null ? Guid.Empty : result.prd_guid,
                DeleteFlag = true,
                AddDate = DateTime.Now,
                AddUser = Environment.UserName
            };
            return product;
        }

        private ProductPicture MapProductPicture(dynamic result)
        {
            var productpicture = new ProductPicture
            {
                ID = result.ppm_id,
                PictureID = result.ppm_pic_id,
                ProductID = result.ppm_prd_id,
                DisplayOrder = result.ppm_display_order
            };
            return productpicture;
        }
        


        private IEnumerable<Product> GetProductData(string sql, dynamic param = null)
        {
            List<Product> product = null;
            //using (IDbConnection cn = Connection)
            using (IDbConnection cn = Connection)
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
            using (IDbConnection cn = Connection)
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

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var product = cn.Query<dynamic>("select * from products where prd_id = @ProductID", new { ProductID = productID }).FirstOrDefault();
                Product prd = Map(product);
                return prd;
            }
        }

        public ProductPicture GetProductPictureByID(int productID)
        {

            using (IDbConnection cn = Connection)
            {
                ProductPicture prd = new ProductPicture();
                cn.Open();
                var product = cn.Query<dynamic>("select * from ProductPictureMapping where ppm_prd_id = @ProductID", new { ProductID = productID }).FirstOrDefault();
                if (product != null)
                {
                     prd = MapProductPicture(product);
                    return prd;
                }
                else { return prd; }
               
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
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                try
                {
                    var product = cn.Query<dynamic>("select * from products where prd_guid = @prductGuid", new { prductGuid = prductGuid.ToString() }).FirstOrDefault();
                    Product prd = Map(product);
                    return prd;
                }
                catch { return null; }


            }

        }


        public int InsertProductPicture(ProductPicture item)
        {
            var param = new
            {
                ProductID = item.ProductID,
                PictureID = item.PictureID,
                DisplayOrder = item.DisplayOrder
            };
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                try
                {
                    log.Info("Insert Product Picture");
                    int i = cn.Execute(@"Insert Into ProductPictureMapping(ppm_prd_id,ppm_pic_id,ppm_display_order)
                                      Values(@ProductID,@PictureID,@DisplayOrder)", param);
                    return i;
                }
                catch(Exception ex) 
                {
                    log.Error("Error Insert ProductPicture:", ex);
                    return 0; 
                }
            }
        }


        public void UpdateProductPicture(ProductPicture item)
        {
            var param = new
            {
                ProductID = item.ProductID,
                PictureID = item.PictureID,
                DisplayOrder = item.DisplayOrder
            };

            using (IDbConnection cn = Connection)
            {
           
                try
                {
                    log.Info("Update Product Picture");
                    var i = cn.Execute(@"update ProductPictureMapping
                                        set	ppm_pic_id = @PictureID,
	                                        ppm_display_order = @DisplayOrder
                                        where ppm_prd_id = @ProductID", param);
                }
                catch(Exception ex) 
                {
                    log.Error("Error Update ProductPicture", ex);
                }
            }
        }


        public void DeleteProduct(int Id)
        {
            using (IDbConnection cn = Connection)
            {

                try
                {
                    log.Info("Log this Delete error");
                    var i = cn.Query<int>(@"
                                    delete ProductCategoryMapping
                                    where pcm_prd_id = @Id

                                    select @picid = ppm_pic_id from ProductPictureMapping
                                    where ppm_prd_id = @Id

                                    delete ProductPictureMapping
                                    where ppm_prd_id = @Id
                                    
                                    delete ProductVariation
                                    where prv_prd_id = @Id
                                    
                                    delete Products
                                    where prd_id = @Id

                                    delete Picture
                                    where pic_id = @picid", new { Id = Id, picid = 0 }).FirstOrDefault();
                }
                catch (Exception ex) 
                {
                    log.Error(ex.Message,ex);
                }
            }
        }


        public IEnumerable<Product> GetAllFeatureProduct()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                List<Product> products = new List<Product>();
                try
                {
                    var product = cn.Query<dynamic>("select * from products where prd_showonhomepage = 1");
                    foreach (var p in product)
                    {
                        Product prd = Map(p);
                        products.Add(prd);
                    }
                    return products.AsEnumerable();
                }
                catch { return null; }


            }
        }


        public IEnumerable<Product> SearchProductByCriteria(string prdName)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                List<Product> products = new List<Product>();
                try
                {
                    var product = cn.Query<dynamic>(@" select * FROM Products
                                                        where prd_name Like '%' + @prdName +  '%'", new { prdName = prdName });
                    foreach (var p in product)
                    {
                        Product prd = Map(p);
                        products.Add(prd);
                    }
                    return products.AsEnumerable();
                }
                catch { return null; }


            }
        }
    }
}
