using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace GlacialBytes.Foundation.Localizations
{
  /// <summary>
  /// Фабрика локализаций.
  /// </summary>
  public class LocalizationFactory : Singleton<LocalizationFactory>
  {
    /// <summary>Коллекция провайдеров строк локализаций.</summary>
    private Dictionary<char, ILocalizationProvider> _providers;

    /// <summary>
    /// Язык по умолчанию.
    /// </summary>
    public string DefaultLanguage { get; private set; }

    /// <summary>
    /// Список комплементарных языковых пар.
    /// </summary>
    public IEnumerable<LanguagePair> LanguagePairs { get; private set; }

    /// <summary>
    /// Провайдер по умолчанию.
    /// </summary>
    public ILocalizationProvider DefaultProvider { get; set; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    private LocalizationFactory()
    {
      // Создаём стандартные провайдеры
      _providers = new Dictionary<char, ILocalizationProvider>
      {
        [ResourceLocalizationProvider.DefaultPrefix] = new ResourceLocalizationProvider(),
        [XmlLocalizationProvider.DefaultPrefix] = new XmlLocalizationProvider(),
        [DefaultLocalizationProvider.DefaultPrefix] = new DefaultLocalizationProvider(),
      };

      // Заполняем комплементарные пары языков
      var languagePairs = new List<LanguagePair>(3)
      {
        new LanguagePair("ru", "en"),
        new LanguagePair("en", "ru"),
        new LanguagePair("uk", "ru"),
      };
      LanguagePairs = languagePairs;
      DefaultLanguage = "en";
    }

    /// <summary>
    /// Возвращает строку локализации по коду.
    /// </summary>
    /// <param name="code">Код строки локализации.</param>
    /// <param name="value">Локализованная строка в соответствии с культурой.</param>
    /// <returns>true - строка локализации найдена, иначе false.</returns>
    public bool TryGetString(string code, out string value)
    {
      return TryGetString(code, (string)null, out value);
    }

    /// <summary>
    /// Возвращает строку локализации по коду.
    /// </summary>
    /// <param name="code">Код строки локализации.</param>
    /// <param name="culture">Культура локализации.</param>
    /// <param name="value">Локализованная строка в соответствии с культурой.</param>
    /// <returns>true - строка локализации найдена, иначе false.</returns>
    public bool TryGetString(string code, CultureInfo culture, out string value)
    {
      return TryGetString(code, culture?.TwoLetterISOLanguageName, out value);
    }

    /// <summary>
    /// Возвращает строку локализации по её коду.
    /// </summary>
    /// <param name="code">Код строки локализации.</param>
    /// <param name="culture">Культура локализации.</param>
    /// <returns>Локализованная строка.</returns>
    public string GetString(string code, CultureInfo culture = null)
    {
      if (!TryGetString(code, culture?.TwoLetterISOLanguageName, out string value))
        throw new StringNotFoundException(code);
      return value;
    }

    /// <summary>
    /// Возвращает строку локализации по коду.
    /// </summary>
    /// <param name="code">Код строки локализации.</param>
    /// <param name="language">Язык локализации.</param>
    /// <param name="value">Локализованная строка в соответствии с культурой.</param>
    /// <returns>true - строка локализации найдена, иначе false.</returns>
    public bool TryGetString(string code, string language, out string value)
    {
      char providerPrefix = code[0];
      bool result;

      if (providerPrefix == '!')
      {
        value = code.Substring(1);
        return true;
      }

      if (language == null)
        language = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

      string pureCode = code;
      if (!_providers.TryGetValue(providerPrefix, out ILocalizationProvider provider))
      {
        provider = DefaultProvider;
      } else {
        pureCode = code.Substring(1);
      }

      if (provider != null)
      {
        try
        {
          result = provider.TryGetString(pureCode, language, out value);
          if (!result)
          {
            // если строка не найдена, пробуем получить её с комплементарным языком
            language = GetComplementaryLanguage(language);
            if (!String.IsNullOrEmpty(language))
              result = provider.TryGetString(pureCode, language, out value);
          }
        }
        catch (UnsupportedCultureException)
        {
          // Если язык не поддерживается провайдером, то вернём исходную строку
          value = pureCode;
          result = false;
        }
      } else {
        value = default;
        result = false;
      }

      return result;
    }

    /// <summary>
    /// Возвращает строку локализации по её коду.
    /// </summary>
    /// <param name="code">Код строки локализации.</param>
    /// <param name="language">Язык локализации.</param>
    /// <returns>Локализованная строка.</returns>
    public string GetString(string code, string language)
    {
      if (!TryGetString(code, language, out string value))
        throw new StringNotFoundException(code);
      return value;
    }

    /// <summary>
    /// Возвращает список локализованных значений строк для всех поддерживаемых языков.
    /// </summary>
    /// <param name="code">Код строки локализации.</param>
    /// <param name="values">Список локализованных значений.</param>
    /// <returns>Результат успешности завершения метода.</returns>
    public bool TryGetStringValues(string code, out LocalizedStringValue[] values)
    {
      char providerPrefix = code[0];
      bool result = true;

      string pureCode = code;
      if (!_providers.TryGetValue(providerPrefix, out ILocalizationProvider provider))
      {
        provider = DefaultProvider;
      } else {
        pureCode = code.Substring(1);
      }

      if (provider == null)
        throw new ProviderNotFoundException(providerPrefix);

      var languages = provider.GetSupportedLanguages();
      values = new LocalizedStringValue[languages.Count()];

      int languageIndex = 0;
      foreach (var language in languages)
      {
        try
        {
          string value = provider.GetString(pureCode, language);
          values[languageIndex] = new LocalizedStringValue()
          {
            Language = language,
            Value = value,
          };
        } catch (StringNotFoundException) {
          values[languageIndex] = new LocalizedStringValue()
          {
            Language = language,
            Value = pureCode,
          };
          result = false;
        }

        ++languageIndex;
      }
      return result;
    }

    /// <summary>
    /// Возвращает список локализованных значений строк для всех поддерживаемых языков.
    /// </summary>
    /// <param name="code">Код строки локализации.</param>
    /// <returns>Список локализованных значений.</returns>
    public LocalizedStringValue[] GetStringValues(string code)
    {
      if (!TryGetStringValues(code, out LocalizedStringValue[] values))
        return Enumerable.Empty<LocalizedStringValue>().ToArray();
      return values;
    }

    /// <summary>
    /// Добавляет указанные локализованные значения для строки.
    /// </summary>
    /// <param name="code">Код строки локализации.</param>
    /// <param name="values">Список локализованных значений.</param>
    public void AddStringValues(string code, LocalizedStringValue[] values)
    {
      char providerPrefix = code[0];
      if (!_providers.TryGetValue(providerPrefix, out ILocalizationProvider provider))
      {
        if (DefaultProvider != null)
          provider = DefaultProvider;
        else
          throw new StringNotFoundException(code);
      }

      code = code.Substring(1);
      foreach (var value in values)
      {
        provider.AddString(code, value.Value, value.Language);
      }
    }

    /// <summary>
    /// Регистрирует нового провайдера строк локализации.
    /// </summary>
    /// <param name="provider">Провайдер локализаций.</param>
    public void RegisterProvider(ILocalizationProvider provider)
    {
      _providers[provider.StringPrefix] = provider;
    }

    /// <summary>
    /// Возвращает провайдера строк локализации по префиксу из списка зарегистрированных.
    /// </summary>
    /// <param name="prefix">Префик строк для данного провайдера.</param>
    /// <returns>Интерфейс провайдера строк локализаций.</returns>
    public ILocalizationProvider GetProvider(char prefix)
    {
      if (_providers.ContainsKey(prefix))
        return _providers[prefix];
      throw new ProviderNotFoundException(prefix);
    }

    /// <summary>
    /// Возвращает комплементарный язык для указанного.
    /// </summary>
    /// <param name="language">Основной язык.</param>
    /// <returns>Комплементарный язык, либо null, если такого языка нет.</returns>
    public string GetComplementaryLanguage(string language)
    {
      var complementaryPair = LanguagePairs.FirstOrDefault((P) => P.MainLanguage.Equals(language));

      string complementaryLanguage = (complementaryPair == null) ? DefaultLanguage : complementaryPair.ComplementaryLanguage;
      return complementaryLanguage.Equals(language) ? null : complementaryLanguage;
    }
  }
}
