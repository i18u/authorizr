using System.Collections.Generic;
using i18u.Authorizr.Core.Util;

namespace i18u.Authorizr.Tests.Util
{
    internal static class SlowComparisonExtensions
    {
        public static T GetItemAtOrDefault<T>(this SlowComparer comparer, IEnumerable<T> collection, int index)
        {
            var method = comparer
                .GetPrivateMethod("GetItemAtOrDefault")
                .MakeGenericMethod(typeof(T));

            if (method == null)
            {
                throw new System.Exception();
            }

            return (T)method.Invoke(comparer, new object[] { collection, index });
        }
    }
}