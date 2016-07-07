using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Test.Server.Base;

namespace Test.Nancy.Aspnet
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        static Bootstrapper()
        {
            Initializer.Initialize();
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
    }
}