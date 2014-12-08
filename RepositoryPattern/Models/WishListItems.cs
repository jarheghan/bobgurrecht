using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryPattern.Models
{
    public class WishListItems
    {
        public int ProductID { get; set; }
        public int ProductVariationID { get; set; }
        public int Quantity { get; set; }
    }

    public class EmailWishList
    {
        public string FullName { get; set; }
        public string ProductName { get; set; }
        public string ProductVariationDesc { get; set; }
        public int Quantity { get; set; }
        public string Picture { get; set; }
        public int OrderItemID { get; set; }
        public string OrderItemDate { get; set; }
        public string ProductNumber { get; set; }
    }
}