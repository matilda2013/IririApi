using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Helpers
{
    public static class ObjectExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return (source == null || !source.Any());
        }

        public static bool IsNull<T>(this T source)
        {
            return source == null;
        }

        public static bool IsNotNull<T>(this T source)
        {
            return source != null;
        }

        public static bool IsDefault<T>(this T source)
            where T : struct
        {
            return source.Equals(default(T));
        }

        public static bool IsEmpty(this Guid source)
        {
            return source == Guid.Empty;
        }

        public static bool IsAnyOf<T>(this T source, params T[] values)
        {
            if (values.IsNullOrEmpty())
            {
                return false;
            }

            return values.Contains(source);
        }

        public static bool HasOneItem<T>(this IEnumerable<T> source)
        {
            return (source.Count() == 1);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source.IsNullOrEmpty())
            {
                return;
            }

            foreach (var item in source)
            {
                action(item);
            }
        }

        public static bool IsCollection(this Type type)
        {
            if (type == null)
            {
                return false;
            }

            switch (type.Name)
            {
                case "IEnumerable`1":
                case "IList`1":
                case "List`1":
                case "ICollection`1":
                    return true;
                default:
                    return type.IsArray || type.Name.EndsWith("[]");
            }
        }

        public static IList<TResult> SelectIfNotNull<T, TResult>(this IEnumerable<T> source, Func<T, TResult> condition)
        {
            if (source == null)
            {
                return null;
            }

            return source.Select(condition).ToList();
        }

        public static string BreakupText(this string source)
        {
            if (source.IsNullOrEmpty() || source.Length == 1)
            {
                return source;
            }

            var charList = new List<char> { source[0] };
            var num = 0;
            var value = 0;
            var fail = true;

            for (int i = 1; i < source.Length; i++)
            {
                value = i - 1;

                if (num == 1 && char.IsLower(source[value]) && char.IsUpper(source[i]) && fail)
                {
                    charList.Add('/');
                    fail = false;
                }

                else if (char.IsUpper(source[i]))
                {
                    charList.Add(' ');
                    num++;
                }

                charList.Add(source[i]);
            }

            return new string(charList.ToArray());
        }

        public static bool ToBool(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }

            return value.Equals("true", StringComparison.InvariantCultureIgnoreCase);
        }

        public static int ToSafeInt(this string value)
        {
            if (int.TryParse(value, out var intVale))
            {
                return intVale;
            }

            return default(int);
        }

        public static T FromJson<T>(this string source)
        {
            return JsonConvert.DeserializeObject<T>(source);
        }

        public static object FromJson(this string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type);
        }

        public static string ToJson<T>(this T data, bool camelCasing = false)
        {
            if (!camelCasing)
            {
                return JsonConvert.SerializeObject(data);
            }

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(data, settings);
        }

        public static V GetValue<K, V>(this IDictionary<K, V> source, K key, V defaultValue = default(V))
        {
            if (source.IsNullOrEmpty())
            {
                return defaultValue;
            }

            if (source.TryGetValue(key, out var value))
            {
                return value;
            }

            return defaultValue;
        }

        public static int LowerValue(this int value1, int value2)
        {
            return value1 > value2 ? value2 : value1;
        }

        public static int CompareWordsWith(this string word, string compareWith)
        {

            if (compareWith.IsNull() || compareWith.Length < 1 || word.IsNull() || word.Length < 1)
            {
                return 0;
            }
            var wordArray = word.Split(' ');
            var counter = 0;
            for (var i = 0; i < wordArray.Length; i++)
            {
                if (compareWith.IndexOf(wordArray[i], StringComparison.InvariantCultureIgnoreCase) != -1) counter++;
            }


            var percentage = (counter * 100) / wordArray.Length;
            return percentage;
        }
    }
}
