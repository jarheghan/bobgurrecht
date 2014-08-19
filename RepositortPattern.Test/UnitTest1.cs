using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryPattern.Data;
using RepositoryPattern.Infrastructure;
using RepositoryPattern.Model;
using System.Collections.Generic;
using RepositoryPattern.Model.Catalog;
using System.Linq;
namespace RepositortPattern.Test
{
    [TestClass]
    public class UnitTest1
    {
        public IProductRepository _productRepo;
        public ICategoryRepository _categoryRepo;

        [TestMethod]
        public void insert_product_into_the_product_table()
        {
            _productRepo = new ProductDataMapper();
           
            List<Product> prd = new List<Product>();
            prd.Add(new Product { Name = "Hair Cream", Description = "This is to make your hair", Price = 50,Category = 1});
            prd.Add(new Product { Name = "Baby Cloth", Description = "This is for babies to wear", Price = 40, Category = 1 });
            prd.Add(new Product { Name = "Hand band", Description = "This is to wear in your hands", Price = 45, Category = 1 });
            prd.Add(new Product { Name = "LapTops", Description = "This is to make your hair", Price = 500,Category = 2});
            prd.Add(new Product { Name = " Computer Mouses", Description = "This is for babies to wear", Price = 42, Category = 2 });
            prd.Add(new Product { Name = "Bluetooth Device", Description = "This is to wear in your hands", Price = 450, Category = 2 });
            
            foreach (var p in prd)
            {
                _productRepo.Add(p);
            }

        }

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
            prdcat.Add(new ProductCategory { ProductID = 1, CategoryID = 3, DisplayOrder = 1, IsFeaturedProduct = false });
            prdcat.Add(new ProductCategory { ProductID = 2, CategoryID = 3, DisplayOrder = 2, IsFeaturedProduct = false });
            prdcat.Add(new ProductCategory { ProductID = 3, CategoryID = 4, DisplayOrder = 3, IsFeaturedProduct = false });
            prdcat.Add(new ProductCategory { ProductID = 4, CategoryID = 4, DisplayOrder = 4, IsFeaturedProduct = false });
            prdcat.Add(new ProductCategory { ProductID = 5, CategoryID = 5, DisplayOrder = 5, IsFeaturedProduct = false });

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
                                return new ProductCategory()
                                {
                                    ID = x.ID,
                                    CategoryID = x.CategoryID,
                                    ProductID = x.ProductID,
                                    Product = product,
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
    }
}
