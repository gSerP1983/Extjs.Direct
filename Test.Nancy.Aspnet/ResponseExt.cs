using Extjs.Direct.Extension;
using Nancy;
using Nancy.Responses;

namespace Test.Nancy.Aspnet
{
    public static class ResponseExt
    {
        public static Response AsJavaScript(this IResponseFormatter formatter, string contents)
        {
            return new TextResponse(contents, "text/javascript");
        }
    }
}
