using CommonNet6.Collection;
using System.Collections.Generic;

namespace Model
{
    /// <summary>Поставщик (источник) товаров.</summary>
    public interface IProductsSource
    {
        /// <summary>Извещает об изменение в списке товаров: полной его смене,
        /// удалении-добавлении товара, обновлении товара.</summary>
        event NotifyListChangedEventHandler<IProduct> ProductChanged;

        /// <summary>Возвращает все товары в индексированном списке
        /// несвязанном с Бизнес (Доменной) логикой.</summary>
        /// <returns>Индексированная коллекция (лист, массив и др.) всех товаров.</returns>
        IReadOnlyCollection<IProduct> GetProducts();

        /// <summary>Добавление товара.</summary>
        /// <param name="product">Добавляемый товар.</param>
        void Add(IProduct product);

        /// <summary>Удаление товара.</summary>
        /// <param name="product">Удаляемый товар.</param>
        void Remove(IProduct product);

        /// <summary>Изменение товара.</summary>
        /// <param name="product">Изменяемый товар.</param>
        void Change(IProduct product);

        /// <summary>Переинициализация списка товаров из Хранилища Данных.</summary>
        void Refresh();
    }
}
