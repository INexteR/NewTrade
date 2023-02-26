﻿using System.Collections.Generic;

namespace Model
{
    /// <summary>Целевая модель.</summary>
    public interface IShop : IAuthorization
    {
        /// <summary>Наименование магазина.</summary>
        string Name { get; }


        // Чтобы упростить пока сделаем просто производный интерфейс.
        ///// <summary>Авторизация - необходимая компонента данной предметной области.</summary>
        //IAuthorization Authorization { get; }

        IEnumerable<IProduct> GetProducts();

        IEnumerable<IManufacturer> GetManufacturers();
    }
}