using System;
using System.Collections.Specialized;

namespace CommonNet6.Collection
{
    public class NotifyListChangedEventArgs<T> : EventArgs
    {
        public NotifyCollectionChangedAction Action { get; }

        public T? OldItem { get; }
        public T? NewItem { get; }

        public int OldIndex { get; }
        public int NewIndex { get; }

        protected NotifyListChangedEventArgs(NotifyCollectionChangedAction action, T? oldItem, T? newItem, int oldIndex, int newIndex)
        {
            Action = action;
            OldItem = oldItem;
            NewItem = newItem;
            OldIndex = oldIndex;
            NewIndex = newIndex;
        }

        public static NotifyListChangedEventArgs<T> Add(T? newItem, int newIndex)
        {
            return new(NotifyCollectionChangedAction.Add, default, newItem, -1, newIndex);
        }
        public static NotifyListChangedEventArgs<T> Reset()
        {
            return new(NotifyCollectionChangedAction.Reset, default, default, -1, -1);
        }
        public static NotifyListChangedEventArgs<T> Replace(T? oldItem, T? newItem, int index)
        {
            return new(NotifyCollectionChangedAction.Replace, oldItem, newItem, index, index);
        }
        public static NotifyListChangedEventArgs<T> Remove(T? oldItem, int oldIndex)
        {
            return new(NotifyCollectionChangedAction.Remove, oldItem, default, oldIndex, -1);
        }
        public static NotifyListChangedEventArgs<T> Move(T? item, int oldIndex, int newIndex)
        {
            return new(NotifyCollectionChangedAction.Move, item, item, oldIndex, newIndex);
        }
    }
}
