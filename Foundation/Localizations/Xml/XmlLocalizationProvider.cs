using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Linq;
using System.Globalization;
using System.Threading;

namespace GlacialBytes.Foundation.Localizations
{
  /// <summary>
  /// Провайдер строк локализаций из XML файлов.
  /// </summary>
  public class XmlLocalizationProvider : ILocalizationProvider
  {
    /// <summary>Словари локализаций из XML.</summary>
    private ListDictionary _localizationTables;

    /// <summary>
    /// Язык по умолчанию.
    /// </summary>
    public string DefaultLanguage { get; set; }

    /// <summary>
    /// Префикс провайдера по умочанию.
    /// </summary>
    public const char DefaultPrefix = '#';

    /// <summary>
    /// Конструктор.
    /// </summary>
    public XmlLocalizationProvider()
    {
      _localizationTables = new ListDictionary();
    }

    #region ILocalizationProvider

    /// <summary>
    /// Префикс локализуемых строк, обрабатываемых провайдером.
    /// </summary>
    public virtual char StringPrefix
    {
      get { return _prefix; }
      set { _prefix = value; }
    }
    private char _prefix = DefaultPrefix;

    /// <summary>
    /// <see cref="ILocalizationProvider.TryGetString(string, CultureInfo, out string)"/>
    /// </summary>
    public bool TryGetString(string code, CultureInfo culture, out string value)
    {
      Dictionary<string, string> localizationTable;

      if (culture == null) culture = Thread.CurrentThread.CurrentCulture;
      string language = culture.Name;
      if (!_localizationTables.Contains(language))
      {
        language = culture.TwoLetterISOLanguageName.ToUpper();
        if (!_localizationTables.Contains(language))
        {
          language = DefaultLanguage.ToUpper();
          if (!_localizationTables.Contains(language))
            throw new UnsupportedCultureException(code);
        }
      }

      localizationTable = (Dictionary<string, string>)_localizationTables[language];
      code = code.ToUpper();
      return localizationTable.TryGetValue(code, out value);
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.GetString(string, CultureInfo)"/>
    /// </summary>
    public string GetString(string code, CultureInfo culture)
    {
      if (!TryGetString(code, culture, out string value))
        throw new StringNotFoundException(code);
      return value;
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.GetString(string, string)"/>
    /// </summary>
    public string GetString(string code, string language)
    {
      if (!TryGetString(code, language, out string value))
        throw new StringNotFoundException(code);
      return value;
    }

    /// <summary>
    /// <see cref="NpoComputer.Foundation.Localizations.ILocalizationProvider" />
    /// </summary>
    public bool TryGetString(string code, string language, out string value)
    {
      Dictionary<string, string> localizationTable;
      language = language.ToUpper();
      if (!_localizationTables.Contains(language))
      {
        language = DefaultLanguage.ToUpper();
        if (!_localizationTables.Contains(language))
          throw new UnsupportedCultureException(code);
      }

      localizationTable = (Dictionary<string, string>)_localizationTables[language];
      code = code.ToUpper();
      return localizationTable.TryGetValue(code, out value);
    }

    /// <summary>
    /// <see cref="NpoComputer.Foundation.Localizations.ILocalizationProvider" />
    /// </summary>
    public IEnumerable<(string key, string value)> GetStrings(CultureInfo culture)
    {
      if (culture == null) culture = Thread.CurrentThread.CurrentCulture;
      string language = culture.Name.ToUpper();
      if (!_localizationTables.Contains(language))
      {
        language = culture.TwoLetterISOLanguageName.ToUpper();
        if (!_localizationTables.Contains(language))
          return Enumerable.Empty<(string, string)>();
      }

      var localizationTable = (Dictionary<string, string>)_localizationTables[language];
      if (localizationTable.Count == 0)
        return Enumerable.Empty<(string, string)>();

      var result = new List<(string, string)>(localizationTable.Count);
      foreach (var entry in localizationTable)
      {
        result.Add((StringPrefix + entry.Key, entry.Value));
      }

      return result;
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.AddString(string, string, string)"/>
    /// </summary>
    public void AddString(string code, string value, string language)
    {
      Dictionary<string, string> localizationTable;

      //Получение словаря локализаций
      language = language.ToUpper();
      if (_localizationTables.Contains(language))
      {
        localizationTable = (Dictionary<string, string>)_localizationTables[language];
      } else {
        localizationTable = new Dictionary<string, string>();
        _localizationTables.Add(language, localizationTable);
      }

      code = code.ToUpper();
      localizationTable[code] = value;
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.GetSupportedLanguages"/>
    /// </summary>
    public IEnumerable<string> GetSupportedLanguages()
    {
      var result = new List<string>();
      foreach (var key in _localizationTables.Keys)
      {
        result.Add(key.ToString());
      }
      return result;
    }
    #endregion

    /// <summary>
    /// Загружает локализации из файла.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <param name="language">Язык локализаций.</param>
    public void LoadFromFile(string fileName, string language)
    {
      Dictionary<string, string> localizationTable;
      XDocument Doc = XDocument.Load(fileName);
      IEnumerable<XElement> Elements = Doc.Root.Elements();
      var groupAttribute = Doc.Root.Attribute("group");
      string group = groupAttribute != null ? Doc.Root.Attribute("group").Value : "";

      //Получение словаря локализаций
      language = language.ToUpper();
      if (_localizationTables.Contains(language))
      {
        localizationTable = (Dictionary<string, string>)_localizationTables[language];
      } else {
        localizationTable = new Dictionary<string, string>(Elements.Count());
        _localizationTables.Add(language, localizationTable);
      }

      if (!String.IsNullOrEmpty(group))
        group = group.ToUpper();

      //Заполнение словаря локализаций
      foreach (XElement node in Elements)
      {
        string key = node.Attribute("key").Value.ToUpper();
        if (!String.IsNullOrEmpty(group))
          key = group + "." + key;
        localizationTable[key] = node.Value;
      }
    }
  }
}
