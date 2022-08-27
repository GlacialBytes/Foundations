using System;
using Microsoft.Extensions.DependencyInjection;

namespace GlacialBytes.Foundation.Dependencies
{
  /// <summary>
  /// Активатор сервисов.
  /// </summary>
  public static class ServiceActivator
  {
    /// <summary>Провайдер сервисов.</summary>
    internal static IServiceProvider _serviceProvider = null;

    /// <summary>
    /// Конфигурирует активатор.
    /// </summary>
    /// <param name="serviceProvider">Провайдер сервисов.</param>
    public static void Configure(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Создаёт область видимости сервисов.
    /// </summary>
    /// <param name="serviceProvider">Провайдер сервисов.</param>
    /// <returns>Интерфейс области видимости.</returns>
    public static IServiceScope GetScope(IServiceProvider serviceProvider = null)
    {
      var provider = serviceProvider ?? _serviceProvider;
      return provider?
          .GetRequiredService<IServiceScopeFactory>()
          .CreateScope();
    }
  }
}
