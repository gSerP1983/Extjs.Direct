using System;
using System.Collections.Generic;
using System.Linq;
using Extjs.Direct;
using Extjs.Direct.Domain;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Response = Nancy.Response;

namespace Test.Nancy.Aspnet
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        static Bootstrapper()
        {
            Executor.Initialize(GetRpcTypes(), CreateObject, new DirectSettings("TestMvc"), LogError);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            // CORS Enable
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx => WithCors(ctx.Response));
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            // CORS Enable
            for (var i = 0; i < conventions.StaticContentsConventions.Count(); i++)
            {
                var convention = conventions.StaticContentsConventions[i];
                conventions.StaticContentsConventions[i] = (ctx, root) =>
                {
                    var response = convention.Invoke(ctx, root);
                    return WithCors(response);
                };
            }
        }

        private static Response WithCors(Response response)
        {
            if (response == null)
                return null;

            return response
                .WithHeader("Access-Control-Allow-Origin", "*") // вместо '*' можно прочесть из ctx.Request.Headers["Origin"].FirstOrDefault()
                .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                .WithHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Authorization");
        }

        private static object CreateObject(Type type, object context)
        {
            var any = type.GetConstructors().Any(x =>
                x.GetParameters().Length == 1 &&
                typeof(NancyContext).IsAssignableFrom(x.GetParameters().First().ParameterType)
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