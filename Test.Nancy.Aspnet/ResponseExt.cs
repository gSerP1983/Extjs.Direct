using Nancy;
using Nancy.Responses;
using Newtonsoft.Json;

namespace Test.Nancy.Aspnet
{
    public static class ResponseExt
    {
        public static Response AsJavaScript(this IResponseFormatter formatter, string contents)
        {
            return new TextResponse(contents, "text/javascript");
        }

        public static Response AsJavaScript(this IResponseFormatter formatter, object contents)
        {
            return AsJavaScript(formatter, JsonConvert.SerializeObject(contents));
        }
    }
}
