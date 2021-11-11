using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Utils.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> input)
        {
            return input == null
                   || !input.Any();
        }

        public static bool DoesNotContain<T>(this IEnumerable<T> stuff, T thing)
        {
            return !stuff.Contains(thing);
        }

        public static bool DoesNotContain(this IEnumerable<string> stuff, string thing)
        {
            return !stuff.Contains(thing, StringComparer.CurrentCultureIgnoreCase);
        }

        public static bool IsContainedIn<T>(this T item, params T[] items)
        {
            return item.IsContainedIn(items.AsEnumerable());
        }

        public static bool IsContainedIn<T>(this T item, IEnumerable<T> items)
        {
            return items.Contains(item);
        }

        public static string StringJoin<T>(this IEnumerable<T> input, string separator)
        {
            return string.Join(separator, input);
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static IEnumerable<T> ToEnumerable<T>(this T @object)
        {
            yield return @object;
        }


        public static async Task<IEnumerable<T>> WhenAll<T>(this IEnumerable<Task<T>> tasks)
        {
            var items = await Task.WhenAll(tasks);
            return items.Where(i => i != null);
        }
    }


    public static class EnumExtensions
    {
        public static IEnumerable<TEnum> Values<TEnum>()
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var enumType = typeof(TEnum);

            if (!enumType.IsEnum)
                throw new ArgumentException();

            return Enum.GetValues(enumType).Cast<TEnum>();
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
        }

        public static T Parse<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}