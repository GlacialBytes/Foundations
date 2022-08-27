namespace GlacialBytes.Foundation.Data
{
  /// <summary>
  /// Модель данных.
  /// </summary>
  /// <typeparam name="TKey">Тип ключа в хранилище.</typeparam>
  public class DataModel<TKey>
  {
    /// <summary>
    /// Идентификатор сущности в хранилище.
    /// </summary>
    public TKey Id { get; set; }
  }
}
