using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DMAM.Core.Collections
{
    public static class SortedList
    {
        public static void InsertSorted<T>(this IList<T> list, T item, IComparer<T> comparer)
        {
            var sortedItems = new List<T>(list);
            sortedItems.Add(item);
            sortedItems.Sort(comparer);

            var index = sortedItems.IndexOf(item);
            list.Insert(index, item);
        }

        public static void InitializeSorted<T>(this IList<T> list, IEnumerable<T> items, IComparer<T> comparer)
        {
            if (list.Count != 0)
            {
                list.Clear();
            }

            var sortedItems = new List<T>(items);
            sortedItems.Sort(comparer);

            foreach (var sortedItem in sortedItems)
            {
                list.Add(sortedItem);
            }
        }
    }
}
