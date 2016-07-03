using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Test.MVC.Startup))]
namespace Test.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
