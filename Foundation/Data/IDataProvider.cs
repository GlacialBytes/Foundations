using System;
using System.Threading;
using System.Threading.Tasks;

namespace GlacialBytes.Foundation.Data
{
  /// <summary>
  /// Интерфейс провайдера данных.
  /// </summary>
  /// <typeparamref name = "TKey" > Тип ключа в хранилище.</typeparamref>
  public interface IDataProvider<TKey> : IDisposable
  {
    /// <summary>
    /// Возвращает репозиторий по типу сущности.
    /// </summary>
    /// <typeparam name="TItem">Тип хранимой сущности.</typeparam>
    /// <returns>Интерфейс репозитория.</returns>
    IRepository<TKey, TItem> GetRepository<TItem>() where TItem : DataModel<TKey>;

    /// <summary>
    /// Сохраняет изменения объектов в хранилище.
    /// </summary>
    /// <returns>Количество сохранённых объектов.</returns>
    int SaveChanges();

    /// <summary>
    /// Асинхронно сохраняет изменения объектов в хранилище.
    /// </summary>
    /// <returns>Объект Task , представляющий асинхронную операцию, содержащую количество сохранённых объектов.</returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Асинхронно сохраняет изменения объектов в хранилище.
    /// </summary>
    /// <param name="cancellationToken">Токен завершения операции.</param>
    /// <returns>Объект Task , представляющий асинхронную операцию, содержащую количество сохранённых объектов.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
  }
}
