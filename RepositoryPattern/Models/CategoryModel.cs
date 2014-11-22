using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepositoryPattern.Model.Catalog;
using RepositoryPattern.Model.Media;

namespace RepositoryPattern.Models
{
    public class CategoryModel
    {
        public Category Category { get; set; }
        public Picture Picture { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}