using Extjs.Direct;
using Extjs.Direct.Nancy;
using Nancy.Extensions;
using Nancy;

namespace Test.Nancy.Aspnet
{
    public class ExtjsModule : NancyModule
    {
        public ExtjsModule()
        {
            Get["/meta/"] = _ => Response.AsJavaScript(Executor.Instance.Meta());
            Post["/"] = _ => Response.AsJavaScript(Executor.Instance.Execute(Request.Body.AsString(), Context));
        }
    }
}