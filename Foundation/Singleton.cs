using System;
using System.Runtime.CompilerServices;

namespace GlacialBytes.Foundation
{
  /// <summary>
  /// Класс одиночка.
  /// </summary>
  /// <typeparam name="T">Тип класса наследника.</typeparam>
  public class Singleton<T> where T : Singleton<T>
  {
    private static readonly object _syncRoot = RuntimeHelpers.GetObjectValue(new object());
    private static T _instance;

    public static T Instance
    {
      get
      {
        if (_instance == null)
        {
          lock (_syncRoot)
          {
            if (_instance == null)
              _instance = (T)Activator.CreateInstance(typeof(T), true);
          }
        }
        return _instance;
      }
      set
      {
        if (value != null)
          throw new ArgumentException("Null value expected.");
        lock (_syncRoot)
        {
          _instance = default(T);
        }
      }
    }

    static Singleton()
    {
    }

    protected Singleton()
    {
      _instance = (T)this;
    }
  }
}
