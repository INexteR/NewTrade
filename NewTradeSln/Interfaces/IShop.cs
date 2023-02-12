
namespace Interfaces
{
    /// <summary>
    /// Целевая модель
    /// </summary>
    public interface IShop
    {
        /// <summary>
        /// Наименование магазина
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Авторизация - необходимая компонента данной предметной области
        /// </summary>
        IAuthorization Authorization { get; }
    }
}
