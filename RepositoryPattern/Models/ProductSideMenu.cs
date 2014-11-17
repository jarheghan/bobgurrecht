using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepositoryPattern.Model.Catalog;

namespace RepositoryPattern.Models
{
    public class ProductSideMenu
    {
        public Category SingleCategory { get; set; }
        public IEnumerable<Category> MultipleCategory { get; set; }
    }
}