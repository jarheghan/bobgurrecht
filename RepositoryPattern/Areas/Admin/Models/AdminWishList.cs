using PagedList;
using RepositoryPattern.Model.Catalog;
using RepositoryPattern.Model.Customers;
using RepositoryPattern.Model.Media;
using RepositoryPattern.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryPattern.Areas.Admin.Models
{
    public class AdminWishList
    {
        public int? Page { get; set; }
        public IPagedList<CustomerWishList> CustomerWishList { get; set; }
    }

    public class CustomerWishList
    {
        public string FullName { get; set; }
        public string ProductName { get; set; }
        public string ProductVariationDesc { get; set; }
        public int Quantity { get; set; }
        public string Picture { get; set; }
        public int OrderItemID { get; set; }
        public string OrderItemDate { get; set; }
    }
}