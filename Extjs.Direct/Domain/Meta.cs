// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Extjs.Direct.Domain
{
    public class RemotingApi
    {
        public RemotingApi()
        {
            actions = new Dictionary<string, DirectMethod[]>();
            type = "remoting";
        }
        
        /// <summary>
        /// Object literal defining the server side actions and methods
        /// </summary>
        public IDictionary<string, DirectMethod[]> actions { get; set; }
        
        /// <summary>
        /// The amount of time in milliseconds to wait before sending a batched request. Defaults to: 10
        /// </summary>          
        [DefaultValue(10)]
        public int enableBuffer { get; set; }

        /// <summary>
        /// The unique id of the provider
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Number of times to re-attempt delivery on failure of a call. Defaults to: 1
        /// </summary>
        [DefaultValue(1)]
        public int maxRetries { get; set; }

        /// <summary>
        /// Namespace for the Remoting Provider (defaults to Ext.global).
        /// </summary>
        public string @namespace { get; set; }

        /// <summary>
        /// The timeout to use for each request.
        /// </summary>
        public int? timeout { get; set; }

        /// <summary>
        /// The url to connect to the Ext.direct.Manager server-side router
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// Neccecary to set the "remoting" value, without it does not work
        /// </summary>
        public string type { get; set; }        

        public RemotingApi Map(DirectSettings settings)
        {
            enableBuffer = settings.BufferLength;
            maxRetries = settings.MaxRetries;
            @namespace = settings.Namespace;
            timeout = settings.Timeout;
            url = settings.Url;
            id = settings.Id;
            return this;
        }
    }

    public class DirectMethod
    {
        public string name { get; set; }
        public int len { get; set; }
        //public bool? strict { get; set; }
        //public string[] @params { get; set; }

        public static DirectMethod Create(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            return new DirectMethod
            {
                name = methodInfo.Name,
                len = methodInfo.GetGenericArguments().Length + methodInfo.GetParameters().Length
            };
        }
    }
}