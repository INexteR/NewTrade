﻿using Model;
using System.Collections.ObjectModel;

namespace ViewModels
{
    /// <summary>Этот интерфейс кроме списка продуктов,
    /// должен ещё содержать списки связанных с ним сущностей,
    /// чтобы выбирать из них. Они задаются в базовых интерфейсах. 
    /// Для производителей и ролей у нас есть интерфейсы.
    /// Для остальных нужно добавить.</summary>
    public interface IProductsViewModel : IManufacturersViewModel
    {
        string Name { get; }

        // TODO: С точки зрения источника нам не важен тим коллекции.
        // В реализации это может массив, список, ObservableCollection, BindingLis и др.
        // Важная для View функциональность - это только последжоавтельность товаров.
        IEnumerable<IProduct> Products { get; }

        /// <summary>Команда добавления товара.</summary>
        RelayCommand<IProduct> AddProduct { get; }

        /// <summary>Команда удаления товара.</summary>
        RelayCommand<IProduct> RemoveProduct { get; }

        /// <summary>Команда редактирования товара.</summary>
        RelayCommand<IProduct> ChangeProduct { get; }
    }

}
