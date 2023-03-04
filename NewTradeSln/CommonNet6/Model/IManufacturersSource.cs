using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>Поставщик (источник) производителей.</summary>
    public interface IManufacturersSource
    {
        /// <summary>Такое событие говорит нам, что список производителей не предназначен
        /// для изменения в потребителях Модели, но может меняться в Бизнес Логике Модели.</summary>
        //event NotifyListChangedEventHandler<IManufacturer> ManufacturerChanged;

        /// <summary>Такое событие говорит нам, что список производителей не предназначен
        /// для изменения в потребителях Модели и может меняться в Бизнес Логике Модели
        /// только целиком весь разом.</summary>
        event EventHandler ManufacturersChanged;

        /// <summary>Возвращает всех производителей в индексированном списке
        /// несвязанном с Бизнес (Доменной) логикой.</summary>
        /// <returns>Индексированная коллекция (лист, массив и др.) всех производителей.</returns>
        IReadOnlyList<IManufacturer> GetManufacturers();
    }
}
