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
            Product prd = new Product();
            ProductCategory prdCat = new ProductCategory();
            product.ProductGuid = Guid.NewGuid();
            _productRepository.Add(product);
            prd = _productRepository.GetProductByGuid(product.ProductGuid);

            prdCat.CategoryID = product.CategoryID;
            prdCat.ProductID = prd.ID;
            _categoryRepository.InsertProductCategory(prdCat);

            return View(product);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            return View();
        }

        public ActionResult Delete(int Id)
        {
            return View();
        }

    }
}
