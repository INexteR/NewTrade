namespace Model
{
    /// <summary>Роль.</summary>
    public interface IRole
    {
        /// <summary>Идентификатор.</summary>
        int Id { get; }

        /// <summary>Название.</summary>
        string Name { get; }

        /// <summary>Права, которыми располагает роль.</summary>
        Rights Rights { get; }
    }
}
