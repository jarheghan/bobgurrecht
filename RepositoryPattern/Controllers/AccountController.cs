using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using WebMatrix.WebData;
using RepositoryPattern.Models;
using RepositoryPattern.Model.Customers;

namespace RepositoryPattern.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public AccountController(ICustomerInfoRepository CustomerRepository, IUserRepository userRepository)
        {
            _userRepo = userRepository;
            _customerRepo = CustomerRepository;
        }
        private readonly IUserRepository _userRepo;
        private readonly ICustomerInfoRepository _customerRepo;

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

        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRole(string rolename)
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("RPConnection", "Roles", "RoleId", "RoleName", autoCreateTables: true);
            var roles = (SimpleRoleProvider)Roles.Provider;
            if (!roles.RoleExists(rolename))
            {
                roles.CreateRole(rolename);

            }
            return View();
        }


        [HttpPost]
        public ActionResult Login(Users user)
        {
            if (ModelState.IsValid)
            {

                bool success = WebSecurity.Login(user.Username, user.Password);

                if (success)
                {
                    var username = HttpContext.User.Identity;
                    string returnurl = Request.QueryString["ReturnUrl"];
                    if (returnurl == null)
                    {
                        Response.Redirect("~/");
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
            Response.Redirect("~/");
            return View();
        }

        public ActionResult SignInModal()
        {
            return PartialView();
        }

        public ActionResult LoginModal(Users user)
        {
            Errors err = new Errors();
            if (ModelState.IsValid)
            {

                bool success = WebSecurity.Login(user.Username, user.Password);

                if (success)
                {
                    var username = HttpContext.User.Identity;
                    string returnurl = Request.QueryString["ReturnUrl"];
                    if (returnurl == null)
                    {
                        // Response.Redirect("~/");
                        err.Type = "1";
                        err.Message = "/Home/Index";
                        return Json(err, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        err.Type = "1";
                        err.Message = returnurl;
                        return Json(err, JsonRequestBehavior.AllowGet);
                    }
                }
                if (!success)
                {
                   
                    err.Message = "Username or Password may be incorrect. Please enter valid username and password";
                    err.Type = "2";
                    return Json(err,JsonRequestBehavior.AllowGet);
                }
            }

            return null;

        }

        public ActionResult CustomerRegistration()
        {
            return PartialView();
        }

        public ActionResult RegisterCustomer(Users users, CustomerInfo customer)
        {
            Errors err = new Errors();
            if (users.Password != users.ConfirmPassword)
            {
                var err1 = new Errors
                {
                    Message = "Password does not match",
                    Type = "5"
                };
                return Json(err1, JsonRequestBehavior.AllowGet);
            }
            if (users.Password == users.ConfirmPassword)
            {
                WebSecurity.CreateUserAndAccount(users.Username, users.Password);
                string rolename = "Customer";
               
                var roles = (SimpleRoleProvider)Roles.Provider;
                if (roles.RoleExists(rolename))
                {
                    string[] arrayUser = { users.Username };
                    string[] arrayRole = {rolename};
                    roles.AddUsersToRoles(arrayUser, arrayRole);
                }
                if (!roles.RoleExists(rolename))
                {
                    roles.CreateRole(rolename);
                    string[] arrayUser = { users.Username };
                    string[] arrayRole = { rolename };
                    roles.AddUsersToRoles(arrayUser, arrayRole);
                }

                var usr = _userRepo.GetSingleUser(users.Username);
                customer.ID = usr.ID;
                customer.AddUser = users.Username;
                customer.AddDate = DateTime.Now;
                customer.DeleteFlag = false;
                customer.Active = true;
                customer.CustomerGuid = Guid.NewGuid();

               int i =  _customerRepo.InsertCustomerInfo(customer);
               if (i > 0)
               {
                   bool success = WebSecurity.Login(users.Username, users.Password);

                   if (success)
                   {
                       var username = HttpContext.User.Identity;
                       string returnurl = Request.QueryString["ReturnUrl"];
                       if (returnurl == null)
                       {
                           // Response.Redirect("~/");
                           err.Type = "1";
                           err.Message = "/Home/Index";
                           return Json(err, JsonRequestBehavior.AllowGet);
                       }
                       else
                       {
                           err.Type = "1";
                           err.Message = returnurl;
                           return Json(err, JsonRequestBehavior.AllowGet);
                       }
                   }
               }
                //var err2 = new Errors
                //{
                //    Message = "Success",
                //    Type = "2"
                //};
                //return Json(err2, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public ActionResult RecoverForgottenPassword()
        {
            WebSecurity.UserExists(
            return null;
        }
    }
}
