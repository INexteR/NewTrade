using System;
using System.Collections.Specialized;

namespace CommonNet6.Collection
{
    public class NotifyCollectionChangedAction<T> : EventArgs
    {
        public NotifyCollectionChangedAction Action { get; }

        public T? OldItem { get; }
        public T? NewItem { get; }

        protected NotifyCollectionChangedAction(NotifyCollectionChangedAction action, T? oldItem, T? newItem)
        {
            Action = action;
            OldItem = oldItem;
            NewItem = newItem;
        }

        public static NotifyCollectionChangedAction<T> Add(T? newItem)
        {
            return new(NotifyCollectionChangedAction.Add, default, newItem);
        }
        public static NotifyCollectionChangedAction<T> Replace(T? oldItem, T? newItem)
        {
            return new(NotifyCollectionChangedAction.Replace, oldItem, newItem);
        }
        public static NotifyCollectionChangedAction<T> Remove(T? oldItem)
        {
            return new(NotifyCollectionChangedAction.Remove, oldItem, default);
        }
        public static NotifyCollectionChangedAction<T> Reset()
        {
            return new(NotifyCollectionChangedAction.Reset, default, default);
        }
    }
}
