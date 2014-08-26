using RepositoryPattern.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Admin/Product/

        public ActionResult Index()
        {
            return View();
        }

    }
}
