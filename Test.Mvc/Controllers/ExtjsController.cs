using System.Web.Mvc;
using Extjs.Direct;
using Extjs.Direct.Extension;
using Test.Mvc.Util;

namespace Test.Mvc.Controllers
{
    [EnableCorsFilter]
    public class ExtjsController : Controller
    {
        public ActionResult Meta()
        {
            if (Request.HttpMethod == "OPTIONS")
                return ExtjsControllerHelper.Ok;

            return Json(Executor.Instance.Meta(), JsonRequestBehavior.AllowGet);
        }

        // OPTIONS
        public ActionResult Rpc()
        {
            return ExtjsControllerHelper.Ok;
        }

        [HttpPost]
        // ReSharper disable once UnusedParameter.Global
        public ActionResult Rpc(object empty)
        {
            var request = Request.InputStream.AsString();
            return new NewtonsoftJsonResult(Executor.Instance.Execute(request, HttpContext));
        }
    }
}