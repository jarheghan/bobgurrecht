using RepositoryPattern.Model.Catalog;
using RepositoryPattern.Model.Media;
using RepositoryPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Controllers
{
    [Authorize]
    public partial class HomeController : BaseController
    {
        //
        // GET: /Home/

        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository,
            IProductVariationRepository prdVariationRepo, IPictureRepository pictureRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _prdVariationRepo = prdVariationRepo;
            _pictureRepo = pictureRepository;
        }
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductVariationRepository _prdVariationRepo;
        private readonly IPictureRepository _pictureRepo;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product(int prd)
        {
            ProductGroup prdGroup = new ProductGroup();
            
            var products = _productRepository.GetProductByID(prd);
            var prdpic = _productRepository.GetProductPictureByID(prd);
            var picture = _pictureRepo.GetPictureById(prdpic.PictureID);

            prdGroup.Product = products;
            prdGroup.ProductPic = prdpic;
            prdGroup.Picture = picture;
            return View(prdGroup);
        }

    }
}
