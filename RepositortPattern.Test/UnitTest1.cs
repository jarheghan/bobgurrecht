using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryPattern.Data;
using RepositoryPattern.Infrastructure;
using RepositoryPattern.Model;
using System.Collections.Generic;
using RepositoryPattern.Model.Catalog;
using System.Linq;
using RepositoryPattern.Model.Media;
using RepositoryPattern.Model.Sales;
using RepositoryPattern.Model.Customers;
namespace RepositortPattern.Test
{
    [TestClass]
    public class UnitTest1
    {
        public IProductRepository _productRepo;
        public ICategoryRepository _categoryRepo;

       
        private  IProductRepository _productRepository;
        private  ICategoryRepository _categoryRepository;
        private  IProductVariationRepository _prdVariationRepo;
        private  IPictureRepository _pictureRepo;
        private  IOrderRepository _orderRepository;
        private  IOrderItemsRepository _orderItemRepository;
        private  IUserRepository _userRepository;

        //[TestMethod]
        //public void insert_product_into_the_product_table()
        //{
        //    _productRepo = new ProductDataMapper();
           
        //    List<Product> prd = new List<Product>();
        //    prd.Add(new Product { Name = "Hair Cream", Description = "This is to make your hair", Price = 50,Category = 1});
        //    prd.Add(new Product { Name = "Baby Cloth", Description = "This is for babies to wear", Price = 40, Category = 1 });
        //    prd.Add(new Product { Name = "Hand band", Description = "This is to wear in your hands", Price = 45, Category = 1 });
        //    prd.Add(new Product { Name = "LapTops", Description = "This is to make your hair", Price = 500,Category = 2});
        //    prd.Add(new Product { Name = " Computer Mouses", Description = "This is for babies to wear", Price = 42, Category = 2 });
        //    prd.Add(new Product { Name = "Bluetooth Device", Description = "This is to wear in your hands", Price = 450, Category = 2 });
            
        //    foreach (var p in prd)
        //    {
        //        _productRepo.Add(p);
        //    }

        //}

        [TestMethod]
        public void get_list_of_product_by_category()
        {
            _productRepo = new ProductDataMapper();
            IEnumerable<Product> prd = _productRepo.GetProductByCategory(1);
            int ss = prd.Count();
            Assert.AreEqual(ss, 4);
            
        }

        [TestMethod]
        public void insert_category_into_the_category_table()
        {
            _categoryRepo = new CategoryDataMapper();

            List<Category> cat = new List<Category>();
            cat.Add(new Category { Name = "Pipe/Hose/Tubing", Description = "Pipe/Hose/Tubing", Alias = "Pipe/Hose/Tubing", MetaKeyword = "", DisplayOrder = 1 });
            cat.Add(new Category { Name = "Supply", Description = "Supply", Alias = "Cloth", MetaKeyword = "", DisplayOrder = 1 });
            cat.Add(new Category { Name = "Plumbering Specialties", Description = "This Technology Garget", Alias = "Tech", MetaKeyword = "", DisplayOrder = 1 });
            cat.Add(new Category { Name = "Shower/Tub", Description = "This Cloth", Alias = "Cloth", MetaKeyword = "", DisplayOrder = 1 });
            cat.Add(new Category { Name = "Tools", Description = "Tools", Alias = "Tech", MetaKeyword = "", DisplayOrder = 1 });
            cat.Add(new Category { Name = "Evaperative Cooler", Description = "This Cloth", Alias = "Cloth", MetaKeyword = "", DisplayOrder = 1 });
            cat.Add(new Category { Name = "Water Cooler/Drinking Fountain", Description = "Water Cooler/Drinking Fountain", Alias = "Cloth", MetaKeyword = "", DisplayOrder = 1 });
            cat.Add(new Category { Name = "Disposer/Parts", Description = "Disposer/Parts", Alias = "Disposer/Parts", MetaKeyword = "", DisplayOrder = 1 });
            cat.Add(new Category { Name = "Fixtures", Description = "Fixtures", Alias = "Fixtures", MetaKeyword = "", DisplayOrder = 1 });

            foreach (var p in cat)
            {
                _categoryRepo.Add(p);
            }

        }

        [TestMethod]
        public void insert_productcategory_into_productcategory_table()
        {
            _categoryRepo = new CategoryDataMapper();
            List<ProductCategory> prdcat = new List<ProductCategory>();
            prdcat.Add(new ProductCategory { ProductID = 1, CategoryID = 1, DisplayOrder = 1, IsFeaturedProduct = false });
            prdcat.Add(new ProductCategory { ProductID = 2, CategoryID = 1, DisplayOrder = 2, IsFeaturedProduct = false });
            prdcat.Add(new ProductCategory { ProductID = 3, CategoryID = 2, DisplayOrder = 3, IsFeaturedProduct = false });
            prdcat.Add(new ProductCategory { ProductID = 4, CategoryID = 2, DisplayOrder = 4, IsFeaturedProduct = false });
            prdcat.Add(new ProductCategory { ProductID = 5, CategoryID = 3, DisplayOrder = 5, IsFeaturedProduct = false });

            foreach (var pc in prdcat)
            {
                _categoryRepo.InsertProductCategory(pc);
            }
        }

        [TestMethod]
        public void get_product_by_productid()
        {
            _productRepo = new ProductDataMapper();
            Product prd = _productRepo.GetProductByID(1);
            Console.WriteLine(prd.Name + " " + prd.Price + " " + prd.Description);

        }

        [TestMethod]
        public void get_list_of_productcategory_by_categoryid()
        {
            _categoryRepo = new CategoryDataMapper();
            _productRepo = new ProductDataMapper();
            var productCategory = _categoryRepo.GetProductCategoriesByCategoryID(1);
            
            var model = productCategory
                        .Select(x =>
                            {
                                var product = _productRepo.GetProductByID(x.ProductID ?? default(int));
                                var category = _categoryRepo.GetCategoryById(x.CategoryID ?? default(int));
                                return new ProductCategory()
                                {
                                    ID = x.ID,
                                    CategoryID = x.CategoryID,
                                    ProductID = x.ProductID,
                                    Product = product,
                                    Category = category,
                                    IsFeaturedProduct = x.IsFeaturedProduct,
                                    AddDate = x.AddDate,
                                    AddUser = x.AddUser,
                                    ChangeDate = x.ChangeDate,
                                    ChangeUser = x.ChangeUser,
                                    DeleteFlag = x.DeleteFlag
                                };
                            });
          

            Assert.AreEqual(3, productCategory.Count());
        }

        [TestMethod]
        public void test_to_grt_all_category()
        {
            _categoryRepository = new CategoryDataMapper();
            var category = _categoryRepository.GetAllCategories();
            var mainmenu = category.Where(x => x.ParentCategoryID == null);
            var submenu = from c in category
                          from m in mainmenu
                          where m.ID == c.ParentCategoryID
                          select new { c };
            var subsubMenu = from d in category
                             from s in submenu
                             where s.c.ID == d.ParentCategoryID
                             select new { d };

        }

        [TestMethod]
        public void test_to_get_top_10_requested_product()
        {
            _orderItemRepository = new OrderItemsDataMapper();
            var allOrderItem = _orderItemRepository.GetAllOrderItems();
            var top10OrderItem = from p in allOrderItem
                                 group p by p.ProductID into g
                                 select new { ProductID = g.Key, ProductCount = g.Count()};

            var sstop10 = top10OrderItem.Where(x => x.ProductCount > 2).Take(10);
        }
    }
}
