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

        public ActionResult Product(int prd, int catID)
        {
            ProductGroup prdGroup = new ProductGroup();
            
            var products = _productRepository.GetProductByID(prd);
            var prdpic = _productRepository.GetProductPictureByID(prd);
            var picture = _pictureRepo.GetPictureById(prdpic.PictureID);
            var prdVariation = _prdVariationRepo.GetAllProductVariation(prd);
            prdGroup.Product = products;
            prdGroup.ProductPic = prdpic;
            prdGroup.Picture = picture;
            prdGroup.ProductVariation = prdVariation;
            ViewBag.catID = catID;
            return View(prdGroup);
        }

        public ActionResult CategoryProduct(int? Id)
        {
            ViewBag.CatId = Id ?? default(int);
            return View();
        }

        public ActionResult CategorySubCategory(int? Id)
        {
            ViewBag.CatId = Id ?? default(int);
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();

        }

    }
}
