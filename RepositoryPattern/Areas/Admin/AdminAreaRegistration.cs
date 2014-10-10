using System.Web.Mvc;

namespace RepositoryPattern.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
          
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {controller="Home", action = "Index", area="Admin" ,id = UrlParameter.Optional },
                new[] { "RepositoryPattern.Areas.Admin.Controllers" }
            );

            context.MapRoute(
             "Admin_Product",
             "Admin/{controller}/{action}/{Id}",
             new { controller = "Product", action = "Edit", area = "Admin", Id = -1 },
             new[] { "RepositoryPattern.Areas.Admin.Controllers" }
         );
          
        }
    }
}
