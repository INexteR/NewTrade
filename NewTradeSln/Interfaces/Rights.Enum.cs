﻿namespace Interfaces
{
    /// <summary>Возможные комбинации прав.</summary>
    [Flags]
    public enum Rights
    {
        /// <summary>Просмотр.</summary>
        Viewing = 0, 
        /// <summary>Добавление.</summary>
        Adding = 1, 
        /// <summary>Удаление.</summary>
        Deleting = 2, 
        /// <summary>Редактирование.</summary>
        Editing = 4 
    }
}
