

namespace Interfaces
{
    /// <summary>
    /// Возможные комбинации прав
    /// </summary>
    [Flags]
    public enum Rights
    {
        Viewing = 0, //Просмотр
        Adding = 1, //Добавление
        Deleting = 2, //Удаление
        Editing = 4 //Редактирование
    }
}
