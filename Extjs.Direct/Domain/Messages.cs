// ReSharper disable InconsistentNaming
namespace Extjs.Direct.Domain
{
    public abstract class MessageBase
    {
        public string action { get; set; }
        public string method { get; set; }
        public string type { get; set; }
        public int tid { get; set; }
    }

    public class Request : MessageBase
    {
        public Request()
        {
            data = new object[] {};
        }

        public object[] data { get; set; }
    }

    public class Response : MessageBase
    {
        public object result { get; set; }
        public ResponseMeta meta { get; set; }

        public Response()
        {
            meta = new ResponseMeta {success = true};
        }

        public static Response Map(MessageBase message)
        {
            if (message == null)
                return null;

            return new Response
            {
                action = message.action,
                method = message.method,
                type = message.type,
                tid = message.tid,
            };
        }
    }

    public class ResponseMeta
    {
        public bool success { get; set; }
        public string msg{ get; set; }
        public string fullMsg { get; set; }
    }
}