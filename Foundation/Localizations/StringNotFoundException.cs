using System;

namespace GlacialBytes.Foundation.Localizations
{
  /// <summary>
  /// Строка локализации не найдена.
  /// </summary>
  public class StringNotFoundException : Exception
  {
    /// <summary>
    /// Код строки локализации.
    /// </summary>
    public string StringCode { get; private set; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="stringCode">Код строки локализации.</param>
    public StringNotFoundException(string stringCode)
      : base($"Localized string with code '{stringCode}' is not found.")
    {
      StringCode = stringCode;
    }
  }
}
