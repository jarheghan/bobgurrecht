using log4net;
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

        ILog log = LogManager.GetLogger(typeof(CommonController));
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
            try
            {
                var productCategory = _categoryRepository.GetProductCategoriesByCategoryID(Id ?? default(int));
                var catg = _categoryRepository.GetCategoryById(Id ?? default(int));

                IEnumerable<ProductCategory> prdcat = productCategory
                            .Select(x =>
                            {
                                var product = _productRepository.GetProductByID(x.ProductID ?? default(int));
                                var category = _categoryRepository.GetCategoryById(x.CategoryID ?? default(int));
                                var prdPic = _productRepository.GetProductPictureByID(x.ProductID ?? default(int));
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
                ViewBag.Category = catg;

                return PartialView(prdcat);
            }
            catch (Exception ex) { log.Error("Common Error:", ex); return PartialView(); }
        }

        public ActionResult SideMenu()
        {
            IEnumerable<Category> cat = _categoryRepository.GetAllCategories();
            return PartialView(cat);
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
