namespace Extjs.Direct.Extension
{
    public static class StringExt
    {
        public static bool IsJsonArray(this string parameter)
        {
            return parameter.StartsWith("[") && parameter.EndsWith("]");
        }

        public static bool IsJsonObject(this string parameter)
        {
            return parameter.StartsWith("{") && parameter.EndsWith("}");
        }

        public static bool IsJson(this string parameter)
        {
            return IsJsonObject(parameter) || IsJsonArray(parameter);
        }
    }
}