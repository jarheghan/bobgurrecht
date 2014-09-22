using RepositoryPattern.Controllers;
using RepositoryPattern.Model.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(ICategoryRepository categoryRepository
            ,IProductRepository productRepository)
        {
           this._categoryRepository = categoryRepository;
           this._productRepository = productRepository;
        }
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        //
        // GET: /Admin/Category/

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                return RedirectToAction("List");
            }
            else
                return View(category);
        }

        public ActionResult Edit(int Id)
        {
            Category cat = new Category();
            cat = _categoryRepository.GetCategoryById(Id);
            return View(cat);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                return RedirectToAction("List");
            }
            else { return View(category); }
        }

        public ActionResult List()
        {
            IEnumerable<Category> cat = _categoryRepository.GetAllCategories();
            return View(cat);
        }

        public ActionResult Remove(int? categoryId)
        {
            if (categoryId != null)
            {
                _categoryRepository.DeleteCategory(categoryId ?? 0);
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("List");
            }
        }

       
    }
}
