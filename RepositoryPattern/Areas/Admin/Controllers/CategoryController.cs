using RepositoryPattern.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Areas.Admin.Controllers
{
    public class CategoryController : Controller
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
        public ActionResult Create(int ss)
        {
            return View();
        }

        public ActionResult Edit(int categoryId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int categoryId)
        {
            return View();
        }

        public ActionResult List()
        {
            IEnumerable<Category> cat = _categoryRepository.GetAllCategories();
            return View(cat);
        }
    }
}
