using RepositoryPattern.Areas.Admin.Models;
using RepositoryPattern.Controllers;
using RepositoryPattern.Model.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using RepositoryPattern.Models;
using RepositoryPattern.Model.Media;

namespace RepositoryPattern.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : BaseController
    {
        public CategoryController(ICategoryRepository categoryRepository
            ,IProductRepository productRepository, IPictureRepository pictureRepository)
        {
           this._categoryRepository = categoryRepository;
           this._productRepository = productRepository;
           this._pictureRepository = pictureRepository;
        }
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPictureRepository _pictureRepository;
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
            //CategoryModel cat = new CategoryModel();
            cat = _categoryRepository.GetCategoryById(Id);
            //if(cat.Category.PictureID !=null)
            // cat.Picture = _pictureRepository.GetPictureById(cat.Category.PictureID??default(int));
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

        public ActionResult List(CategorySearchModel searchModel)
        {
            var pageIndex = searchModel.Page ?? 1;
            const int RecordPerPage = 10;
            searchModel.SearchResult = _categoryRepository.GetAllCategories().ToPagedList(pageIndex,RecordPerPage);
            return View(searchModel);
        }

        [HttpPost]
        public ActionResult List(CategorySearchModel searchModel, string ss = null)
        {
            const int RecordsPerPage = 10;
            if (!string.IsNullOrEmpty(searchModel.SearchButton) || searchModel.Page.HasValue)
            {
                var category = _categoryRepository.GetAllCategories();
                var filtercategory = category.Where(x => (x.Name.Contains(searchModel.CategoryName)
                                       || searchModel.CategoryName == null)
                    ).OrderBy(p => p.Name);

                var pageIndex = 1;
                if (searchModel.CategoryName != null)
                    searchModel.SearchResult = filtercategory.ToPagedList(pageIndex, RecordsPerPage);
                if (searchModel.CategoryName == null)
                    searchModel.SearchResult = category.ToPagedList(pageIndex, RecordsPerPage);
            }
            return View(searchModel);
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
