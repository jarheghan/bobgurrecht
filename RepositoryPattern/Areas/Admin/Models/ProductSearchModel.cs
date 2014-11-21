
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using RepositoryPattern.Model.Catalog;

namespace RepositoryPattern.Areas.Admin.Models
{
    public class ProductSearchModel
    {
        public int? Page { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public IPagedList<Product> SearchResult { get; set; }
        public string  SearchButton { get; set; }
    }
}