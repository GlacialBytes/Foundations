using System;
using System.Globalization;

namespace GlacialBytes.Foundation.Localizations
{
  /// <summary>
  /// Локализованная строка.
  /// </summary>
  public sealed class LocalizedString
  {
    /// <summary>
    /// Параметры локализуемой строки.
    /// </summary>
    private object[] _parameters;

    /// <summary>
    /// Код строки локализации.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="code">Код строки локализации.</param>
    public LocalizedString(string code)
    {
      Code = code;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="code">Код строки локализации.</param>
    /// <param name="parameters">Параметры строки локализации.</param>
    public LocalizedString(string code, params object[] parameters)
    {
      Code = code;
      _parameters = parameters;
    }

    /// <summary>
    /// Оператор преобразования в строку.
    /// </summary>
    /// <param name="lstr">Объект строки локализации.</param>
    /// <returns>Локализованная строка.</returns>
    public static implicit operator string(LocalizedString lstr)
    {
      return lstr.ToString();
    }

    /// <summary>
    /// Оператор преобразования строки в локализованную строку.
    /// </summary>
    /// <param name="str">Строка.</param>
    /// <returns>Локализованная строка.</returns>
    public static implicit operator LocalizedString(string str)
    {
      return new LocalizedString(str);
    }

    /// <summary>
    /// Возвращает строку в соответствии с культурой локализации по умолчанию.
    /// </summary>
    /// <returns>Локализованная строка.</returns>
    public override string ToString()
    {
      if (String.IsNullOrEmpty(Code))
        return String.Empty;
      if (!LocalizationFactory.Instance.TryGetString(Code, out string value))
        return Code;
      if ((_parameters == null || _parameters.Length == 0))
        return value;
      else
        return string.Format(value, _parameters);
    }

    /// <summary>
    /// Возвращает строку в соответствии с заданой культурой локализации.
    /// </summary>
    /// <param name="culture">Культура локализации.</param>
    /// <returns>Локализованная строка.</returns>
    public string ToString(CultureInfo culture)
    {
      if (String.IsNullOrEmpty(Code))
        return String.Empty;
      if (!LocalizationFactory.Instance.TryGetString(Code, culture, out string value))
        return Code;
      if ((_parameters == null || _parameters.Length == 0))
        return value;
      else
        return string.Format(value, _parameters);
    }

    /// <summary>
    /// Возвращает строку в соответствии с заданным языкомлокализации.
    /// </summary>
    /// <param name="language">Язык локализации.</param>
    /// <returns>Локализованная строка.</returns>
    public string ToString(string language)
    {
      if (String.IsNullOrEmpty(Code))
        return String.Empty;
      if (!LocalizationFactory.Instance.TryGetString(Code, language, out string value))
        return Code;
      if ((_parameters == null || _parameters.Length == 0))
        return value;
      else
        return string.Format(value, _parameters);
    }
  }
}
