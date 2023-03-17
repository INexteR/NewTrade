namespace Model
{
    /// <summary>Целевая модель.</summary>
    public interface IShop : IAuthorization, ILoadSources, IProductsSource
    {
        /// <summary>Наименование магазина.</summary>
        string Name { get; }


        // Чтобы упростить пока сделаем просто производный интерфейс.
        ///// <summary>Авторизация - необходимая компонента данной предметной области.</summary>
        //IAuthorization Authorization { get; }

        /// <summary>Проверка разрешения прав для текущего пользователя.</summary>
        /// <param name="rights">Проверяемые права.</param>
        /// <returns><see langword="true"/>, если проверяемы права есть у текущего пользователя.</returns>
        bool RoleCheckAccess(Rights rights);

    }
}
