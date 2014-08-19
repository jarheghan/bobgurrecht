using RepositoryPattern.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Controllers
{
    public partial class CommonController : BaseController
    {
        //
        // GET: /Common/
        public CommonController() { }
       
      
        public CommonController(ICategoryRepository categoryRepository)
        {
           this._categoryRepository = categoryRepository;
        }
        private readonly ICategoryRepository _categoryRepository;
        public ActionResult Menu()
        {
            IEnumerable<Category> cat = _categoryRepository.GetAllCategories();
            return PartialView(cat);
        }

        public ActionResult Slider()
        {
            return PartialView();
        }

    }
}
