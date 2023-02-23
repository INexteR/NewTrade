
namespace Interfaces
{
    /// <summary>Изменение коллекции.</summary>
    public enum CollectionChangedAction
    {
        /// <summary>Добавлен или вставлен элемент.</summary>
        Add,
        /// <summary>Удалён элемент.</summary>
        Remove,
        /// <summary>Заменён элемент.</summary>
        Replace,
        /// <summary>Переинициализация коллекции.</summary>
        Resset
    }
}
