using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;

namespace LazyStarter
{
    public static class LinqExtentions
    {
        public static bool Any(this IDictionary collection)
        {
            return collection.Count > 0;
        }


        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> addItems)
        {
            foreach (T v in addItems)
                collection.Add(v);
        }
        

        public static bool Contains(this IEnumerable collection, object item)
        {
            foreach (var o in collection)
                if (o == item)
                    return true;

            return false;
        }
        

        public static void ForEach<T>(this ICollection collection, Action<T> action)
        {
            foreach (T v in collection)
                action(v);
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T v in collection)
                action(v);
        }


        public static T Last<T>(this Collection<T> collection)
        {
            int count = collection.Count;
            if (count > 0)
                return collection[count - 1];
            return default(T);
        }
        

        public static int IndexOf<T>(this T[] array, T value) where T : class
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i] == value)
                    return i;

            return -1;
        }

        public static IEnumerable<T> OfType<T>(this Array array) where T : class
        {
            int lenght = array.Length;
            List<T> list = new List<T>(lenght);
            for (int i = 0; i < lenght; i++)
            {
                T value = array.GetValue(i) as T;
                if (value.GetType() == typeof(T))
                    list.Add(value);
            }

            return list;
        }

        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> collection)
        {
            return collection.OrderBy(i => i);
        }

        public static V GetValueOrDefault<V>(this WeakReference wr)
        {
            if (wr == null || !wr.IsAlive)
                return default(V);
            return (V)wr.Target;
        }
    }
}
