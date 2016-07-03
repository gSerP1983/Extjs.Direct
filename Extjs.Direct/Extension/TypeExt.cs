using System;

namespace Extjs.Direct.Extension
{
    public static class TypeExt
    {
        public static bool IsSimpleType(this Type type)
        {
            type = type.GetNullableUnderlyingType();
            return type.IsPrimitive
                   || type == typeof (string) || type == typeof (decimal) || type == typeof (Guid)
                   || type == typeof (DateTime) || type == typeof (DateTimeOffset) || type == typeof (TimeSpan);
        }

        public static Type GetNullableUnderlyingType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return Nullable.GetUnderlyingType(type);

            return type;
        }
    }
}