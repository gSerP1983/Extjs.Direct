using System;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Extjs.Direct.Mvc
{
    public class NewtonsoftJsonResult : ActionResult
    {
        private readonly object _data;

        public NewtonsoftJsonResult(object data)
        {
            _data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var response = context.HttpContext.Response;
            response.Write(JsonConvert.SerializeObject(_data));
        }
    }
}