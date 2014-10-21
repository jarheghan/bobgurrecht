using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryPattern.Areas.Admin.Models
{
    public class ProductVariationInfo
    {
        public int ID { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
    }
}