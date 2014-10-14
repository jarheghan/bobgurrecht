using RepositoryPattern.Model.Catalog;
using RepositoryPattern.Model.Media;
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
            ,IProductRepository productRepository, IPictureRepository pictureRepository)
        {
           this._categoryRepository = categoryRepository;
           this._productRepository = productRepository;
           this._pictureRepository = pictureRepository;
        }
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPictureRepository _pictureRepository;
        
        public ActionResult Menu()
        {
            IEnumerable<Category> cat = _categoryRepository.GetAllCategories();
            return PartialView(cat);
        }

        public ActionResult Slider()
        {
            return PartialView();
        }

        public ActionResult DisplayCategoryProduct(int? Id)
        {
            var productCategory = _categoryRepository.GetProductCategoriesByCategoryID(9);

            IEnumerable<ProductCategory> prdcat = productCategory
                        .Select(x =>
                        {
                            var product = _productRepository.GetProductByID(x.ProductID ?? default(int));
                            var category = _categoryRepository.GetCategoryById(x.CategoryID ?? default(int));
                            var prdPic = _productRepository.GetProductPictureByID(x.ProductID??default(int));
                            var pic = _pictureRepository.GetPictureById(prdPic.PictureID);
                            return new ProductCategory()
                            {
                                ID = x.ID,
                                CategoryID = x.CategoryID,
                                ProductID = x.ProductID,
                                Product = product,
                                Picture = pic,
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

        public ActionResult SideMenu()
        {
            return PartialView();
        }

        public ActionResult DisplayFeatureProduct()
        {
            var feautureProducts = _productRepository.GetAllFeatureProduct();
            IEnumerable<ProductCategory> prdcat = feautureProducts
                                        .Select(x => 
                                            {
                                                var prdPic = _productRepository.GetProductPictureByID(x.ID);
                                                var pic = _pictureRepository.GetPictureById(prdPic.PictureID);
                                                var product = _productRepository.GetProductByID(x.ID);
                                                return new ProductCategory()
                                                {
                                                    Picture = pic,
                                                    Product = product
                                                };
                                            });
            return View(prdcat);
        }

       

    }
}
