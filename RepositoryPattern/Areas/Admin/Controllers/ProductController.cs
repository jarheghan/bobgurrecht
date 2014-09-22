using RepositoryPattern.Controllers;
using RepositoryPattern.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Admin/Product/
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
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
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                Product prd = new Product();
                ProductCategory prdCat = new ProductCategory();
                ProductPicture picProd = new ProductPicture();
                product.ProductGuid = Guid.NewGuid();
                _productRepository.Add(product);
                prd = _productRepository.GetProductByGuid(product.ProductGuid);

                prdCat.CategoryID = product.CategoryID;
                prdCat.ProductID = prd.ID;
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

                return RedirectToAction("List");
            }
            else { return View(product); }
        }
       
        public ActionResult Edit(int Id)
        {
            var product = _productRepository.GetProductByID(Id);
            var prdpic = _productRepository.GetProductPictureByID(Id);
            if (prdpic != null)
            {
                product.PictureID = prdpic.PictureID;
                return View(product);
            }
            else
            {
                return View(product);
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
