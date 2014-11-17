using log4net;
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
    public partial class CommonController : BaseController
    {
        //
        // GET: /Common/
        //public CommonController() { }

        ILog log = LogManager.GetLogger(typeof(CommonController));
        public CommonController(ICategoryRepository categoryRepository
            ,IProductRepository productRepository, IPictureRepository pictureRepository,
            IOrderRepository orderRepository, IOrderItemsRepository orderItemRepository,
            IUserRepository userRepository,IProductVariationRepository prdVariationRepo)
        {
           this._categoryRepository = categoryRepository;
           this._productRepository = productRepository;
           this._pictureRepository = pictureRepository;
           this._orderRepository = orderRepository;
           this._orderItemRepository = orderItemRepository;
           this._userRepository = userRepository;
            this._prdVariationRepo = prdVariationRepo;
        }
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPictureRepository _pictureRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsRepository _orderItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductVariationRepository _prdVariationRepo;
        
        public ActionResult Menu(bool removeWL = false)
        {
            ViewBag.RemoveWL = removeWL;
            IEnumerable<Category> cat = _categoryRepository.GetAllCategories();
            return PartialView(cat);
        }

        public ActionResult Slider()
        {
            return PartialView();
        }

        public ActionResult DisplayCategoryProduct(int? Id)
        {
            try
            {
                var productCategory = _categoryRepository.GetProductCategoriesByCategoryID(Id ?? default(int));
                var catg = _categoryRepository.GetCategoryById(Id ?? default(int));

                IEnumerable<ProductCategory> prdcat = productCategory
                            .Select(x =>
                            {
                                var product = _productRepository.GetProductByID(x.ProductID ?? default(int));
                                var category = _categoryRepository.GetCategoryById(x.CategoryID ?? default(int));
                                var prdPic = _productRepository.GetProductPictureByID(x.ProductID ?? default(int));
                                var pic = _pictureRepository.GetPictureById(prdPic.PictureID);
                                return new ProductCategory()
                                {
                                    ID = x.ID,
                                    CategoryID = x.CategoryID,
                                    ProductID = x.ProductID,
                                    Product = product,
                                    Picture = pic,
                                    Category = category,
                                    IsFeaturedProduct = x.IsFeaturedProduct,
                                    AddDate = x.AddDate,
                                    AddUser = x.AddUser,
                                    ChangeDate = x.ChangeDate,
                                    ChangeUser = x.ChangeUser,
                                    DeleteFlag = x.DeleteFlag
                                };
                            });
                ViewBag.Category = catg;

                return PartialView(prdcat);
            }
            catch (Exception ex) { log.Error("Common Error:", ex); return PartialView(); }
        }


        public ActionResult DisplayCategorySubCategory(int? Id)
        {
            try
            {
                var productCategory = _categoryRepository.GetProductCategoriesByCategoryID(Id ?? default(int));
                var catg = _categoryRepository.GetAllCategoriesByParentCategoryId(Id ?? default(int));
                var catparent = _categoryRepository.GetCategoryById(Id ?? default(int));

                ViewBag.Category = catparent;

                return PartialView(catg);
            }
            catch (Exception ex) { log.Error("Common Error:", ex); return PartialView(); }
        }

        public ActionResult SideMenu(int Id)
        {
            IEnumerable<Category> cat = _categoryRepository.GetAllCategories();
            ViewBag.catId = Id;
            return PartialView(cat);
        }


        public ActionResult SideMenuFullCategory(int Id)
        {
            ProductSideMenu psm = new ProductSideMenu();
            Category cat = _categoryRepository.GetCategoryById(Id);
            var multipleCat = _categoryRepository.GetAllCategories();
            psm.SingleCategory = cat;
            psm.MultipleCategory = multipleCat;
            return PartialView(psm);
        }

        public ActionResult DisplayFeatureProduct()
        {
            var feautureProducts = _productRepository.GetAllFeatureProduct();
            IEnumerable<ProductCategory> prdcat = feautureProducts
                                        .Select(x => 
                                            {
                                                var prdPic = _productRepository.GetProductPictureByID(x.ID);
                                                var pic = _pictureRepository.GetPictureById(prdPic.PictureID);
                                                var product = _productRepository.GetProductByID(x.ID);
                                                var prdCat1 = _categoryRepository.GetProductCategoriesByProductID(x.ID);
                                                return new ProductCategory()
                                                {
                                                    Picture = pic,
                                                    Product = product,
                                                    CategoryID = prdCat1.CategoryID
                                                };
                                            });
            return View(prdcat);
        }


        public ActionResult MiniWishlist()
        {
            int cnt;
            IEnumerable<OrderItems> orderitems;
            if (HttpContext.User.Identity.Name != String.Empty)
            {
                var user = _userRepository.GetSingleUser(HttpContext.User.Identity.Name);
                var order = _orderRepository.GetOrderByUserID(user.ID);

                if (order != null)
                {
                    orderitems = _orderItemRepository.GetOrderItemsByOrderID(order.ID);
                    if (orderitems != null)
                    {
                        cnt = orderitems.Count();
                        return PartialView(cnt);
                    }
                }
                else
                {
                    return PartialView(0);
                }
            }
            return PartialView(0);
        }

        public ActionResult MainWishList()
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
                            var pic = _pictureRepository.GetPictureById(prdpic.PictureID);
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

                    return PartialView(wishlist);
                }
                else
                {
                    return PartialView();
                }
            }
            return PartialView();
        }

        public ActionResult DisplayTopProductRequested()
        {
            var feautureProducts = _productRepository.GetAllProduct();
            IEnumerable<ProductCategory> prdcat = feautureProducts
                                        .Select(x =>
                                        {
                                            var prdPic = _productRepository.GetProductPictureByID(x.ID);
                                            var pic = _pictureRepository.GetPictureById(prdPic.PictureID);
                                            var product = _productRepository.GetProductByID(x.ID);
                                            var prdCat1 = _categoryRepository.GetProductCategoriesByProductID(x.ID);
                                            return new ProductCategory()
                                            {
                                                Picture = pic,
                                                Product = product,
                                                CategoryID = prdCat1.CategoryID
                                            };
                                        });
            return View(prdcat);
        }

    }
}
