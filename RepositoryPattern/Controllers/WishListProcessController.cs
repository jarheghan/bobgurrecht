using RepositoryPattern.Mailers;
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
    public class WishListProcessController : BaseController
    {
        public WishListProcessController(ICategoryRepository categoryRepository
            ,IProductRepository productRepository, IPictureRepository pictureRepository,
            IOrderRepository orderRepository, IOrderItemsRepository orderItemRepository,
            IUserRepository userRepository, IProductVariationRepository prdVariationRepo, ICustomerInfoRepository customerRepository)
        {
           this._categoryRepository = categoryRepository;
           this._productRepository = productRepository;
           this._pictureRepository = pictureRepository;
           this._orderRepository = orderRepository;
           this._orderItemRepository = orderItemRepository;
           this._userRepository = userRepository;
            this._prdVariationRepo = prdVariationRepo;
            this._customerRepository = customerRepository;
        }
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPictureRepository _pictureRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsRepository _orderItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductVariationRepository _prdVariationRepo;
        private readonly ICustomerInfoRepository _customerRepository;
        private IUserMailer _userMailer = new UserMailer();
        public IUserMailer UserMailer
        {
            get { return _userMailer; }
            set { _userMailer = value; }
        }
        public IEnumerable<OrderItems> orderitems { get; set; }
        public ActionResult WishListProcessEmail()
        {
            if (HttpContext.User.Identity.Name != String.Empty)
            {
                var user = _userRepository.GetSingleUser(HttpContext.User.Identity.Name);
                var order = _orderRepository.GetOrderByUserID(user.ID);
                orderitems = _orderItemRepository.GetOrderItemsByOrderID(order.ID);
            }
                ProductVariation prdvariation = new ProductVariation();
                IEnumerable<EmailWishList> wishlist = orderitems.Select(x =>
                {
                    var product = _productRepository.GetProductByID(x.ProductID);
                    var prdpic = _productRepository.GetProductPictureByID(x.ProductID);
                    var pic = _pictureRepository.GetPictureById(prdpic.PictureID);
                    var order = _orderRepository.GetSingleOrderByOrderID(x.OrderID);
                    var customer = _customerRepository.GetSingleCustomerInfo(order.UserID);
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

                    return new EmailWishList
                    {
                        ProductName = product.Name,
                        FullName = customer.FirstName + " " + customer.LastName,
                        ProductVariationDesc = prdvariation.Description,
                        Quantity = orderItem.Quantity,
                        Picture = pic.FilePath,
                        OrderItemID = orderItem.ID,
                        OrderItemDate = (orderItem.AddDate ?? DateTime.Now).ToShortDateString(),
                        ProductNumber = product.SKU
                    };
                });
            
            UserMailer.EmailWishList(wishlist).Send();

            //return View();
            return Json("success", JsonRequestBehavior.AllowGet);
        }

    }
}
