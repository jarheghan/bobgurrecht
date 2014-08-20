using RepositoryPattern.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Administration/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
