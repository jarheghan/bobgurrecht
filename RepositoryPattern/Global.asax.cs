using RepositoryPattern.Infrastructure.configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;

namespace RepositoryPattern
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static SimpleMembershipInitialize _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
            Log4NetManager.InitializeLog4Net();
        }
    }


    public class SimpleMembershipInitialize
    {
        public SimpleMembershipInitialize()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("RPConnection", "Users", "Id", "Username", autoCreateTables: true);
                //WebSecurity.InitializeDatabaseConnection("RPConnection", "Roles", "RoleId", "RoleName", autoCreateTables: true);
            }
        }
    }
}