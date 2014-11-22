
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using RepositoryPattern.Model.Catalog;

namespace RepositoryPattern.Areas.Admin.Models
{
    public class CategorySearchModel
    {
        public int? Page { get; set; }
        public string CategoryName { get; set; }
        public string SKU { get; set; }
        public IPagedList<Category> SearchResult { get; set; }
        public string  SearchButton { get; set; }
    }
}