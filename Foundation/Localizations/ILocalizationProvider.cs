using System.Collections.Generic;
using System.Globalization;

namespace GlacialBytes.Foundation.Localizations
{
  /// <summary>
  /// Провайдер локализаций.
  /// </summary>
  public interface ILocalizationProvider
  {
    /// <summary>
    /// Префикс локализуемых строк, обрабатываемых провайдером.
    /// </summary>
    char StringPrefix { get; set; }

    /// <summary>
    /// Возвращает строку локализации по коду.
    /// </summary>
    /// <param name="code">Код строки локализации.</param>
    /// <param name="culture">Культура локализации.</param>
    /// <param name="value">Локализованная строка в соответствии с культурой.</param>
    /// <returns>true - строка локализации найдена, иначе false.</returns>
    bool TryGetString(string code, CultureInfo culture, out string value);

    /// <summary>
    /// Возвращает строку локализации по коду.
    /// </summary>
    /// <param name="Code">Код строки локализации.</param>
    /// <param name="Culture">Культура локализации.</param>
    /// <returns>Локализованная строка в соответствии с культурой.</returns>
    string GetString(string code, CultureInfo culture);

    /// <summary>
    /// Возвращает строку локализации по коду.
    /// </summary>
    /// <param name="code">Код строки локализации.</param>
    /// <param name="language">Язык локализации.</param>
    /// <param name="value">Локализованная строка в соответствии с культурой.</param>
    /// <returns>true - строка локализации найдена, иначе false.</returns>
    bool TryGetString(string code, string language, out string value);

    /// <summary>
    /// Возвращает строку локализации по коду.
    /// </summary>
    /// <param name="Code">Код строки локализации.</param>
    /// <param name="Language">Язык локализации.</param>
    /// <returns>Локализованная строка в соответствии с языком.</returns>
    string GetString(string code, string language);

    /// <summary>
    /// Добавляет новую строку локализации.
    /// </summary>
    /// <param name="Code">Код строки локализации.</param>
    /// <param name="Value">Значение строки для заданного языка.</param>
    /// <param name="Language">Язык локализации.</param>
    void AddString(string code, string value, string language);

    /// <summary>
    /// Возвращает все строки локализации в виде пар ключ-значение.
    /// </summary>
    /// <param name="Culture">Культура локализации.</param>
    /// <returns>Список строк локализаций.</returns>
    IEnumerable<(string key, string value)> GetStrings(CultureInfo culture);

    /// <summary>
    /// Возвращает список всех поддерживаемых языков.
    /// </summary>
    /// <returns>Список поддерживаемых языков.</returns>
    IEnumerable<string> GetSupportedLanguages();
  }
}
