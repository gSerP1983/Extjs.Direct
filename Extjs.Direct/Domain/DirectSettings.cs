using System;

namespace Extjs.Direct.Domain
{
    public class DirectSettings
    {
        public DirectSettings(string @namespace)
        {
            if (string.IsNullOrWhiteSpace(@namespace))
                throw new ArgumentNullException("namespace");

            BufferLength = 10;
            MaxRetries = 1;
            Namespace = @namespace;
        }
        
        public int BufferLength { get; set; }        
        public int MaxRetries { get; set; }
        public string Namespace { get; private set; }
        
        public int? Timeout { get; set; }
        public string Url { get; set; }
        public string Id { get; set; }
    }
}