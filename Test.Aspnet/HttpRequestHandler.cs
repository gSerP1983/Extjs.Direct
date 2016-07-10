using System.Threading.Tasks;
using System.Web;
using Extjs.Direct;
using Extjs.Direct.Extension;
using Test.Server.Base;

namespace Test.Aspnet
{
    public class HttpRequestHandler : HttpTaskAsyncHandler
    {
        static HttpRequestHandler()
        {
            Initializer.Initialize();
        }

        public override Task ProcessRequestAsync(HttpContext context)
        {
            EnableCors(context);

            var path = context.Request.Path.Trim('/').ToUpperInvariant();
            if (path == "META" && context.Request.HttpMethod == "GET")
            {
                context.Response.ContentType = "application/json";
                context.Response.Write(Executor.Instance.Meta().AsJson());
            }

            if (path == "RPC" && context.Request.HttpMethod == "POST")
            {
                context.Response.ContentType = "application/json";
                context.Response.Write(Executor.Instance.Execute(context.Request.InputStream.AsString(), context).AsJson());
            }

            return Task.FromResult("ok");
        }

        private static void EnableCors(HttpContext httpContext)
        {
            var origin = httpContext.Request.Headers["Origin"];
            if (origin != null)
            {
                httpContext.Response.AppendHeader("Access-Control-Allow-Origin", origin);
                httpContext.Response.AppendHeader("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
                httpContext.Response.AppendHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Authorization");
            }
        }
    }
}