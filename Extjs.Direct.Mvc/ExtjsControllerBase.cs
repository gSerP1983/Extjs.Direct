using System.Web.Mvc;
using Extjs.Direct.Extension;

namespace Extjs.Direct.Mvc
{
    public class ExtjsControllerBase : Controller
    {
        public ActionResult Meta()
        {
            if (Request.HttpMethod == "OPTIONS")
                return ExtjsControllerHelper.Ok;

            return Json(Executor.Instance.MetaScript(), JsonRequestBehavior.AllowGet);
        }

        // for OPTIONS
        public ActionResult Rpc()
        {
            return ExtjsControllerHelper.Ok;
        }

        [HttpPost]
        // ReSharper disable once UnusedParameter.Global
        public ActionResult Rpc(object emptyParameter)
        {
            var request = Request.InputStream.AsString();
            return new NewtonsoftJsonResult(Executor.Instance.Execute(request, HttpContext));
        }
    }
}