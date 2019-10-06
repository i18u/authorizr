using System;
using System.Linq;
using System.Reflection;

namespace i18u.Authorizr.Tests
{
    internal static class ReflectionHelper
    {
        public static MethodInfo GetPrivateMethod(this object entity, string methodName)
        {
            var type = entity.GetType();
            var methods = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

            Console.WriteLine($"Retrieving method {methodName}");

            return methods;
        }
    }
}