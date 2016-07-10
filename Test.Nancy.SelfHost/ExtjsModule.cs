using Extjs.Direct;
using Extjs.Direct.Extension;
using Nancy;
using Nancy.Extensions;

namespace Test.Nancy.SelfHost
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