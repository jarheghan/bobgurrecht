using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using WebMatrix.WebData;
using RepositoryPattern.Models;

namespace RepositoryPattern.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Register()
        {
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
            Errors err = new Errors();
            return View(err);
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            if (ModelState.IsValid)
            {
                bool success = WebSecurity.Login(user.Username, user.Password);
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
                if (!success)
                {
                    Errors err = new Errors();
                    err.Message = "Username or Password may be incorrect. Please enter valid username and password";
                    err.Type = "2";
                    return View(err);
                }
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
