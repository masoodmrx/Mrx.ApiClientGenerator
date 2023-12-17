using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mrx.ApiClientGenerator.Dart.NetFramework.Helpers
{
    public static class StringExtensions
    {
        public static bool IsValidUrl(this string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNotNullOrEmpty(this string str)
        {
            return !str.IsNullOrEmpty();
        }

        public static string StringJoin(this IEnumerable<object> values, string separator)
        {
            return string.Join(separator, values);
        }

        public static string StringJoin(this object[] values, string separator)
        {
            return string.Join(separator, values);
        }

    }
    public static class LinqExtensions
    {
        public static bool IsNull<Entity>(this IEnumerable<Entity> source)
        {
            if (source != null)
            {
                return !source.Any();
            }

            return true;
        }

        public static bool IsNotNull<Entity>(this IEnumerable<Entity> source)
        {
            return source?.Any() ?? false;
        }

        public static bool IsNull(this IEnumerable source)
        {
            if (source != null)
            {
                return !source.Any();
            }

            return true;
        }

        public static bool IsNotNull(this IEnumerable source)
        {
            return source?.Any() ?? false;
        }

        public static bool Any(this IEnumerable source)
        {
            IEnumerator enumerator = source.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                {
                    _ = enumerator.Current;
                    return true;
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            return false;
        }
    }
}
