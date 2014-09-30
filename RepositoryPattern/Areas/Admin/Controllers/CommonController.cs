using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Areas.Admin.Controllers
{
    [Authorize]
    public class CommonController : Controller
    {
        //
        // GET: /Admin/Common/

        public ActionResult Menu()
        {
            return PartialView();
        }

        public ActionResult BodySide()
        {
            return PartialView();
        }

        public ActionResult Header()
        {
            return PartialView();
        }

        public ActionResult ProductVariation()
        {
            return PartialView();
        }

    }
}
