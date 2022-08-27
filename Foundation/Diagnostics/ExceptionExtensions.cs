using System;
using GlacialBytes.Foundation.Dependencies;

namespace GlacialBytes.Foundation.Diagnostics
{
  /// <summary>
  /// Методы расширения класса исключений.
  /// </summary>
  public static class ExceptionExtensions
  {
    /// <summary>
    /// Обрабатывает исключение используя доступный в контейнере зависимостей обработчик.
    /// </summary>
    /// <param name="ex">Исключение.</param>
    /// <returns>true - если исключение было обработано, иначе false.</returns>
    public static bool Handle(this Exception ex)
    {
      var handler = UnityContainer.Instance.Resolve<IExceptionHandler>();
      if (handler != null)
        handler.Handle(ex);
      return false;
    }
  }
}
