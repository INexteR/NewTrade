using System;
using System.Collections.Generic;

namespace CommonNet6.Collection
{
    public static class CollectionHelper
    {
        public static int FirstRemove<T>(this IList<T> list, Predicate<T> predicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    return i;
                }
            }
            return -1;
        }
        public static int ReplaceOrAdd<T>(this IList<T> list, T newItem, Equality<T> equality)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (equality(list[i], newItem))
                {
                    list[i] = newItem;
                    return i;
                }
            }
            list.Add(newItem);
            return list.Count - 1;
        }

        public static void Reset<T>(this IList<T> list, IEnumerable<T> values)
        {
            var enumerator = values.GetEnumerator();
            int index = 0;
            for (; index < list.Count && enumerator.MoveNext(); index++)
            {
                list[index] = enumerator.Current;
            }
            if (index < list.Count)
            {
                for (int last = list.Count - 1; index <= last; last--)
                    list.RemoveAt(last);
            }
            else
            {
                while (enumerator.MoveNext())
                    list.Add(enumerator.Current);
            }
        }
    }
    public delegate bool Equality<T>(T? x, T? y);

}
