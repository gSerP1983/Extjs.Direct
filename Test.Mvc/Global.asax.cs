using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Test.Server.Base;

namespace Test.Mvc
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Initializer.Initialize();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
