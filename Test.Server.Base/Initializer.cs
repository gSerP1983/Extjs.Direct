using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Extjs.Direct;
using Extjs.Direct.Domain;

namespace Test.Server.Base
{
    public static class Initializer
    {
        public static void Initialize()
        {
            Executor.Initialize(
                GetRpcTypes(), 
                CreateObject, 
                new DirectSettings("serp1983"), 
                LogError
            );
        }

        private static object CreateObject(Type type, object context)
        {
            var any = type.GetConstructors().Any(x => x.GetParameters().Length == 1);
            return any ? Activator.CreateInstance(type, context) : Activator.CreateInstance(type);
        }

        private static IEnumerable<Type> GetRpcTypes()
        {
            yield return typeof(BlStaticClass);
            yield return typeof(BlDefaultCtorClass);
            yield return typeof(BlContextCtorClass);
        }

        private static void LogError(Exception x)
        {
            // use here log4net or nlog or custom logger or etc
            Debug.WriteLine(x.ToString());
        }
    }
}
