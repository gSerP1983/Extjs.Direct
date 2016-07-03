using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Extjs.Direct.Util
{
    public static class TypeResolver
    {
        private static readonly ConcurrentBag<Assembly> AssemblyCache = new ConcurrentBag<Assembly>();
        private static readonly ConcurrentDictionary<string, Type> TypeCache = new ConcurrentDictionary<string, Type>();

        public static void Initialize(params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies.Where(assembly => !AssemblyCache.Contains(assembly)))
                AssemblyCache.Add(assembly);
        }

        public static Type Get(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                return null;

            var result = TypeCache.GetOrAdd(typeName, x =>
            {
                var res = AssemblyCache
                    .SelectMany(assembly => assembly.GetTypes())
                    .FirstOrDefault(type => type.FullName == typeName);

                // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
                if (res == null)
                {
                    res = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(assembly => assembly.GetTypes())
                        .FirstOrDefault(type => type.FullName == typeName);
                }

                return res;
            });

            if (result != null)
            {
                if (!AssemblyCache.Contains(result.Assembly))
                    AssemblyCache.Add(result.Assembly);
            }

            return result;
        }
    }
}