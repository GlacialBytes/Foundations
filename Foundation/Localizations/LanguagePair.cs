namespace GlacialBytes.Foundation.Localizations
{
  /// <summary>
  /// Языковая пара.
  /// </summary>
  public class LanguagePair
  {
    /// <summary>
    /// Основной язык.
    /// </summary>
    public string MainLanguage { get; set; }

    /// <summary>
    /// Комплементарный язык.
    /// </summary>
    public string ComplementaryLanguage { get; set; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="mainLanguage">Основной язык.</param>
    /// <param name="complementaryLanguage">Комплементарный язык.</param>
    public LanguagePair(string mainLanguage, string complementaryLanguage)
    {
      MainLanguage = mainLanguage;
      ComplementaryLanguage = complementaryLanguage;
    }
  }
}
