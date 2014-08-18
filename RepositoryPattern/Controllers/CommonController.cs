using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Controllers
{
    public class CommonController : BaseController
    {
        //
        // GET: /Common/

        public ActionResult Menu()
        {
            return PartialView();
        }

    }
}
