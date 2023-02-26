namespace Model
{
    /// <summary>Состояние авторизации.</summary>
    public enum AuthorizationStatus
    {
        /// <summary>Не выполнена.</summary>
        None,
        /// <summary>В процессе обработки.</summary>
        InProcessing,
        /// <summary>Выполнена.</summary>
        Authorized,
        /// <summary>Ошибка.</summary>
        Fail
    }
}
