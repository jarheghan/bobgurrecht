using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepositoryPattern.Model.Catalog;
using RepositoryPattern.Model.Media;
using RepositoryPattern.Model.Sales;


namespace RepositoryPattern.Models
{
    public class WishListDisplay
    {
        public Product Product { get; set; }
        public Picture Picture { get; set; }
        public OrderItems OrderItems { get; set; }
        public ProductVariation ProductVariation { get; set; }
    }
}