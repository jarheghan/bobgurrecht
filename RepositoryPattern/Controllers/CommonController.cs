using RepositoryPattern.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Controllers
{
    public partial class CommonController : BaseController
    {
        //
        // GET: /Common/
        //public CommonController() { }
       
      
        public CommonController(ICategoryRepository categoryRepository
            ,IProductRepository productRepository)
        {
           this._categoryRepository = categoryRepository;
           this._productRepository = productRepository;
        }
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        
        public ActionResult Menu()
        {
            IEnumerable<Category> cat = _categoryRepository.GetAllCategories();
            return PartialView(cat);
        }

        public ActionResult Slider()
        {
            return PartialView();
        }

        public ActionResult DisplayFeatureProduct()
        {
            var productCategory = _categoryRepository.GetProductCategoriesByCategoryID(1);

            IEnumerable<ProductCategory> prdcat = productCategory
                        .Select(x =>
                        {
                            var product = _productRepository.GetProductByID(x.ProductID ?? default(int));
                            var category = _categoryRepository.GetCategoryById(x.CategoryID ?? default(int));
                            return new ProductCategory()
                            {
                                ID = x.ID,
                                CategoryID = x.CategoryID,
                                ProductID = x.ProductID,
                                Product = product,
                                Category = category,
                                IsFeaturedProduct = x.IsFeaturedProduct,
                                AddDate = x.AddDate,
                                AddUser = x.AddUser,
                                ChangeDate = x.ChangeDate,
                                ChangeUser = x.ChangeUser,
                                DeleteFlag = x.DeleteFlag
                            };
                        });
            return PartialView(prdcat);
        }

    }
}
