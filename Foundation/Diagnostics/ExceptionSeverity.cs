namespace GlacialBytes.Foundation.Diagnostics
{
  /// <summary>
  /// Важность исключения.
  /// </summary>
  public enum ExceptionSeverity
  {
    /// <summary>
    /// Неопределена.
    /// </summary>
    Undefined = -1,

    /// <summary>
    /// Критическая ошибка.
    /// </summary>
    CriticalError = 0,

    /// <summary>
    /// Ошибка.
    /// </summary>
    Error = 1,

    /// <summary>
    /// Предупреждение.
    /// </summary>
    Warning = 2,

    /// <summary>
    /// Отладочное исключение.
    /// </summary>
    Debug = 3,

    /// <summary>
    /// Исключение можно игнорировать.
    /// </summary>
    Ignore = 4
  }
}
