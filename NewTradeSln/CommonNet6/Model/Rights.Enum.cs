using System;

namespace Model
{
    /// <summary>Возможные комбинации прав.</summary>
    [Flags]
    public enum Rights
    {
        /// <summary>Просмотр - гость</summary>
        Viewing = 0, 
        /// <summary>Добавление - клиент</summary>
        Adding = 1, 
        /// <summary>Редактирование - менеджер</summary>
        Editing = 2,
        /// <summary>Все права - администратор</summary>
        Full = Adding | Editing,
    }
}
