using System;

namespace GlacialBytes.Foundation.Domain
{
  /// <summary>
  /// Интерфейс команды.
  /// </summary>
  /// <typeparam name="TKey">Тип ключей в хранилище.</typeparam>
  /// <typeparam name="TArgs">Тип аргументов команды.</typeparam>
  /// <typeparam name="TResult">Тип результата команды.</typeparam>
  public interface ICommand<TKey, in TArgs, out TResult> : IUnitOfWork<TKey>, IDisposable
  {
    /// <summary>
    /// Выполняет команду.
    /// </summary>
    /// <param name="args">Аргументы.</param>
    /// <returns>Результат выполнения.</returns>
    TResult Execute(TArgs args);
  }

  /// <summary>
  /// Интерфейс команды, выполняемой без резульатат.
  /// </summary>
  /// <typeparam name="TKey">Тип ключей в хранилище.</typeparam>
  /// <typeparam name="TArgs">Тип аргументов команды.</typeparam>
  public interface ICommand<TKey, in TArgs> : IUnitOfWork<TKey>, IDisposable
  {
    /// <summary>
    /// Выполняет команду.
    /// </summary>
    /// <param name="args">Аргументы.</param>
    void Execute(TArgs args);
  }
}
