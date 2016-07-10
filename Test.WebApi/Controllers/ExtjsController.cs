using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Extjs.Direct;
using Extjs.Direct.Extension;
using Test.WebApi.Util;

namespace Test.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExtjsController : ApiController
    {
        [AcceptVerbs("GET")]
        public IHttpActionResult Meta()
        {
            return new RawJsonActionResult(Executor.Instance.Meta().AsJson());
        }

        [AcceptVerbs("POST")]
        public IHttpActionResult Rpc()
        {
            var httpContext = (HttpContextWrapper)Request.Properties["MS_HttpContext"];
            var request = httpContext.Request.InputStream.AsString();
            return new RawJsonActionResult(Executor.Instance.Execute(request, httpContext).AsJson());
        }
    }
}
