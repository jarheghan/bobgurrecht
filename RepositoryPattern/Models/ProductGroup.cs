using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepositoryPattern.Model.Catalog;
using RepositoryPattern.Model.Media;

namespace RepositoryPattern.Models
{
    public class ProductGroup
    {
        public Product Product { get; set; }
        public IEnumerable<ProductVariation> ProductVariation { get; set; }
        public ProductPicture ProductPic { get; set; }
        public Picture Picture { get; set; }
    }
}