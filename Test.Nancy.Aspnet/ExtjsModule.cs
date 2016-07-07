using Extjs.Direct;
using Extjs.Direct.Extension;
using Nancy.Extensions;
using Nancy;

namespace Test.Nancy.Aspnet
{
    public class ExtjsModule : NancyModule
    {
        public ExtjsModule()
        {
            Get["/meta/"] = _ => Response.AsJavaScript(Executor.Instance.Meta().AsJson());
            Post["/rpc/"] = _ => Response.AsJavaScript(Executor.Instance.Execute(Request.Body.AsString(), Context).AsJson());
        }
    }
}