using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryPattern.Areas.Admin.Models
{
    public class HomeDisplay
    {
        public int CustomerCount { get; set; }
        public int ProductCount { get; set; }
        public int WishListCount { get; set; }
    }
}