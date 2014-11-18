using RepositoryPattern.Areas.Admin.Models;
using RepositoryPattern.Controllers;
using RepositoryPattern.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace RepositoryPattern.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : BaseController
    {
        ILog logger = LogManager.GetLogger(typeof(ProductController));
       
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

                if (prd.ID != 0 && prdVariation != null)
                {
                    
                    foreach( var prdVar in prdVariation)
                    {
                        prdVar.ProductID = prd.ID;
                        prdVar.AddUser = HttpContext.User.Identity.Name;
                        prdVar.AddDate = DateTime.Now;
                        prdVar.DeleteFlag = false;
                        _prdVariationRepo.InsertProductVariation(prdVar);
                    }
                }

                return RedirectToAction("List");
            }
            else { return View(product); }
        }
       
        public ActionResult Edit(int? Id)
        {
            Catalog cat = new Catalog();
            ProductCategory prdCat = new ProductCategory();
            var product = _productRepository.GetProductByID(Id??1);
            var prdpic = _productRepository.GetProductPictureByID(Id??1);
            var prdVariation = _prdVariationRepo.GetAllProductVariation(Id??1);
            cat.Product = product;
            cat.ProductVariation = prdVariation.ToList();

            prdCat = _categoryRepository.GetProductCategoryByProductID(product.ID);
            if(prdCat != null)
                cat.Product.CategoryID = prdCat.CategoryID??1;
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
        public ActionResult Edit(Product product, IEnumerable<ProductVariation> prdVariation, Catalog cat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    logger.Info("Log Edit for Product Update");
                    Product prd = new Product();
                    ProductCategory prdCat = new ProductCategory();
                    ProductPicture picProd = new ProductPicture();
                    ProductPicture picProd1 = new ProductPicture();
                    _productRepository.Update(cat.Product);

                    //prdCat = _categoryRepository.GetProductCategoryByProductID(product.ID);
                    //picProd = _productRepository.GetProductPictureByID(product.ID);

                    prdCat.CategoryID = cat.Product.CategoryID;
                    prdCat.ProductID = cat.Product.ID;

                    if (prdCat.CategoryID != 0)
                    {
                        bool result = _categoryRepository.ProdductCateogryExitByProductID(prdCat.ProductID??0);
                        if(result == true)
                        _categoryRepository.UpdateProductCategory(prdCat);
                        if (result == false)
                            _categoryRepository.InsertProductCategory(prdCat);
                    }

                    picProd.PictureID = cat.Product.PictureID;
                    picProd.ProductID = cat.Product.ID;
                    picProd1 = _productRepository.GetProductPictureByID(cat.Product.ID);
                    if (picProd1.ProductID != 0)
                        _productRepository.UpdateProductPicture(picProd);
                    else
                        _productRepository.InsertProductPicture(picProd);

                    if (product.ID != 0 && cat.ProductVariation != null)
                    {
                        foreach (var prdVar in cat.ProductVariation)
                        {
                            prdVar.ChangeUser = HttpContext.User.Identity.Name;
                            prdVar.ChangeDate = DateTime.Now;
                            prdVar.DeleteFlag = false;
                            prdVar.ProductID = product.ID;
                            if (prdVar.ID != 0)
                                _prdVariationRepo.UpdatePrductVariation(prdVar);
                            else
                                _prdVariationRepo.InsertProductVariation(prdVar);
                        }
                    }

                    return RedirectToAction("List");
                }

                catch (Exception ex)
                {
                    logger.Error("Update Error for Product", ex);
                    return View(cat);
                }
            }
            else { return View(cat); }
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

        public ActionResult DeleteVariation(int Id, int prdId)
        {
            if (Id != 0)
            {
                _prdVariationRepo.DeleteProductVariation(Id);
                return RedirectToAction("Edit", "Product", new { Id = prdId });
            }
            return Content(@"<script language='javascript' type='text/javascript'>catalog.messagealert('The record was not deleted')</script>");
        }

    }
}
