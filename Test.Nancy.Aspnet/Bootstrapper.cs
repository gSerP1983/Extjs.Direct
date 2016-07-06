using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Extjs.Direct;
using Extjs.Direct.Domain;
using Nancy;

namespace Test.Nancy.Aspnet
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        static Bootstrapper()
        {
            Executor.Initialize(GetRpcTypes(), CreateObject, new DirectSettings("TestMvc"), LogError);
        }

        private static object CreateObject(Type type, object context)
        {
            var any = type.GetConstructors().Any(x =>
                x.GetParameters().Length == 1 &&
                typeof(HttpContextBase).IsAssignableFrom(x.GetParameters().First().ParameterType)
                );

            if (any)
            {
                var nancyContext = context as NancyContext;
                if (nancyContext == null)
                    throw new InvalidCastException("Expected type NancyContext.");

                return Activator.CreateInstance(type, nancyContext);
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