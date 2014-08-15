using RepositoryPattern.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace RepositoryPattern.Data
{
    public class CategoryDataMapper : AbstractDataMapper<Category>, ICategoryRepository
    {
        public IEnumerable<Category> GetProductByCategory()
        {
            using (SqlCeConnection cn = Connection2)
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

            using (SqlCeConnection cn = Connection2) 
            {
                var i = cn.Query<int>(@"INSERT INTO CATEGORIES 
                                    (cat_name, cat_description,cat_alias,cat_meta_keyword,cat_parent_category_id,cat_display_order,
                                     cat_add_date, cat_add_user, cat_delete_flag)
                                    VALUES(@Name,@Description,@Alias,@MetaKeyword,@ParentCategoryID,@DisplayOrder,@AddDate,@AddUser,@DeleteFlag)"
                                    ,param);

            }
        }

        public void Remove(Category item)
        {
            throw new NotImplementedException();
        }

        public void Update(Category item)
        {
            throw new NotImplementedException();
        }
    }
}
