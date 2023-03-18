namespace Model
{
    /// <summary>Возможные права.</summary>
    public enum Rights
    {
        /// <summary>Просмотр - гость</summary>
        Viewing,
        /// <summary>Добавление - клиент</summary>
        Adding,
        /// <summary>Редактирование - менеджер</summary>
        Updating,
        /// <summary>Все права - администратор</summary>
        Full
    }
}
