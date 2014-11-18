using log4net;
using RepositoryPattern.Model.Catalog;
using RepositoryPattern.Model.Customers;
using RepositoryPattern.Model.Media;
using RepositoryPattern.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Controllers
{
    public class SearchController : BaseController
    {
        ILog log = LogManager.GetLogger(typeof(CommonController));
        public SearchController(ICategoryRepository categoryRepository
            ,IProductRepository productRepository, IPictureRepository pictureRepository,
            IOrderRepository orderRepository, IOrderItemsRepository orderItemRepository,
            IUserRepository userRepository,IProductVariationRepository prdVariationRepo)
        {
           this._categoryRepository = categoryRepository;
           this._productRepository = productRepository;
           this._pictureRepository = pictureRepository;
           this._orderRepository = orderRepository;
           this._orderItemRepository = orderItemRepository;
           this._userRepository = userRepository;
            this._prdVariationRepo = prdVariationRepo;
        }
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPictureRepository _pictureRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsRepository _orderItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductVariationRepository _prdVariationRepo;

        public ActionResult SearchProduct(string prdname)
        {
            var searchedPrd = _productRepository.SearchProductByCriteria(prdname);
            IEnumerable<ProductCategory> prdcat = searchedPrd
                                        .Select(x =>
                                        {
                                            var prdPic = _productRepository.GetProductPictureByID(x.ID);
                                            var pic = _pictureRepository.GetPictureById(prdPic.PictureID);
                                            var product = _productRepository.GetProductByID(x.ID);
                                            var prdCat1 = _categoryRepository.GetProductCategoriesByProductID(x.ID);
                                            return new ProductCategory()
                                            {
                                                Picture = pic,
                                                Product = product,
                                                CategoryID = prdCat1.CategoryID
                                            };
                                        });
            return View(prdcat);
        }

    }
}
