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
    public class OrderController : BaseController
    {
        //
        // GET: /Order/

        public OrderController(IProductRepository productRepository, ICategoryRepository categoryRepository,
            IProductVariationRepository prdVariationRepo, IPictureRepository pictureRepository, 
            IOrderRepository orderRepository, IOrderItemsRepository orderItemsRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _prdVariationRepo = prdVariationRepo;
            _pictureRepo = pictureRepository;
            _userRepo = userRepository;
            _orderItemsRepo = orderItemsRepository;
            _orderRepo = orderRepository;
        }
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductVariationRepository _prdVariationRepo;
        private readonly IPictureRepository _pictureRepo;
        private readonly IUserRepository _userRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderItemsRepository _orderItemsRepo;

        public ActionResult AddToWishList(WishListItems items)
        {
            var user = HttpContext.User.Identity;
            if (user.IsAuthenticated == true)
            {
                //First let get the User ID
                Order orders = new Order();
                OrderItems orderItems = new OrderItems();
                Errors err = new Errors();
                int orderID;
                var usermain = _userRepo.GetSingleUser(user.Name);
                bool orderExist = _orderRepo.OrderUserExist(usermain.ID);
                if (!orderExist)
                {
                    orders.Active = true;
                    orders.UserID = usermain.ID;
                    orders.AddUser = user.Name;
                    orders.AddDate = DateTime.Now;
                    orders.DeleteFlag = false;
                    orderID =  _orderRepo.InsertOrders(orders);

                    if (orderID != 0)
                    {
                        orderItems.OrderItemGuid = Guid.NewGuid();
                        orderItems.OrderID = orderID;
                        orderItems.ProductID = items.ProductID;
                        orderItems.ProductVariationID = items.ProductVariationID;
                        orderItems.Quantity = items.Quantity;
                        orderItems.AddUser = user.Name;
                        orderItems.AddDate = DateTime.Now;
                        orderItems.DeleteFlag = false;
                        if (_orderItemsRepo.OrderItemWithSameProductIDAndVariationIDAndOrderID(orderItems) == false)
                        {
                            _orderItemsRepo.InsertOrderItems(orderItems);
                            err.Message = "success";
                            return Json(err, JsonRequestBehavior.AllowGet);
                        }

                        err.Message = "Duplicate";
                        return Json(err, JsonRequestBehavior.AllowGet);
                    }
                }
                if (orderExist)
                {
                    orders = _orderRepo.GetOrderByUserID(usermain.ID);
                    if (orders.ID != 0)
                    {
                       
                        orderItems.OrderItemGuid = Guid.NewGuid();
                        orderItems.OrderID = orders.ID;
                        orderItems.ProductID = items.ProductID;
                        orderItems.ProductVariationID = items.ProductVariationID;
                        orderItems.Quantity = items.Quantity;
                        orderItems.AddUser = user.Name;
                        orderItems.AddDate = DateTime.Now;
                        orderItems.DeleteFlag = false;
                        if (_orderItemsRepo.OrderItemWithSameProductIDAndVariationIDAndOrderID(orderItems) == false)
                        {
                            _orderItemsRepo.InsertOrderItems(orderItems);
                            var orderItems1 = _orderItemsRepo.GetOrderItemsByOrderID(orders.ID);
                            return PartialView("MiniWishList", orderItems1.Count());
                            //err.Message = "success";
                            //return Json(err, JsonRequestBehavior.AllowGet);
                        }
                        err.Message = "Duplicate";
                        return Json(err, JsonRequestBehavior.AllowGet);
                    }
                }
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
