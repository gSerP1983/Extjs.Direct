using Newtonsoft.Json;

namespace Extjs.Direct.Extension
{
    public static class ObjectExt
    {
        public static string AsJson(this object obj)
        {
            return obj == null ? null : JsonConvert.SerializeObject(obj);
        }
    }
}