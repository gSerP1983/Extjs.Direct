using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Extjs.Direct.Domain;
using Extjs.Direct.Extension;
using Extjs.Direct.Util;
using Newtonsoft.Json;

namespace Extjs.Direct
{
    public class Executor
    {        
        public static Executor Instance { get; private set; }
        public static void Initialize(IEnumerable<Type> rpcTypes, Func<Type, object, object> objectFactory, DirectSettings settings, Action<Exception> logError)
        {
            Instance = new Executor(rpcTypes, objectFactory, settings, logError);
        }

        private static RemotingApi _metaCache;

        private readonly IEnumerable<Type> _rpcTypes;
        private readonly DirectSettings _settings;
        private readonly Func<Type, object, object> _objectFactory;
        private readonly Action<Exception> _logError;

        private Executor(IEnumerable<Type> rpcTypes, Func<Type, object, object> objectFactory, DirectSettings settings, Action<Exception> logError)
        {
            _rpcTypes = rpcTypes;
            _settings = settings;
            _logError = logError;
            _objectFactory = objectFactory;
        }

        public RemotingApi Meta()
        {
            return _metaCache ?? (_metaCache = MetaInner());
        }

        private RemotingApi MetaInner()
        {
            var result = new RemotingApi().Map(_settings);

            foreach (var type in _rpcTypes)
            {
                var methods = GetAllDeclaredMethods(type)
                    .Select(DirectMethod.Create).ToArray();

                result.actions.Add(GetDottedFullName(type), methods);
            }

            return result;
        }

        private static IEnumerable<MethodInfo> GetAllDeclaredMethods(Type type)
        {
            return type
                .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public)
                .Where(x => x.GetBaseDefinition().DeclaringType != typeof(object));
        }

        private static string GetDottedFullName(Type type)
        {
            // for nested classes, example: UnitTest.It.Extjs.ExtjsDirectFacadeTest+Util
            return type.FullName.Replace("+", ".");
        }

        public object Execute(string requestString, object context)
        {
            if (string.IsNullOrWhiteSpace(requestString))
                return new ResponseMeta { success = false, msg = "The empty request." };

            if (!requestString.IsJsonArray())
                requestString = "[" + requestString + "]";

            // it is necessary to transform the object into an array, error in json.net
            requestString = requestString.Replace("\"data\":{},", "\"data\":[],");

            Request[] requests;
            try
            {
                requests = JsonConvert.DeserializeObject<Request[]>(requestString);
            }
            catch (Exception x)
            {
                LogError(x);
                return new ResponseMeta { success = false, msg = "Rpc-request had wrong format." };
            }

            return requests.Select(x => Instance.Execute(x, context)).ToArray();
        }

        private Response Execute(Request request, object context)
        {
            return ExecuteInner(request, context);
        }

        private Response ExecuteInner(Request request, object context)
        {
            request = PrepareRequest(request);
            try
            {
                object service = null;
                var mi = GetMethodInfo(request);
                if (!mi.IsStatic)
                    service = _objectFactory(GetRpcTypeByName(request.action, _rpcTypes), context);                

                return CreateResponse(request, mi, service);
            }
            catch (Exception x)
            {
                LogError(x);
                return CreateErrorResponse(request, x);
            }
        }

        private Request PrepareRequest(Request request)
        {
            if (request.data == null)
                request.data = new object[] {};

            return request;
        }

        private MethodInfo GetMethodInfo(Request request)
        {
            var type = GetRpcTypeByName(request.action, _rpcTypes);
            if (type == null)
                throw new ApplicationException(string.Format("Type '{0}' was not found.", request.action));

            var methodInfos = MethodUtil.Find(type, request.method);
            if (methodInfos == null || !methodInfos.Any())
                throw new ApplicationException(string.Format("Method '{0}' in type '{1}' was not found.", request.method, type));

            var methodInfo = GetBestMethodInfo(methodInfos, request.data.Length);
            if (methodInfo == null)
                throw new ApplicationException(string.Format("Method '{0}' with the required number of parameters is not found in type {1}.", 
                    request.method, type));

            return methodInfo;
        }

        private static Response CreateResponse(Request request, MethodInfo methodInfo, object obj)
        {
            var response = Response.Map(request);

            var length = methodInfo.GetGenericArguments().Length;
            var parameters = request.data.Skip(length).ToArray();
            var genericArguments = request.data.Take(length).ToArray();
            response.result = GetResult(obj, methodInfo, parameters, genericArguments);

            return response;
        }

        private static object GetResult(object obj, MethodInfo methodInfo, object[] parameters, object[] genericArguments)
        {
            return MethodCaller.GetResult(obj, methodInfo, parameters, genericArguments);
        }

        private static MethodInfo GetBestMethodInfo(IEnumerable<MethodInfo> methods, int parametersCount)
        {
            return methods.FirstOrDefault(
                x => x.GetGenericArguments().Length + x.GetParameters().Length == parametersCount);
        }

        private static Type GetRpcTypeByName(string name, IEnumerable<Type> rpcTypes)
        {
            if (rpcTypes == null || string.IsNullOrWhiteSpace(name))
                return null;

            return rpcTypes.FirstOrDefault(x => GetDottedFullName(x) == name);
        }

        private static Response CreateErrorResponse(Request request, Exception ex)
        {
            var response = Response.Map(request);
            response.meta.success = false;
            response.meta.msg = ex.Message;
            response.meta.fullMsg = ex.ToString();
            return response;
        }

        private void LogError(Exception x)
        {
            if (_logError != null)
                _logError(x);
        }
    }
}