using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Extjs.Direct;
using Extjs.Direct.Extension;

namespace Test.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExtjsController : ApiController
    {
        [AcceptVerbs("GET")]
        public string Meta()
        {
            return Executor.Instance.Meta().AsJson();
        }

        [AcceptVerbs("POST")]
        public string Rpc()
        {
            var httpContext = (HttpContextWrapper)Request.Properties["MS_HttpContext"];
            var request = httpContext.Request.InputStream.AsString();
            return Executor.Instance.Execute(request, httpContext).AsJson();
        }
    }
}
