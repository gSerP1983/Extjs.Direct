using Nancy;
using Nancy.Responses;

namespace Test.Nancy.SelfHost
{
    public static class ResponseExt
    {
        public static Response AsJavaScript(this IResponseFormatter formatter, string contents)
        {
            return new TextResponse(contents, "application/json");
        }
    }
}
