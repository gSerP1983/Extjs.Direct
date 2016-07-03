using System.IO;

namespace Extjs.Direct.Extension
{
    public static class StreamExt
    {
        public static string AsString(this Stream stream)
        {
            if (stream == null)
                return string.Empty;

            stream.Position = 0;
            using (var sr = new StreamReader(stream))
                return sr.ReadToEnd().Trim();
        }
    }
}