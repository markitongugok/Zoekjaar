using System;
using System.Reflection;

namespace Business.Core.Extensions
{
    public static class TypeExtensions
    {
        public static MethodInfo GetMethodInfo(this Type type, string name, params Type[] parameterTypes)
        {
            return type.GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, parameterTypes, null);
        }
    }
}
