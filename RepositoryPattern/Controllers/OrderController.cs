using RepositoryPattern.Model.Catalog;
using RepositoryPattern.Model.Customers;
using RepositoryPattern.Model.Media;
using RepositoryPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Controllers
{
    public class OrderController : BaseController
    {
        //
        // GET: /Order/

        public OrderController(IProductRepository productRepository, ICategoryRepository categoryRepository,
            IProductVariationRepository prdVariationRepo, IPictureRepository pictureRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _prdVariationRepo = prdVariationRepo;
            _pictureRepo = pictureRepository;
            _userRepo = userRepository;
        }
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductVariationRepository _prdVariationRepo;
        private readonly IPictureRepository _pictureRepo;
        private readonly IUserRepository _userRepo;

        public ActionResult AddToWishList(WishListItems items)
        {
            var user = HttpContext.User.Identity;
            if (user.IsAuthenticated == true)
            {
                //First let get the User ID
                var usermain = _userRepo.GetSingleUser(user.Name);

                Errors err = new Errors();
                err.Message = "success";
                return Json(err, JsonRequestBehavior.AllowGet);
            }
            if (user.IsAuthenticated == false)
            {
                Errors err = new Errors();
                err.Message = "authenticate";
                return Json(err, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Errors err = new Errors();
                err.Message = "error";
                return Json(err, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
