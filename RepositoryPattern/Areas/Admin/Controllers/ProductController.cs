using RepositoryPattern.Areas.Admin.Models;
using RepositoryPattern.Controllers;
using RepositoryPattern.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        //
        // GET: /Admin/Product/
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IProductVariationRepository prdVariationRepo)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _prdVariationRepo = prdVariationRepo;
        }
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductVariationRepository _prdVariationRepo;
        public ActionResult List()
        {
            try
            {
                var product = _productRepository.GetAllProduct();
                return View(product);
            }
            catch { return View(); };
          
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, ProductCategory prdCat, IEnumerable<ProductVariation> prdVariation)
        {
            if (ModelState.IsValid)
            {
                Product prd = new Product();
                //ProductCategory prdCat = new ProductCategory();
                ProductPicture picProd = new ProductPicture();
                product.ProductGuid = Guid.NewGuid();
                _productRepository.Add(product);
                prd = _productRepository.GetProductByGuid(product.ProductGuid);

                prdCat.CategoryID = product.CategoryID;
                prdCat.ProductID = prd.ID;
                prdCat.IsFeaturedProduct = prdCat.IsFeaturedProduct;
                if (product.CategoryID != 0)
                {
                    _categoryRepository.InsertProductCategory(prdCat);
                }

                picProd.PictureID = product.PictureID;
                picProd.ProductID = prd.ID;
                picProd.DisplayOrder = 1;

                if (picProd.PictureID != 0)
                {
                    _productRepository.InsertProductPicture(picProd);
                }

                if (prd.ID != 0)
                {
                    
                    foreach( var prdVar in prdVariation)
                    {
                        prdVar.ProductID = prd.ID;
                        prdVar.AddUser = HttpContext.User.Identity.Name;
                        prdVar.AddDate = DateTime.Now;
                        _prdVariationRepo.InsertProductVariation(prdVar);
                    }
                }

                return RedirectToAction("List");
            }
            else { return View(product); }
        }
       
        public ActionResult Edit(int Id)
        {
            Catalog cat = new Catalog();
            var product = _productRepository.GetProductByID(Id);
            var prdpic = _productRepository.GetProductPictureByID(Id);
            var prdVariation = _prdVariationRepo.GetAllProductVariation(Id);
            cat.Product = product;
            cat.ProductVariation = prdVariation;
            if (prdpic != null)
            {
                product.PictureID = prdpic.PictureID;
                return View(cat);
            }
            else
            {
                return View(cat);
            }
           
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                Product prd = new Product();
                ProductCategory prdCat = new ProductCategory();
                ProductPicture picProd = new ProductPicture();
                _productRepository.Update(product);

                prdCat.ProductID = product.ID;
                prdCat.CategoryID = product.CategoryID;

                if (product.CategoryID != 0)
                    _categoryRepository.UpdateProductCategory(prdCat);

                picProd.PictureID = product.PictureID;
                picProd.ProductID = product.ID;
                if (product.PictureID != 0)
                    _productRepository.UpdateProductPicture(picProd);

                return RedirectToAction("List");
            }
            else { return View(product); }
        }

        public ActionResult Delete(int? Id)
        {
            if (Id != null)
            {
                _productRepository.DeleteProduct(Id ?? 0);
                return RedirectToAction("List");
            }
            else
            { 
                var val = "Product-page";
                return RedirectToAction("List", new {val = 2 });
            }
            
        }

    }
}
