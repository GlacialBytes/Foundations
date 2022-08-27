using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GlacialBytes.Foundation.Data
{
  /// <summary>
  /// Интерфейс репозитория.
  /// </summary>
  /// <typeparam name="TKey">Тип ключа.</typeparam>
  /// <typeparam name="TItem">Тип хранимого объекта.</typeparam>
  public interface IRepository<TKey, TItem>
    where TItem : DataModel<TKey>
  {
    /// <summary>
    /// Имя репозитория.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Проверяет существуют ли в хранилище данные, подходящие под условие.
    /// </summary>
    /// <param name="where">Выражение для выборки объектов.</param>
    /// <returns>true - если подходящие данные обнаружены, иначе false.</returns>
    bool Any(Expression<Func<TItem, bool>> where);

    /// <summary>
    /// Асинхронно проверяет существуют ли в хранилище данные, подходящие под условие.
    /// </summary>
    /// <param name="where">Выражение для выборки объектов.</param>
    /// <returns>true - если подходящие данные обнаружены, иначе false.</returns>
    Task<bool> AnyAsync(Expression<Func<TItem, bool>> where);

    /// <summary>
    /// Асинхронно проверяет существуют ли в хранилище данные, подходящие под условие.
    /// </summary>
    /// <param name="where">Выражение для выборки объектов.</param>
    /// <param name="cancellationToken">Токен завершения операции.</param>
    /// <returns>true - если подходящие данные обнаружены, иначе false.</returns>
    Task<bool> AnyAsync(Expression<Func<TItem, bool>> where, CancellationToken cancellationToken);

    /// <summary>
    /// Создаёт новый объект в хранилище.
    /// </summary>
    /// <param name="item">Данные нового объекта.</param>
    void Create(TItem item);

    /// <summary>
    /// Асинхронно создаёт новый объект в хранилище.
    /// </summary>
    /// <param name="item">Данные нового объекта.</param>
    /// <returns>Объект задачи с данными нового объекта.</returns>
    Task<TItem> CreateAsync(TItem item);

    /// <summary>
    /// Асинхронно создаёт новый объект в хранилище.
    /// </summary>
    /// <param name="item">Данные нового объекта.</param>
    /// <param name="cancellationToken">Токен завершения операции.</param>
    /// <returns>Объект задачи с данными нового объекта.</returns>
    Task<TItem> CreateAsync(TItem item, CancellationToken cancellationToken);

    /// <summary>
    /// Создаёт множество объектов в хранилище.
    /// </summary>
    /// <param name="items">Данные новых объектов.</param>
    void CreateMany(IEnumerable<TItem> items);

    /// <summary>
    /// Асинхронно создаёт множество объектов в хранилище
    /// </summary>
    /// <param name="items">Данные новых объектов.</param>
    /// <returns>Объект задачи.</returns>
    Task CreateManyAsync(IEnumerable<TItem> items);

    /// <summary>
    /// Асинхронно создаёт множество объектов в хранилище
    /// </summary>
    /// <param name="items">Данные новых объектов.</param>
    /// <param name="cancellationToken">Токен завершения операции.</param>
    /// <returns>Объект задачи.</returns>
    Task CreateManyAsync(IEnumerable<TItem> items, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет указанные объекты.
    /// </summary>
    /// <param name="key">Ключ удаляемого объекта.</param>
    void Delete(TKey key);

    /// <summary>
    /// Асинхронно удаляет указанные объекты.
    /// </summary>
    /// <param name="key">Ключ удаляемого объекта.</param>
    /// <returns>Объект задачи.</returns>
    Task DeleteAsync(TKey key);

    /// <summary>
    /// Асинхронно удаляет указанные объекты.
    /// </summary>
    /// <param name="key">Ключ удаляемого объекта.</param>
    /// <param name="cancellationToken">Токен завершения операции.</param>
    /// <returns>Объект задачи.</returns>
    Task DeleteAsync(TKey key, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет указанные объекты.
    /// </summary>
    /// <param name="where">Выражение для выборки объектов.</param>
    /// <returns>Количество удалённых объектов.</returns>
    int DeleteMany(Expression<Func<TItem, bool>> where);

    /// <summary>
    /// Асинхронно удаляет указанные объекты.
    /// </summary>
    /// <param name="where">Выражение для выборки объектов.</param>
    /// <returns>Объект задачи с количеством удалённых объектов.</returns>
    Task<int> DeleteManyAsync(Expression<Func<TItem, bool>> where);

    /// <summary>
    /// Асинхронно удаляет указанные объекты.
    /// </summary>
    /// <param name="where">Выражение для выборки объектов.</param>
    /// <param name="cancellationToken">Токен завершения операции.</param>
    /// <returns>Объект задачи с количеством удалённых объектов.</returns>
    Task<int> DeleteManyAsync(Expression<Func<TItem, bool>> where, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает объект по ключу.
    /// </summary>
    /// <param name="key">Ключ запрашиваемого объекта.</param>
    /// <returns>Объект репозитория.</returns>
    TItem Get(TKey key);

    /// <summary>
    /// Асинхронно возвращает объект по ключу.
    /// </summary>
    /// <param name="key">Ключ запрашиваемого объекта.</param>
    /// <returns>Объект задачи с полученным объектом.</returns>
    Task<TItem> GetAsync(TKey key);

    /// <summary>
    /// Асинхронно возвращает объект по ключу.
    /// </summary>
    /// <param name="key">Ключ запрашиваемого объекта.</param>
    /// <param name="cancellationToken">Токен завершения операции.</param>
    /// <returns>Объект задачи с полученным объектом.</returns>
    Task<TItem> GetAsync(TKey key, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает все объекты.
    /// </summary>
    /// <returns>Список всех объектов.</returns>
    IEnumerable<TItem> GetAll();

    /// <summary>
    /// Асинхронно возвращает все объекты.
    /// </summary>
    /// <returns>Объект задачи со списком полученных объектов.</returns>
    Task<IEnumerable<TItem>> GetAllAsync();

    /// <summary>
    /// Асинхронно возвращает все объекты.
    /// </summary>
    /// <param name="cancellationToken">Токен завершения операции.</param>
    /// <returns>Объект задачи со списком полученных объектов.</returns>
    Task<IEnumerable<TItem>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает объекты по выражению выборки.
    /// </summary>
    /// <param name="where">Выражение для выборки объектов.</param>
    /// <returns>Список объектов.</returns>
    IEnumerable<TItem> GetMany(Expression<Func<TItem, bool>> where);

    /// <summary>
    /// Асинхронно возвращает объекты по выражению выборки.
    /// </summary>
    /// <param name="where">Выражение для выборки объектов.</param>
    /// <returns>Объект задачи со списокм полученных объектов.</returns>
    Task<IEnumerable<TItem>> GetManyAsync(Expression<Func<TItem, bool>> where);

    /// <summary>
    /// Асинхронно возвращает объекты по выражению выборки.
    /// </summary>
    /// <param name="where">Выражение для выборки объектов.</param>
    /// <param name="cancellationToken">Токен завершения операции.</param>
    /// <returns>Объект задачи со списокм полученных объектов.</returns>
    Task<IEnumerable<TItem>> GetManyAsync(Expression<Func<TItem, bool>> where, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает один объект по выражению выборки.
    /// </summary>
    /// <param name="where">Выражение для выборки объектов.</param>
    /// <returns>Выбираемый объект.</returns>
    TItem GetOne(Expression<Func<TItem, bool>> where);

    /// <summary>
    /// Асинхронно возвращает один объект по выражению выборки.
    /// </summary>
    /// <param name="where">Выражение для выборки объектов.</param>
    /// <returns>Объект задачи с полученным объектом.</returns>
    Task<TItem> GetOneAsync(Expression<Func<TItem, bool>> where);

    /// <summary>
    /// Асинхронно возвращает один объект по выражению выборки.
    /// </summary>
    /// <param name="where">Выражение для выборки объектов.</param>
    /// <param name="cancellationToken">Токен завершения операции.</param>
    /// <returns>Объект задачи с полученным объектом.</returns>
    Task<TItem> GetOneAsync(Expression<Func<TItem, bool>> where, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает интерфейс выборки объектов.
    /// </summary>
    /// <returns>Интерфейс запроса.</returns>
    IQueryable<TItem> GetQuery();

    /// <summary>
    /// Асинхронно возвращает интерфейс выборки объектов.
    /// </summary>
    /// <returns>Объект задачи с запросом.</returns>
    Task<IQueryable<TItem>> GetQueryAsync();

    /// <summary>
    /// Асинхронно возвращает интерфейс выборки объектов.
    /// </summary>
    /// <param name="cancellationToken">Токен завершения операции.</param>
    /// <returns>Объект задачи с запросом.</returns>
    Task<IQueryable<TItem>> GetQueryAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет данные указанного объекта.
    /// </summary>
    /// <param name="item">Новые данные объекта.</param>
    /// <param name="key">Идентификатор обновляемого объекта.</param>
    void Update(TItem item, TKey key);

    /// <summary>
    /// Асинхронно обновляет данные указанного объекта.
    /// </summary>
    /// <param name="item">Новые данные объекта.</param>
    /// <param name="key">Идентификатор обновляемого объекта.</param>
    /// <returns>Объект задачи.</returns>
    Task UpdateAsync(TItem item, TKey key);

    /// <summary>
    /// Асинхронно обновляет данные указанного объекта.
    /// </summary>
    /// <param name="item">Новые данные объекта.</param>
    /// <param name="key">Идентификатор обновляемого объекта.</param>
    /// <param name="cancellationToken">Токен завершения операции.</param>
    /// <returns>Объект задачи.</returns>
    Task UpdateAsync(TItem item, TKey key, CancellationToken cancellationToken);

    /// <summary>
    /// Полнотекствоый поиск объектов.
    /// </summary>
    /// <typeparam name="TProperty">Тип свойства.</typeparam>
    /// <param name="propertyExpression">Выражение, возвращающее свойство модели, по которому производится поиск.</param>
    /// <param name="query">Строка для поиска.</param>
    /// <returns>Список найденных объектов.</returns>
    IEnumerable<TItem> FullTextSearch<TProperty>(Expression<Func<TItem, TProperty>> propertyExpression, string query);

    /// <summary>
    /// Полнотекствоый поиск объектов с дополнительным условием выборки.
    /// </summary>
    /// <typeparam name="TProperty">Тип свойства.</typeparam>
    /// <param name="propertyExpression">Выражение, возвращающее свойство модели, по которому производится поиск.</param>
    /// <param name="query">Строка для поиска.</param>
    /// <param name="where">Выражение для выборки объектов.</param>
    /// <returns>Список найденных объектов.</returns>
    IEnumerable<TItem> FullTextSearch<TProperty>(Expression<Func<TItem, TProperty>> propertyExpression, string query, Expression<Func<TItem, bool>> where);
  }
}
