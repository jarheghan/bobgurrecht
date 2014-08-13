using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryPattern.Data;
using RepositoryPattern.Infrastructure;
using RepositoryPattern.Model;
using System.Collections.Generic;
using RepositoryPattern.Model.Product;
namespace RepositortPattern.Test
{
    [TestClass]
    public class UnitTest1
    {
        public IProductRepository _productRepo;

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
            
        }
    }
}
