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

        IEnumerable<IUnit> GetUnits();

        /// <summary>Возвращает всех производителей в индексированном списке
        /// несвязанном с Бизнес (Доменной) логикой.</summary>
        /// <returns>Индексированная коллекция (лист, массив и др.) всех производителей.</returns>
        IEnumerable<IManufacturer> GetManufacturers();

        IEnumerable<ISupplier> GetSuppliers();

        IEnumerable<ICategory> GetCategories();

        /// <summary>Возвращает все товары в индексированном списке
        /// несвязанном с Бизнес (Доменной) логикой.</summary>
        /// <returns>Индексированная коллекция (лист, массив и др.) всех товаров.</returns>
        IEnumerable<IProduct> GetProducts();

    }
}
