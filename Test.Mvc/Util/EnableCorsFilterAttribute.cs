using System.Web;
using System.Web.Mvc;

namespace Test.Mvc.Util
{
    public class EnableCorsFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();

            ExtjsControllerHelper.EnableCors(filterContext.RequestContext.HttpContext);

            base.OnActionExecuting(filterContext);
        }
    }
}