using RepositoryPattern.Areas.Admin.Models;
using RepositoryPattern.Controllers;
using RepositoryPattern.Model.Catalog;
using RepositoryPattern.Model.Customers;
using RepositoryPattern.Model.Media;
using RepositoryPattern.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace RepositoryPattern.Areas.Admin.Controllers
{
    public class WishListController : BaseController
    {
        //
        // GET: /Admin/WishList/
        public WishListController(ICategoryRepository categoryRepository
            ,IProductRepository productRepository, IPictureRepository pictureRepository,
            IOrderRepository orderRepository, IOrderItemsRepository orderItemRepository,
            IUserRepository userRepository,IProductVariationRepository prdVariationRepo, ICustomerInfoRepository customerRepository)
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
        
        public ActionResult List(AdminWishList adminList)
        {
            AdminWishList adminwishList = new AdminWishList();
              const int RecordsPerPage = 10;
              var pageIndex =  adminList.Page ?? 1;
                    var orderitems = _orderItemRepository.GetAllOrderItems();
                    ProductVariation prdvariation = new ProductVariation();
                    IEnumerable<CustomerWishList> wishlist = orderitems.Select( x => 
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

                            return new CustomerWishList
                            {
                               ProductName = product.Name,
                               FullName = customer.FirstName + " " + customer.LastName,
                               ProductVariationDesc = prdvariation.Description,
                               Quantity = orderItem.Quantity,
                               Picture = pic.FilePath,
                               OrderItemID = orderItem.ID,
                               OrderItemDate = (orderItem.AddDate??DateTime.Now).ToShortDateString()
                            };
                        });
                    adminwishList.CustomerWishList = wishlist.ToPagedList(pageIndex, RecordsPerPage);
                    return View(adminwishList);
        }

        public ActionResult RemoveWishList(int OrderItemId)
        {

            int result = _orderItemRepository.DeleteOrderItems(OrderItemId);
            return RedirectToAction("List");
        }

    }
}
