using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;

namespace GlacialBytes.Foundation.Localizations
{
  /// <summary>
  /// Провайедр строк локализаций из ресурсов приложения.
  /// </summary>
  public class ResourceLocalizationProvider : ILocalizationProvider
  {
    /// <summary>Менеджеры ресурсов для разных языков.</summary>
    private readonly Dictionary<string, ResourceManager> _managers;

    /// <summary>Список поддерживаемых языков.</summary>
    private readonly List<string> _supportedLanguages;

    /// <summary>
    /// Язык по умолчанию.
    /// </summary>
    public string DefaultLanguage { get; set; }

    /// <summary>
    /// Префикс провайдера по умочанию.
    /// </summary>
    public const char DefaultPrefix = '%';

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ResourceLocalizationProvider()
    {
      _managers = new Dictionary<string, ResourceManager>();
      _supportedLanguages = new List<string>();
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
      if (culture == null) culture = Thread.CurrentThread.CurrentCulture;

      string[] groupAndName = code.Split('.');
      if (groupAndName.Length == 2)
      {
        if (_managers.ContainsKey(groupAndName[0]))
        {
          var manager = _managers[groupAndName[0]];
          try
          {
            value = manager.GetString(groupAndName[1], culture);
          } catch (Exception) {
            value = default(string);
            return false;
          }

          if (!String.IsNullOrEmpty(value))
            return true;
        }
      }
      value = default;
      return false;
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.GetString(string, CultureInfo)"/>
    /// </summary>
    public string GetString(string code, CultureInfo culture)
    {
      string result;
      string[] GroupAndName = code.Split('.');

      if (culture == null) culture = Thread.CurrentThread.CurrentCulture;
      if (GroupAndName.Length != 2)
        throw new StringNotFoundException(code);
      if (!_managers.ContainsKey(GroupAndName[0]))
        throw new StringNotFoundException(code);

      var manager = _managers[GroupAndName[0]];
      try
      {
        result = manager.GetString(GroupAndName[1], culture);
      } catch (Exception) {
        result = null;
      }
      if (String.IsNullOrEmpty(result))
        throw new StringNotFoundException(code);
      return result;
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.TryGetString(string, string, out string)"/>
    /// </summary>
    public bool TryGetString(string code, string language, out string value)
    {
      return TryGetString(code, new CultureInfo(language), out value);
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.GetString(string, string)"/>
    /// </summary>
    public string GetString(string code, string language)
    {
      return GetString(code, new CultureInfo(language));
    }

    /// <summary>
    /// <see cref="NpoComputer.Foundation.Localizations.ILocalizationProvider" />
    /// </summary>
    /// <exception cref="NotSupportedException">Метод не поддерживается данным провайдером.</exception>
    public IEnumerable<(string key, string value)> GetStrings(CultureInfo Culture)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.AddString(string, string, string)"/>
    /// </summary>
    /// <exception cref="NotSupportedException">Метод не поддерживается данным провайдером.</exception>
    public void AddString(string code, string value, string language)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// <see cref="ILocalizationProvider.GetSupportedLanguages"/>
    /// </summary>
    public IEnumerable<string> GetSupportedLanguages()
    {
      return _supportedLanguages.Distinct();
    }
    #endregion

    /// <summary>
    /// Добавляет менеджер ресурсов со строками локализации к провайдеру.
    /// </summary>
    /// <param name="GroupName">Имя группы строк.</param>
    /// <param name="ResourceManager">Менеджер ресурсов.</param>
    public void AddResourceManager(string GroupName, ResourceManager ResourceManager)
    {
      _managers[GroupName] = ResourceManager;

      var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
      foreach (CultureInfo culture in cultures)
      {
        try
        {
          var resourceSet = ResourceManager.GetResourceSet(culture, true, false);
          if (resourceSet != null)
            _supportedLanguages.Add(culture.TwoLetterISOLanguageName.ToLower());
        } catch (CultureNotFoundException) {
          //ignore
        }
      }
    }
  }
}
