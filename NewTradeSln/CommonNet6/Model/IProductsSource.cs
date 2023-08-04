using CommonNet6.Collection;
using System.Collections.Generic;

namespace Model
{
    /// <summary>Поставщик (источник) товаров.</summary>
    public interface IProductsSource : ILoadSources
    {
        /// <summary>Извещает об изменение в списке товаров: полной его смене,
        /// удалении-добавлении товара, обновлении товара.</summary>
        event NotifyListChangedEventHandler<IProduct> ProductChanged;

        /// <summary>Добавление товара.</summary>
        /// <param name="product">Добавляемый товар.</param>
        void Add(IProduct product);

        /// <summary>Удаление товара.</summary>
        /// <param name="product">Удаляемый товар.</param>
        void Remove(IProduct product);

        /// <summary>Изменение товара.</summary>
        /// <param name="product">Изменяемый товар.</param>
        void Update(IProduct product);

        bool CanAddAndUpdate { get; }
        bool CanRemove { get; }
    }
}
