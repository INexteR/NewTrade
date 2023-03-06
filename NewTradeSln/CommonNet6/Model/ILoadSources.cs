using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>Поставщик источников.</summary>
    public interface ILoadSources
    {
        bool IsSourcesLoaded { get; }

        /// <summary>Такое событие говорит нам, что список производителей не предназначен
        /// для изменения в потребителях Модели и может меняться в Бизнес Логике Модели
        /// только целиком весь разом.</summary>
        event EventHandler SourcesLoadedChanged;

        IReadOnlyList<IUnit> GetUnits();

        /// <summary>Возвращает всех производителей в индексированном списке
        /// несвязанном с Бизнес (Доменной) логикой.</summary>
        /// <returns>Индексированная коллекция (лист, массив и др.) всех производителей.</returns>
        IReadOnlyList<IManufacturer> GetManufacturers();

        IReadOnlyList<ISupplier> GetSuppliers();

        IReadOnlyList<ICategory> GetCategories();

        /// <summary>Возвращает все товары в индексированном списке
        /// несвязанном с Бизнес (Доменной) логикой.</summary>
        /// <returns>Индексированная коллекция (лист, массив и др.) всех товаров.</returns>
        IReadOnlyCollection<IProduct> GetProducts();

    }
}
