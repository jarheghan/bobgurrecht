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
}