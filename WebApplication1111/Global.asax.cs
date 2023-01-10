using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1111
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var routes = RouteTable.Routes;
            routes.MapRoute("Default", "{controller}/{action}",
                new { controller = "Home", action = "index" }
                );
            //AreaRegistration.RegisterAllAreas();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
