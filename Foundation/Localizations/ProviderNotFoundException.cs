using System;

namespace GlacialBytes.Foundation.Localizations
{
  /// <summary>
  /// Провайдер локализаций не найден.
  /// </summary>
  public class ProviderNotFoundException : Exception
  {
    /// <summary>
    /// Префикс запрашиваемого провайдера.
    /// </summary>
    public char ProviderPrefix { get; private set; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="providerPrefix">Префикс запрашиваемого провайдера.</param>
    public ProviderNotFoundException(char providerPrefix)
      : base($"Localization provider with prefix '{providerPrefix}' is not found.")
    {
      ProviderPrefix = providerPrefix;
    }
  }
}
