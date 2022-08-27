using System;

namespace GlacialBytes.Foundation.Localizations
{
  /// <summary>
  /// Исключение отсутствия локализации для заданой культуры.
  /// </summary>
  public class UnsupportedCultureException : Exception
  {
    /// <summary>
    /// Культура.
    /// </summary>
    public string CultureName { get; private set; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="cultureName">Культура.</param>
    public UnsupportedCultureException(string cultureName)
      : base($"Provider culture '{cultureName}' is not supported.")
    {
      CultureName = cultureName;
    }
  }
}
