using System.Web;
using System.Web.Mvc;

namespace Test.Mvc.Util
{
    public static class ExtjsControllerHelper
    {
        public static ActionResult Ok
        {
            get { return new HttpStatusCodeResult(200); }
        }

        public static void EnableCors(HttpContextBase httpContext)
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