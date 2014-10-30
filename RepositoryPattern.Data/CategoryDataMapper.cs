using RepositoryPattern.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using log4net;

namespace RepositoryPattern.Data
{
    public class CategoryDataMapper : AbstractDataMapper<Category>, ICategoryRepository
    {
        ILog log = LogManager.GetLogger(typeof(CategoryDataMapper));
        public IEnumerable<Category> GetProductByCategory()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var category = cn.Query<Category>("select * from categories");
                return category;
            }
        }

        protected override string TableName
        {
            get { throw new NotImplementedException(); }
        }

        public override Category Map(dynamic result)
        {
            var category = new Category
            {
                ID = result.cat_id,
                Name = result.cat_name,
                Description = result.cat_description,
                MetaKeyword = result.cat_meta_keyword,
                MetaDescription = result.cat_meta_description,
                MetaTitle = result.cat_meta_title,
                PageSize = result.cat_page_size,
                PictureID = result.cat_picture_id,
                ParentCategoryID = result.cat_parent_category_id,
                AllowSelectedPageSize = result.cat_allow_select_page_size,
                Alias = result.cat_alias,
                PriceRanges = result.cat_price_ranges,
                ShowOnHomePage = result.cat_show_home_page,
                DisplayOrder = result.cat_display_order,
                AddUser = result.cat_add_user,
                AddDate = result.cat_add_date,
                ChangeDate = result.cat_change_date,
                ChangeUser = result.cat_change_user,
                DeleteFlag = result.cat_delete_flag
            };
            return category;
        }

        private ProductCategory Map(dynamic result,string name)
        {
            name = "";
            var productCategory = new ProductCategory
            {
                ID = result.pcm_id,
                ProductID = result.pcm_prd_id,
                CategoryID = result.pcm_cat_id,
                IsFeaturedProduct = result.pcm_is_feature_product,
                DisplayOrder = result.pcm_display_order,
                AddDate = result.pcm_add_date,
                AddUser = result.pcm_add_user,
                ChangeDate = result.pcm_change_date,
                ChangeUser = result.pcm_change_user,
                DeleteFlag = result.pcm_delete_flag
            };
            return productCategory;
        }

        public void Add(Category item)
        {
            var param = new
            {
                Name = item.Name,
                Description = item.Description,
                MetaKeyword = item.MetaKeyword,
                MetaDescription = item.MetaDescription,
                MetaTitle = item.MetaTitle,
                PageSize = item.PageSize,
                PictureID = item.PictureID,
                ParentCategoryID = item.ParentCategoryID,
                AllowSelectedPageSize = item.AllowSelectedPageSize,
                Alias = item.Alias,
                PriceRanges = item.PriceRanges,
                ShowOnHomePage = item.ShowOnHomePage,
                DisplayOrder = item.DisplayOrder,
                AddUser = "Jarheghan",
                AddDate = DateTime.Now,
                DeleteFlag = false
            };

            using (IDbConnection cn = Connection) 
            {
                var i = cn.Query<int>(@"INSERT INTO CATEGORIES 
                                    (cat_name, cat_description,cat_alias,cat_meta_keyword,cat_parent_category_id,cat_display_order,
                                     cat_picture_id,
                                     cat_add_date, cat_add_user, cat_delete_flag)
                                    VALUES(@Name,@Description,@Alias,@MetaKeyword,@ParentCategoryID,@DisplayOrder,@PictureID,@AddDate,@AddUser,@DeleteFlag)"
                                    ,param);

            }
        }

        public void Remove(Category item)
        {
            throw new NotImplementedException();
        }

        public void Update(Category item)
        {
            var param = new
            {
                Name = item.Name,
                Description = item.Description,
                PictureID = item.PictureID,
                ParentCategoryID = item.ParentCategoryID,
                Alias = item.Alias,
                ChangeUser = "Jarheghan",
                ChangeDate = DateTime.Now,
                DeleteFlag = false,
                ID = item.ID
            };
            using (IDbConnection cn = Connection)
            {
                var i = cn.Query<int>(@"update CATEGORIES
                                set cat_name = @Name,
	                                cat_description = @Description,
	                                cat_alias = @Alias,
	                                cat_parent_category_id = @ParentCategoryID,
	                                cat_picture_id = @PictureID,
	                                cat_change_date = @ChangeDate,
	                                cat_change_user = @ChangeUser,
	                                cat_delete_flag = @DeleteFlag
                                where cat_id = @ID", param);

            }
          
        }

        public IEnumerable<Category> GetAllCategories()
        {
            using (IDbConnection cn = Connection)
            {
                List<Category> category = new List<Category>();
                cn.Open();
                var categories = cn.Query<dynamic>("select * from categories");
                foreach (var ca in categories)
                {
                    Category cat = Map(ca);
                    category.Add(cat);
                }
                return category.AsEnumerable();
            }
        }

        public IEnumerable<ProductCategory> GetProductCategoriesByCategoryID(int categoryId)
        {
            List<ProductCategory> pcm = new List<ProductCategory>();
            using (IDbConnection cn = Connection)
            {
                try
                {
                    var productcategory = cn.Query<dynamic>("select * from ProductCategoryMapping where pcm_cat_id = @categoryId",
                        new { categoryId = categoryId });
                    foreach (var pc in productcategory)
                    {
                        ProductCategory p = Map(pc, "");
                        pcm.Add(p);
                    }
                    return pcm.AsEnumerable();
                }
                catch(Exception ex)
                {
                    log.Error("Category Error:", ex);
                    return null;
                }
            }
        }

        public IEnumerable<ProductCategory> GetProductCategoriesByProductID(int productId)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int categoryId)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var categories = cn.Query<dynamic>("select * from categories where cat_id = @categoryId",
                                new {categoryId = categoryId }).FirstOrDefault();
                Category cat = Map(categories);
                return cat;
            }
        }

        public IEnumerable<Category> GetAllCategoriesDisplayedOnHomePage()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllCategoriesByParentCategoryId(int parentCategoryID)
        {
            List<Category> categoryList = new List<Category>();
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var categories = cn.Query<dynamic>("select * from categories where cat_id = @parentCategoryID",
                                new { parentCategoryID = parentCategoryID });
                foreach (var mycat in categories)
                {
                    categoryList.Add(Map(mycat));
                }

                return categoryList.AsEnumerable();
            }
        }

        public void InsertProductCategory(ProductCategory productCategory)
        {
            var param = new
            {
                ProductID = productCategory.ProductID,
                CategoryID = productCategory.CategoryID,
                IsFeaturedProduct = productCategory.IsFeaturedProduct,
                DisplayOrder = productCategory.DisplayOrder,
                AddUser = "Jarheghan",
                AddDate = DateTime.Now,
                DeleteFlag = false
            };

            using (IDbConnection cn = Connection)
            {
                var i = cn.Query<int>(@"INSERT INTO ProductCategoryMapping 
                                    (pcm_prd_id, pcm_cat_id,pcm_is_featured_product,pcm_display_order,
                                     pcm_add_date, pcm_add_user, pcm_delete_flag)
                                    VALUES(@ProductID,@CategoryID,@IsFeaturedProduct,@DisplayOrder,@AddDate,@AddUser,@DeleteFlag)"
                                    , param);

            }
        }

        public void UpdateProductCategory(ProductCategory productCategory)
        {
            var param = new
            {
                ProductID = productCategory.ProductID,
                CategoryID = productCategory.CategoryID,
                IsFeaturedProduct = productCategory.IsFeaturedProduct,
                DisplayOrder = productCategory.DisplayOrder,
                ChangeUser = "Jarheghan",
                ChangeDate = DateTime.Now
            };

            using (IDbConnection cn = Connection)
            {
                try
                {
                    var i = cn.Execute(@"Update ProductCategoryMapping 
                                      set   pcm_cat_id = @CategoryID
		                                    ,pcm_is_featured_product = @IsFeaturedProduct
		                                    ,pcm_display_order = @DisplayOrder
                                             ,pcm_change_date = @ChangeDate
                                             , pcm_change_user = @ChangeUser
                                          where pcm_prd_id = @ProductID", param);
                }
                catch { }

            }
        }

        public void DeleteProductCategory(int productcategoryId)
        {
            throw new NotImplementedException();
        }



        public void DeleteCategory(int Id)
        {
            using (IDbConnection cn = Connection)
            {
                try
                {
                    var i = cn.Execute(@"delete Categories
                                        where cat_id = @Id", new { Id = Id });
                }
                catch { }

            }
        }


        //ProductCategory GetProductCategoryByProductID(int productId)
        //{
        //    using (IDbConnection cn = Connection)
        //    {
        //        ProductCategory prdCat = new ProductCategory();
        //        try
        //        {
        //            var i = cn.Query<dynamic>(@"select * FROM ProductCategoryMapping where pcm_prd_id = @productId"
        //                                , new { productId = productId }).FirstOrDefault();

        //            prdCat = Map(i, "ProductCategory");
        //            return prdCat;
        //        }

        //        catch { };
        //        return null;
        //    }
        //}


        ProductCategory ICategoryRepository.GetProductCategoryByProductID(int productId)
        {
            using (IDbConnection cn = Connection)
            {
                ProductCategory prdCat = new ProductCategory();
                try
                {
                    var i = cn.Query<dynamic>(@"select * FROM ProductCategoryMapping where pcm_prd_id = @productId"
                                        , new { productId = productId }).FirstOrDefault();

                    prdCat = Map(i, "ProductCategory");
                    return prdCat;
                }

                catch { };
                return null;
            }
        }
    }
}
