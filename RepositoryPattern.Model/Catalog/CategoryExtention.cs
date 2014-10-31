using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Catalog
{
    public static class CategoryExtention
    {
        public static string GetCategoryBreadCrumb(this Category category, ICategoryRepository categoryService, IDictionary<int, Category> mappedCategories = null)
        {
            string result = string.Empty;
            bool ss = category.DeleteFlag??false;
            while (category != null && !ss)
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = category.Name;
                }
                else
                {
                    result = category.Name + ">>" + result;
                }
                int parentId = category.ParentCategoryID??default(int);
                if (mappedCategories == null)
                {
                    category = categoryService.GetCategoryById(parentId);
                }
                else
                {
                    category = mappedCategories.ContainsKey(parentId) ? mappedCategories[parentId] : categoryService.GetCategoryById(parentId);
                }
            }
            return result;
        }
    }
}
