using System;
using System.Collections.Generic;
using System.Globalization;

namespace GlacialBytes.Foundation.Localizations
{
  /// <summary>
  /// Реализация провайдера локализаций по умолчанию.
  /// </summary>
  public class DefaultLocalizationProvider : ILocalizationProvider
  {
    /// <summary>
    /// Префикс провайдера по умочанию.
    /// </summary>
    public const char DefaultPrefix = '!';

    #region ILocalizationProvider

    /// <summary>
    /// <see cref="ILocalizationProvider.StringPrefix"/>
    /// </summary>
    public char StringPrefix
    {
      get { return DefaultPrefix; }
      set { throw new NotSupportedException(); }
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.AddString(string, string, string)"/>
    /// </summary>
    public void AddString(string code, string value, string language)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.GetString(string, CultureInfo)"/>
    /// </summary>
    public string GetString(string code, CultureInfo culture)
    {
      return code;
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.GetString(string, string)"/>
    /// </summary>
    public string GetString(string code, string language)
    {
      return code;
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.GetStrings(CultureInfo)"/>
    /// </summary>
    /// <exception cref="NotSupportedException">Метод не поддерживается реализацией.</exception>
    public IEnumerable<(string key, string value)> GetStrings(CultureInfo culture)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.GetSupportedLanguages"/>
    /// </summary>
    public IEnumerable<string> GetSupportedLanguages()
    {
      return new string[] { "ru", "en" };
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.TryGetString(string, CultureInfo, out string)"/>
    /// </summary>
    /// <returns></returns>
    public bool TryGetString(string code, CultureInfo culture, out string value)
    {
      value = code;
      return true;
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.TryGetString(string, string, out string)"/>
    /// </summary>
    public bool TryGetString(string code, string language, out string value)
    {
      value = code;
      return true;
    }
    #endregion
  }
}
