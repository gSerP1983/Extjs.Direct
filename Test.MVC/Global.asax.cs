using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Extjs.Direct;
using Extjs.Direct.Domain;

namespace Test.MVC
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // add this string
            Executor.Initialize(GetRpcTypes(), CreateObject, new DirectSettings("TestMvc"), LogError);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static object CreateObject(Type type, object context)
        {
            var any = type.GetConstructors().Any(x =>
                x.GetParameters().Length == 1 &&
                typeof (HttpContextBase).IsAssignableFrom(x.GetParameters().First().ParameterType)
                );

            if (any)
            {
                var httpContext = context as HttpContextBase;
                if (httpContext == null)
                    throw new InvalidCastException("Expected type HttpContextBase.");

                return Activator.CreateInstance(type, httpContext);
            }

            return Activator.CreateInstance(type);
        }

        private static IEnumerable<Type> GetRpcTypes()
        {
            yield return typeof(int);
        }


        private static void LogError(Exception x)
        {
            // use here log4net or nlog or custom logger or etc
        }

    }
}
