using System;

namespace GlacialBytes.Foundation.Diagnostics
{
  /// <summary>
  /// Интерфейс обработчика исключений.
  /// </summary>
  public interface IExceptionHandler
  {
    /// <summary>
    /// Обрабатывает исключение.
    /// </summary>
    /// <param name="exception">Исключение.</param>
    /// <returns>Признак обработки исключения.</returns>
    bool Handle(Exception exception);

    /// <summary>
    /// Обрабатывает исключение.
    /// </summary>
    /// <param name="exception">Исключение.</param>
    /// <param name="severity">Предопределённая важность исключения.</param>
    /// <returns>Признак обработки исключения.</returns>
    bool Handle(Exception exception, ExceptionSeverity severity);
  }
}
