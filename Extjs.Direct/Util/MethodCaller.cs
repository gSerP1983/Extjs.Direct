using System;
using System.Linq;
using System.Reflection;
using Extjs.Direct.Extension;
using Newtonsoft.Json;

namespace Extjs.Direct.Util
{
    public static class MethodCaller
    {
        public static object GetResult(object obj, MethodInfo methodInfo, object[] parameters, object[] genericArguments = null)
        {            
            ThrowExceptionIfParametersAreNotValid(methodInfo, parameters);            
            parameters = PrepareParameters(methodInfo, parameters);
            methodInfo = MakeGenericMethod(methodInfo, genericArguments);

            if (methodInfo.IsStatic)
                return MethodUtil.CallStatic(methodInfo, parameters);

            if (obj == null)
                throw new ApplicationException(string.Format("For type '{0}' failed to create the object.", methodInfo.DeclaringType));

            return MethodUtil.Call(obj, methodInfo, parameters);
        }

        private static void ThrowExceptionIfParametersAreNotValid(MethodInfo methodInfo, object[] parameters)
        {
            if (methodInfo.GetParameters().Length != parameters.Length)
                throw new ApplicationException(string.Format("For the method '{0}. {1}' given the wrong number of parameters {2}.", 
                    methodInfo.DeclaringType, methodInfo.Name, parameters.Length));
        }

        private static MethodInfo MakeGenericMethod(MethodInfo methodInfo, object[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
                return methodInfo;

            var length = methodInfo.GetGenericArguments().Length;
            if (length == 0)
                return methodInfo;

            var types = parameters.Take(length)
                .Select(ResolveType)
                .ToArray();

            return methodInfo.MakeGenericMethod(types);            
        }

        private static object[] PrepareParameters(MethodInfo methodInfo, object[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
                return parameters;

            return methodInfo.GetParameters()
                .Select((x, i) => Convert(parameters.ElementAt(i), x.ParameterType))
                .ToArray();
        }

        private static object Convert(object value, Type type)
        {            
            if (!type.IsSimpleType())
            {
                if (type == typeof (Type))
                {
                    ThrowCanNotConvertNullToType(value, type);
                    value = TypeResolver.Get(value.ToString());
                }
                else if (value.ToString().IsJson())
                {
                    ThrowCanNotConvertNullToType(value, type);
                    value = JsonConvert.DeserializeObject(value.ToString(), type);
                }
            }
            else
                value = SafeConverter.Convert(value, type);

            return value;
        }

        private static void ThrowCanNotConvertNullToType(object value, Type type)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                throw new ApplicationException(string.Format("Can not convert '{0}' to type {1}.", value ?? "NULL", type));
        }

        private static Type ResolveType(object typeObj)
        {
            if (typeObj == null)
                throw new ArgumentNullException("typeObj");

            var typeString = typeObj.ToString();
            if (string.IsNullOrWhiteSpace(typeString))
                throw new ArgumentNullException("typeObj");            

            var result = TypeResolver.Get(typeString);
            if (result == null)
                throw new ApplicationException(string.Format("Type '{0}' was not found.", typeString));

            return result;
        } 
    }
}