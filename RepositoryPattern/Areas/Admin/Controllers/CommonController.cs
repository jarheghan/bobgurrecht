using RepositoryPattern.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Areas.Admin.Controllers
{
    [Authorize]
    public class CommonController : Controller
    {
        //
        // GET: /Admin/Common/
        public CommonController(IProductRepository productRepository, ICategoryRepository categoryRepository, IProductVariationRepository prdVariationRepo)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _prdVariationRepo = prdVariationRepo;
        }
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductVariationRepository _prdVariationRepo;
        public ActionResult Menu()
        {
            return PartialView();
        }

        public ActionResult BodySide()
        {
            return PartialView();
        }

        public ActionResult Header()
        {
            return PartialView();
        }

        public ActionResult ProductVariation()
        {
            return PartialView();
        }
        public ActionResult ProductVariationEdit(int Id)
        {
            var prdVar = _prdVariationRepo.GetSingeProductVariation(Id);
            return PartialView(prdVar);
        }

    }
}
