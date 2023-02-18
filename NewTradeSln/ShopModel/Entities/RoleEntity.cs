using Interfaces;

namespace ShopModel.Entities
{
    internal class RoleEntity : IRole
    {
        // Явная реализация интерфейса.
        int IRole.Id => Id;
        string IRole.Name => Name;
        Rights IRole.Rights => Rights;

        // Свойства, которые доступны только внутри сброки.
        internal int Id { get; set; }
        internal string Name { get; set; } = string.Empty;
        internal Rights Rights { get; set; }
    }
}
