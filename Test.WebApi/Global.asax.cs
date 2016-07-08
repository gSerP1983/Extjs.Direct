using System.Web;
using System.Web.Http;
using Test.Server.Base;

namespace Test.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Initializer.Initialize();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
