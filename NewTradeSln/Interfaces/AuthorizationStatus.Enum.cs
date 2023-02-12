
namespace Interfaces
{
    /// <summary>Состояние авторизации.</summary>
    public enum AuthorizationStatus
    {
        /// <summary>Не выполнена.</summary>
        None,
        /// <summary>Выполнена.</summary>
        Authorized,
        /// <summary>В процессе обработки.</summary>
        InProcessing
    }
}
