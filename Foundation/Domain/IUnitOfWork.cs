using System;
using System.Threading;
using System.Threading.Tasks;
using GlacialBytes.Foundation.Data;

namespace GlacialBytes.Foundation.Domain
{
  /// <summary>
  /// Интерфейс единицы работы.
  /// </summary>
  /// <typeparam name="TKey">Тип ключа в хранилище.</typeparam>
  public interface IUnitOfWork<TKey> : IDisposable
  {
    /// <summary>
    /// Провайдер данных.
    /// </summary>
    IDataProvider<TKey> DataProvider { get; }

    /// <summary>
    /// Фиксирует выполненную работу.
    /// </summary>
    void Commit();

    /// <summary>
    /// Асинхронно фиксирует выполненную работу.
    /// </summary>
    /// <returns>Объект задачи.</returns>
    Task CommitAsync();

    /// <summary>
    /// Асинхронно фиксирует выполненную работу.
    /// </summary>
    /// <param name="cancellationToken">Токен завершения операции.</param>
    /// <returns>Объект задачи.</returns>
    Task CommitAsync(CancellationToken cancellationToken);
  }
}
