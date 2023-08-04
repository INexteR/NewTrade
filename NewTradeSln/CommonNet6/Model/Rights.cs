namespace Model
{
    /// <summary>Возможные права.</summary>
    public enum Rights
    {
        /// <summary>Просмотр - гость</summary>
        Viewing,
        /// <summary>Добавление и редактирование - клиент</summary>
        AddingAndUpdating,
        /// <summary>Все права - администратор</summary>
        Full
    }
}
