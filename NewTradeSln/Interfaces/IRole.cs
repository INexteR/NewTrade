
namespace Interfaces
{
    /// <summary>
    /// Роль
    /// </summary>
    public interface IRole
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Права, которыми располагает роль
        /// </summary>
        Rights Rights { get; set; }
    }
}