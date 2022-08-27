using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace GlacialBytes.Foundation.Runtime
{
  /// <summary>
  /// Менеджер плагинов.
  /// </summary>
  public static class PluginManager
  {
    /// <summary>
    /// Кэш типов плагинов.
    /// </summary>
    /// <remarks>Ключ - имя системы.</remarks>
    private static readonly Dictionary<string, Type> _pluginTypesCache = new Dictionary<string, Type>();

    /// <summary>
    /// Рабочая директория приложения.
    /// </summary>
    private static string _applicationBaseDirectory = null;

    /// <summary>
    /// Список обрабатываемых имен сборок для разрешения зависимостей.
    /// </summary>
    private static HashSet<string> _handleLibraryNames = new HashSet<string>();

    /// <summary>
    /// Статический конструктор.
    /// </summary>
    static PluginManager()
    {
      _applicationBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
      AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
    }

    /// <summary>
    ///  Регистрация плагина.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <param name="adapterType">Тип адаптера (наименование типа, наименование сборки).</param>
    public static void RegisterPlugin(string name, string adapterType)
    {
      (string typeName, string assemblyName) = SplitFullTypeName(adapterType);

      string assemblyPath = Path.Combine(_applicationBaseDirectory, assemblyName + ".dll");
      var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
      Type pluginType = assembly.GetType(typeName);
      if (pluginType == null)
        return;

      _pluginTypesCache.Add(name, pluginType);

      // TODO: реализовать загрузку зависимостей из deps файла
#if false
      // Добавление зависимостей.
      var assemblyConfigPath = Path.Combine(_applicationBaseDirectory, assemblyName + ".deps.json");
      var assemblyConfig = JObject.Parse(File.ReadAllText(assemblyConfigPath));
      foreach (var dependencies in assemblyConfig.SelectTokens("$....dependencies"))
      {
        foreach (dynamic depend in dependencies)
        {
          _handleLibraryNames.Add(depend.Name);
        }
      }
#endif
    }

    /// <summary>
    /// Разрешение проблем с загрузкой сборок.
    /// </summary>
    /// <returns>Путь к сборке.</returns>
    private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
      (string typeName, _) = SplitFullTypeName(args.Name);
      if (_handleLibraryNames.Contains(typeName))
        return Assembly.LoadFrom(Path.Combine(_applicationBaseDirectory, typeName + ".dll"));

      return null;
    }

    /// <summary>
    /// Получение имени типа и сборки.
    /// </summary>
    /// <param name="fullTypeName">Полное имя сборки.</param>
    /// <returns>Имя типа и сборки</returns>
    private static (string typeName, string assemblyName) SplitFullTypeName(string fullTypeName)
    {
      var splitted = fullTypeName.Split(",");
      var typeName = splitted[0].Trim();
      var assemblyName = splitted[1].Trim();
      return (typeName, assemblyName);
    }

    /// <summary>
    /// Создание экземпляра плагина.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <param name="args">Аргументы.</param>
    /// <returns>Экземпляр плагина.</returns>
    public static object CreateInstance(string name, params object[] args)
    {
      return Activator.CreateInstance(_pluginTypesCache[name], args);
    }

    /// <summary>
    /// Создание экземпляра плагина.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <param name="args">Аргументы.</param>
    /// <typeparam name="T">Тип плагина.</typeparam>
    /// <returns>Экземпляр плагина.</returns>
    public static T CreateInstance<T>(string name, params object[] args)
      where T : class
    {
      return Activator.CreateInstance(_pluginTypesCache[name], args) as T;
    }

    /// <summary>
    /// Возвращает список зарегистрированных типов.
    /// </summary>
    /// <returns>Список типов.</returns>
    public static IEnumerable<Type> GetRegisteredTypes()
    {
      return _pluginTypesCache.Values.ToList();
    }
  }
}
