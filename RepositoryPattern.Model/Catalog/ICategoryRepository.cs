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
        IEnumerable<Category> GetAllCategories();
        IEnumerable<ProductCategory> GetProductCategoriesByCategoryID(int categoryId);
        IEnumerable<ProductCategory> GetProductCategoriesByProductID(int productId);
        Category GetCategoryById(int categoryId);
        IEnumerable<Category> GetAllCategoriesDisplayedOnHomePage();
        IEnumerable<Category> GetAllCategoriesByParentCategoryId(int parentCategoryID);
        void InsertProductCategory(ProductCategory productCategory);
        void UpdateProductCategory(ProductCategory productCategory);
        void DeleteProductCategory(int productcategoryId);
    }
}
