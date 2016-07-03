using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Extjs.Direct.Util
{
    public static class MethodUtil
    {
        static readonly ConcurrentDictionary<Tuple<Type, string>, MethodInfo[]> MethodInfoCache = new ConcurrentDictionary<Tuple<Type, string>, MethodInfo[]>();

        public static MethodInfo[] Find(Type type, string methodName)
        {
            var key = Tuple.Create(type, methodName);
            return MethodInfoCache.GetOrAdd(key, x =>
            {
                return x.Item1.GetMethods()
                    .Where(m => m.Name == x.Item2)
                    .ToArray();
            });
        }

        public static MethodInfo[] Find<T>(string methodName)
        {
            return Find(typeof(T), methodName);
        }

        public static MethodInfo[] Find(object obj, string methodName)
        {
            return Find(obj.GetType(), methodName);
        }

        public static object Call(object obj, MethodInfo methodInfo, object[] parameters = null)
        {
            return methodInfo.Invoke(obj, parameters);
        }

        public static object CallStatic(MethodInfo methodInfo, object[] parameters = null)
        {
            return Call(null, methodInfo, parameters);
        }

    }
}