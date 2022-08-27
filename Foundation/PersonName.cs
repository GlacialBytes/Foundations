using System;
using System.Globalization;

namespace GlacialBytes.Foundation
{
  /// <summary>
  /// Имя персоны.
  /// </summary>
  public class PersonName
  {
    /// <summary>
    /// Имя собственное.
    /// </summary>
    public string GivenName { get; private set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string Surname { get; private set; }

    /// <summary>
    /// Отчество или среднее имя.
    /// </summary>
    public string Patronym { get; private set; }

    /// <summary>
    /// Возвращает полное имя пользователя.
    /// </summary>
    /// <param name="nativeCulture">Родная культура персоны.</param>
    /// <returns>Строка полного имени.</returns>
    public string GetFullName(CultureInfo nativeCulture = null)
    {
      if (String.IsNullOrWhiteSpace(GivenName))
        throw new ArgumentException("Empty givenName value is not allowed");

      if ("en".Equals(nativeCulture?.TwoLetterISOLanguageName))
      {
        return String.IsNullOrWhiteSpace(Patronym) ?
          String.IsNullOrWhiteSpace(Surname) ? GivenName : $"{GivenName} {Surname}" :
          String.IsNullOrWhiteSpace(Surname) ? $"{GivenName} {Patronym}" : $"{GivenName} {Patronym} {Surname}";
      }
      else
      {
        return String.IsNullOrWhiteSpace(Patronym) ?
          String.IsNullOrWhiteSpace(Surname) ? GivenName : $"{Surname} {GivenName}" :
          String.IsNullOrWhiteSpace(Surname) ? $"{GivenName} {Patronym}" : $"{Surname} {GivenName} {Patronym}";
      }
    }

    /// <summary>
    /// Возвращает короткое имя пользователя.
    /// </summary>
    /// <param name="nativeCulture">Родная культура персоны.</param>
    /// <returns>Строка короткого имени.</returns>
    public string GetShortName(CultureInfo nativeCulture = null)
    {
      if (String.IsNullOrWhiteSpace(GivenName))
        throw new ArgumentException("Empty givenName value is not allowed");

      string initials = GetInitials();
      if ("en".Equals(nativeCulture?.TwoLetterISOLanguageName))
      {
        return String.IsNullOrWhiteSpace(Surname) ? initials : $"{initials} {Surname}";
      }
      else
      {
        return String.IsNullOrWhiteSpace(Surname) ? initials : $"{Surname} {initials}";
      }
    }

    /// <summary>
    /// Возвращает инициалы.
    /// </summary>
    /// <returns>Строка инициалов.</returns>
    public string GetInitials()
    {
      if (String.IsNullOrWhiteSpace(GivenName))
        throw new ArgumentException("Empty givenName value is not allowed");

      return String.IsNullOrEmpty(Patronym) ?
        $"{GivenName[0]}." :
        Patronym.EndsWith('.') ?
        $"{GivenName[0]}.{Patronym}" : $"{GivenName[0]}.{Patronym[0]}.";
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="givenName">Имя собственное.</param>
    /// <param name="surname">Фамилия.</param>
    /// <param name="patronym">Отчество или среднее имя.</param>
    public PersonName(string givenName, string surname, string patronym)
    {
      GivenName = givenName;
      Surname = surname;
      Patronym = patronym;
    }
  }
}
