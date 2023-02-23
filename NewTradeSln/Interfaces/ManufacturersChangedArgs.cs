
namespace Interfaces
{
    /// <summary>Аргумент события изменения источника произвоидителей.</summary>
    public class ManufacturersChangedArgs : EventArgs
    {
        /// <summary>Причина события.</summary>
        public CollectionChangedAction Action { get; }

        /// <summary>Индекс задействованного элемента.
        /// Для <see cref="CollectionChangedAction.Resset"/> равен -1.</summary>
        public int Index { get; }

        /// <summary>Удаляемый элемент.
        /// Задаётся для <see cref="CollectionChangedAction.Remove"/> и <see cref="CollectionChangedAction.Replace"/>.</summary>
        public IManufacturer? OldManufacturer { get; }

        /// <summary>Добавляемый элемент.
        /// Задаётся для <see cref="CollectionChangedAction.Add"/> и <see cref="CollectionChangedAction.Replace"/>.</summary>
        public IManufacturer? NewManufacturer { get; }

        /// <summary>Создаёт аргумент добавления (вставки) элемента.</summary>
        /// <param name="index">Индекс вставки.</param>
        /// <param name="newManufacturer">Добавляемый элемент.</param>
        /// <returns>Экземпляр <see cref="ManufacturersChangedArgs"/>.</returns>
        public static ManufacturersChangedArgs Add(int index, IManufacturer newManufacturer)
            => new(CollectionChangedAction.Add, index, null, newManufacturer);

        /// <summary>Создаёт аргумент удаления элемента.</summary>
        /// <param name="index">Индекс удаления.</param>
        /// <param name="newManufacturer">Удаляемый элемент.</param>
        /// <returns>Экземпляр <see cref="ManufacturersChangedArgs"/>.</returns>
        public static ManufacturersChangedArgs Remove(int index, IManufacturer oldManufacturer)
            => new(CollectionChangedAction.Remove, index, oldManufacturer, null);

        /// <summary>Создаёт аргумент замены элемента.</summary>
        /// <param name="index">Индекс замены.</param>
        /// <param name="oldManufacturer">Старый элемент.</param>
        /// <param name="newManufacturer">Новый элемент.</param>
        /// <returns>Экземпляр <see cref="ManufacturersChangedArgs"/>.</returns>
        public static ManufacturersChangedArgs Replace(int index, IManufacturer oldManufacturer, IManufacturer newManufacturer)
            => new(CollectionChangedAction.Replace, index, oldManufacturer, newManufacturer);

        /// <summary>Создаёт аргумент переинициализации коллекции.</summary>
        /// <returns>Экземпляр <see cref="ManufacturersChangedArgs"/>.</returns>
        public static ManufacturersChangedArgs Reset()
            => new(CollectionChangedAction.Resset, -1, null, null);

        private ManufacturersChangedArgs(CollectionChangedAction action, int index, IManufacturer? oldManufacturer, IManufacturer? newManufacturer)
        {
            Action = action;
            Index = index;
            OldManufacturer = oldManufacturer;
            NewManufacturer = newManufacturer;
        }
    }
}
