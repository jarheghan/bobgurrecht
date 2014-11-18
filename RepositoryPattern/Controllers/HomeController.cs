using RepositoryPattern.Model.Catalog;
using RepositoryPattern.Model.Customers;
using RepositoryPattern.Model.Media;
using RepositoryPattern.Model.Sales;
using RepositoryPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Controllers
{
    
    public partial class HomeController : BaseController
    {
        //
        // GET: /Home/

        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository,
            IProductVariationRepository prdVariationRepo, IPictureRepository pictureRepository,
            IOrderRepository orderRepository, IOrderItemsRepository orderItemRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _prdVariationRepo = prdVariationRepo;
            _pictureRepo = pictureRepository;
            _userRepository = userRepository;
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
        }
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductVariationRepository _prdVariationRepo;
        private readonly IPictureRepository _pictureRepo;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsRepository _orderItemRepository;
        private readonly IUserRepository _userRepository;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product(int prd, int catID)
        {
            ProductGroup prdGroup = new ProductGroup();
            
            var products = _productRepository.GetProductByID(prd);
            var prdpic = _productRepository.GetProductPictureByID(prd);
            var picture = _pictureRepo.GetPictureById(prdpic.PictureID);
            var prdVariation = _prdVariationRepo.GetAllProductVariation(prd);
            prdGroup.Product = products;
            prdGroup.ProductPic = prdpic;
            prdGroup.Picture = picture;
            prdGroup.ProductVariation = prdVariation;
            ViewBag.catID = catID;
            return View(prdGroup);
        }

        public ActionResult CategoryProduct(int? Id)
        {
            ViewBag.CatId = Id ?? default(int);
            return View();
        }

        public ActionResult CategorySubCategory(int? Id)
        {
            ViewBag.CatId = Id ?? default(int);
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();

        }
        public ActionResult Error()
        {
            ViewBag.Error404 = "404";
            return View();
        }
        public ActionResult Error500()
        {
            ViewBag.Error500 = "500";
            return View();
        }

        public ActionResult WishListCart()
        {
            IEnumerable<OrderItems> orderitems;
            if (HttpContext.User.Identity.Name != String.Empty)
            {
                var user = _userRepository.GetSingleUser(HttpContext.User.Identity.Name);
                var order = _orderRepository.GetOrderByUserID(user.ID);

                if (order != null)
                {
                    orderitems = _orderItemRepository.GetOrderItemsByOrderID(order.ID);
                    ProductVariation prdvariation = new ProductVariation();
                    IEnumerable<WishListDisplay> wishlist = orderitems.Select( x => 
                        {
                            var product = _productRepository.GetProductByID(x.ProductID);
                            var  prdpic = _productRepository.GetProductPictureByID(x.ProductID);
                            var pic = _pictureRepo.GetPictureById(prdpic.PictureID);
                            if (x.ProductVariationID > 0)
                            {
                                 prdvariation = _prdVariationRepo.GetSingeProductVariation(x.ProductVariationID);
                            }
                            var orderItem = new OrderItems
                            {
                                ID = x.ID,
                                OrderID = x.OrderID,
                                ProductID = x.ProductID,
                                ProductVariationID = x.ProductVariationID,
                                Quantity = x.Quantity

                            };

                            return new WishListDisplay
                            {
                                Product = product,
                                ProductVariation = prdvariation,
                                Picture = pic,
                                OrderItems = orderItem
                            };
                        });

                    return View(wishlist);
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

    }
}
