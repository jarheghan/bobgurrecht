using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace RepositoryPattern.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Register()
        {
            //if (!WebSecurity.Initialized)
            //{
            //    WebSecurity.InitializeDatabaseConnection("RPConnection", "Users", "Id", "Username", autoCreateTables: true);
            //}
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            WebSecurity.CreateUserAndAccount(form["username"], form["password"], new { DisplayName = form["displayname"], Country = form["country"] });
            Response.Redirect("~/Account/Login");
            return View();

        }

        public ActionResult Login()
        {
            //if (!WebSecurity.Initialized)
            //{
            //    WebSecurity.InitializeDatabaseConnection("RPConnection", "Users", "Id", "Username", autoCreateTables: true);
            //}
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            bool success = WebSecurity.Login(form["username"], form["password"]);
            if (success)
            {
                string returnurl = Request.QueryString["ReturnUrl"];
                if (returnurl == null)
                {
                    Response.Redirect("~/Admin/");
                }
                else
                    Response.Redirect(returnurl);
            }
            return View();
        }
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            Response.Redirect("~/Account/Login");
            return View();
        }

    }
}
