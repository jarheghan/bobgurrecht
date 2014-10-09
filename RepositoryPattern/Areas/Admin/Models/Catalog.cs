using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepositoryPattern.Model.Catalog;

namespace RepositoryPattern.Areas.Admin.Models
{
    public class Catalog
    {
        public Product Product { get; set; }
        public IList<ProductVariation> ProductVariation { get; set; }
    }
}