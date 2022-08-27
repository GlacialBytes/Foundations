using System;
using System.Data.Common;

namespace GlacialBytes.Foundation.Web
{
  /// <summary>
  /// Предоставляет базовый класс для строго типизированных построителей строк подключения к веб-сервисам.
  /// </summary>
  public class WebServiceConnectionStringBuilder : DbConnectionStringBuilder
  {
    /// <summary>
    /// Имя сервиса.
    /// </summary>
    public string ServiceName
    {
      get { return this["Name"]?.ToString(); }
      set { this["Name"] = value; }
    }

    /// <summary>
    /// Имя хоста.
    /// </summary>
    public string Host
    {
      get { return ContainsKey("Host") ? this["Host"]?.ToString() : null; }
      set { this["Host"] = value; }
    }

    /// <summary>
    /// Номер порта.
    /// </summary>
    public int Port
    {
        get { return ContainsKey("Port") ? Convert.ToInt32(this["Port"]?.ToString()) : UseSsl ? 443 : 80; }
        set { this["Port"] = value.ToString(); }
    }

    /// <summary>
    /// Признак использования SSL.
    /// </summary>
    public bool UseSsl
    {
        get { return ContainsKey("UseSsl") ? Convert.ToBoolean(this["UseSsl"]?.ToString()) : false; }
        set { this["UseSsl"] = value.ToString(); }
    }

    /// <summary>
    /// Путь в адресе.
    /// </summary>
    public string Path
    {
      get { return ContainsKey("Path") ? this["Path"]?.ToString() : null; }
      set { this["Path"] = value; }
    }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public string UserId
    {
      get { return this["User ID"]?.ToString(); }
      set { this["User ID"] = value; }
    }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public string Password
    {
      get { return this["Password"]?.ToString(); }
      set { this["Password"] = value; }
    }

    /// <summary>
    /// Адрес сервиса.
    /// </summary>
    public Uri Address
    {
      get { return new UriBuilder(UseSsl ? "https" : "http", Host, Port, Path).Uri; }
    }
  }
}
