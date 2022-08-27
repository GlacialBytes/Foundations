using System;
using System.Collections.Generic;
using System.Linq;

namespace GlacialBytes.Foundation.Dependencies
{
  /// <summary>
  /// Контейнер зависимостей.
  /// </summary>
  public class UnityContainer : Singleton<UnityContainer>
  {
    #region RegisteredObject
    /// <summary>
    /// Зарегистрированный в контейнере тип.
    /// </summary>
    private class RegisteredType
    {
      private readonly bool _isSinglton;

      public Type ConcreteType { get; private set; }
      public object SingletonInstance { get; private set; }

      /// <summary>
      /// Конструктор.
      /// </summary>
      /// <param name="concreteType">Тип реализации.</param>
      /// <param name="isSingleton">Признак типа-одиночки.</param>
      /// <param name="instance">Экземпляр типа.</param>
      public RegisteredType(Type concreteType, bool isSingleton, object instance)
      {
        _isSinglton = isSingleton;
        ConcreteType = concreteType;
        SingletonInstance = instance;
      }

      /// <summary>
      /// Создаёт экземпляр типа реализации.
      /// </summary>
      /// <param name="args">Аргументы конструктора.</param>
      /// <returns>Экземпляр типа.</returns>
      public object CreateInstance(params object[] args)
      {
        object instance = Activator.CreateInstance(ConcreteType, args);
        if (_isSinglton)
          SingletonInstance = instance; return instance;
      }
    }
    #endregion

    /// <summary>
    /// Коллекция зарегистрированных типов.
    /// </summary>
    private readonly IDictionary<string, RegisteredType> _registeredTypes = new Dictionary<string, RegisteredType>();

    /// <summary>
    /// Регистрирует тип в контейнере.
    /// </summary>
    /// <typeparam name="TType">Регистрируемый тип.</typeparam>
    public void Register<TType>() where TType : class
    {
      Register<TType, TType>(false, null, string.Empty);
    }

    /// <summary>
    /// Регистрирует тип в контейнере.
    /// </summary>
    /// <typeparam name="TType">Регистрируемый тип.</typeparam>
    /// <param name="usageContext">Контекст использования.</param>
    public void Register<TType>(string usageContext) where TType : class
    {
      Register<TType, TType>(false, null, usageContext);
    }

    /// <summary>
    /// Регистрирует тип с конкертной реализацией.
    /// </summary>
    /// <typeparam name="TType">Регистрируемый тип.</typeparam>
    /// <typeparam name="TConcrete">Конкретная реализация регистрируемого типа.</typeparam>
    public void Register<TType, TConcrete>() where TConcrete : class, TType
    {
      Register<TType, TConcrete>(false, null, string.Empty);
    }

    /// <summary>
    /// Регистрирует тип с конкертной реализацией.
    /// </summary>
    /// <typeparam name="TType">Регистрируемый тип.</typeparam>
    /// <typeparam name="TConcrete">Конкретная реализация регистрируемого типа.</typeparam>
    /// <param name="usageContext">Контекст использования.</param>
    public void Register<TType, TConcrete>(string usageContext) where TConcrete : class, TType
    {
      Register<TType, TConcrete>(false, null, usageContext);
    }

    /// <summary>
    /// Регистрирует тип-одиночку.
    /// </summary>
    /// <typeparam name="TType">Регистрируемый тип-одиночка.</typeparam>
    public void RegisterSingleton<TType>() where TType : class
    {
      RegisterSingleton<TType, TType>();
    }

    /// <summary>
    /// Регистрирует тип-одиночку.
    /// </summary>
    /// <typeparam name="TType">Регистрируемый тип-одиночка.</typeparam>
    /// <param name="usageContext">Контекст использования.</param>
    public void RegisterSingleton<TType>(string usageContext) where TType : class
    {
      RegisterSingleton<TType, TType>(usageContext);
    }

    /// <summary>
    /// Регистрирует тип с конкертной реализацией для типа-одиночки.
    /// </summary>
    /// <typeparam name="TType">Регистрируемый тип-одиночка.</typeparam>
    /// <typeparam name="TConcrete">Конкретная реализация регистрируемого типа.</typeparam>
    public void RegisterSingleton<TType, TConcrete>() where TConcrete : class, TType
    {
      Register<TType, TConcrete>(true, null, string.Empty);
    }

    /// <summary>
    /// Регистрирует тип с конкертной реализацией для типа-одиночки.
    /// </summary>
    /// <typeparam name="TType">Регистрируемый тип-одиночка.</typeparam>
    /// <typeparam name="TConcrete">Конкретная реализация регистрируемого типа.</typeparam>
    /// <param name="usageContext">Контекст использования.</param>
    public void RegisterSingleton<TType, TConcrete>(string usageContext) where TConcrete : class, TType
    {
      Register<TType, TConcrete>(true, null, usageContext);
    }

    /// <summary>
    /// Регистрирует экземпляр типа.
    /// </summary>
    /// <typeparam name="TType">Регистрируемый тип.</typeparam>
    /// <param name="instance">Экземпляр типа.</param>
    public void RegisterInstance<TType>(TType instance) where TType : class
    {
      RegisterInstance<TType, TType>(instance);
    }

    /// <summary>
    /// Регистрирует экземпляр типа.
    /// </summary>
    /// <typeparam name="TType">Регистрируемый тип.</typeparam>
    /// <param name="instance">Экземпляр типа.</param>
    /// <param name="usageContext">Контекст использования.</param>
    public void RegisterInstance<TType>(TType instance, string usageContext) where TType : class
    {
      RegisterInstance<TType, TType>(instance, usageContext);
    }

    /// <summary>
    /// Регистрирует экземпляр типа с конкертной реализацией.
    /// </summary>
    /// <typeparam name="TType">Регистрируемый тип.</typeparam>
    /// <typeparam name="TConcrete">Конкретная реализация регистрируемого типа.</typeparam>
    /// <param name="instance">Экземпляр конкретного типа.</param>
    public void RegisterInstance<TType, TConcrete>(TConcrete instance) where TConcrete : class, TType
    {
      Register<TType, TConcrete>(true, instance, string.Empty);
    }

    /// <summary>
    /// Регистрирует экземпляр типа с конкертной реализацией.
    /// </summary>
    /// <typeparam name="TType">Регистрируемый тип.</typeparam>
    /// <typeparam name="TConcrete">Конкретная реализация регистрируемого типа.</typeparam>
    /// <param name="instance">Экземпляр конкретного типа.</param>
    /// <param name="usageContext">Контекст использования.</param>
    public void RegisterInstance<TType, TConcrete>(TConcrete instance, string usageContext) where TConcrete : class, TType
    {
      Register<TType, TConcrete>(true, instance, usageContext);
    }

    /// <summary>
    /// Создаёт экземпляр для зарегистрированного типа.
    /// </summary>
    /// <typeparam name="TTypeToResolve">Зарегистрированный тип.</typeparam>
    /// <returns></returns>
    public TTypeToResolve Resolve<TTypeToResolve>()
    {
      return (TTypeToResolve)ResolveObject(typeof(TTypeToResolve), string.Empty);
    }

    /// <summary>
    /// Создаёт экземпляр для зарегистрированного типа.
    /// </summary>
    /// <typeparam name="TTypeToResolve">Зарегистрированный тип.</typeparam>
    /// <param name="usageContext">Контекст использования.</param>
    /// <returns></returns>
    public TTypeToResolve Resolve<TTypeToResolve>(string usageContext)
    {
      return (TTypeToResolve)ResolveObject(typeof(TTypeToResolve), usageContext);
    }

    /// <summary>
    /// Создаёт экземпляр для зарегистрированного типа.
    /// </summary>
    /// <typeparam name="TTypeToResolve">Зарегистрированный тип.</typeparam>
    /// <param name="usageContext">Контекст использования.</param>
    /// <param name="args">Аргументы конструктора типа.</param>
    /// <returns></returns>
    public TTypeToResolve Resolve<TTypeToResolve>(string usageContext, params object[] args)
    {
      return (TTypeToResolve)ResolveObject(typeof(TTypeToResolve), usageContext, args);
    }

    /// <summary>
    /// Создаёт экземпляр для зарегистрированного типа.
    /// </summary>
    /// <param name="type">Зарегистрированный тип.</param>
    /// <returns>Экземпляр зарегистрированного типа.</returns>
    public object Resolve(Type type)
    {
      return ResolveObject(type, string.Empty);
    }

    /// <summary>
    /// Создаёт экземпляр для зарегистрированного типа.
    /// </summary>
    /// <param name="type">Зарегистрированный тип.</param>
    /// <param name="usageContext">Контекст использования.</param>
    /// <returns>Экземпляр зарегистрированного типа.</returns>
    public object Resolve(Type type, string usageContext)
    {
      return ResolveObject(type, usageContext);
    }

    /// <summary>
    /// Регистрирует экземпляр конкретного типа для простого типа.
    /// </summary>
    /// <typeparam name="TType">Регистрируемый тип.</typeparam>
    /// <typeparam name="TConcrete">Конкретная реализация регистрируемого типа.</typeparam>
    /// <param name="isSingleton">Признак типа-одиночки.</param>
    /// <param name="instance">Экземпляр класса конкретного типа.</param>
    /// <param name="usageContext">Контекст использования.</param>
    private void Register<TType, TConcrete>(bool isSingleton, TConcrete instance, string usageContext)
    {
      Type type = typeof(TType);
      string key = GetKeyFromType(type, usageContext);
      if (_registeredTypes.ContainsKey(key))
        _registeredTypes.Remove(key);
      _registeredTypes.Add(key, new RegisteredType(typeof(TConcrete), isSingleton, instance));
    }

    /// <summary>
    /// Создаёт экземпляр для зарегистрированного типа.
    /// </summary>
    /// <param name="type">Зарегистрированный тип.</param>
    /// <param name="usageContext">Контекст использования.</param>
    /// <param name="args">Аргументы конструктора типа.</param>
    /// <returns>Экземпляр зарегистрированного типа.</returns>
    private object ResolveObject(Type type, string usageContext, params object[] args)
    {
      var key = GetKeyFromType(type, usageContext);
      var registeredObject = _registeredTypes[key];
      if ((registeredObject == null) && (!string.IsNullOrEmpty(usageContext)))
      {
        key = GetKeyFromType(type, string.Empty);
        registeredObject = _registeredTypes[key];
      }

      if (registeredObject == null)
        throw new ArgumentOutOfRangeException(string.Format("The type {0} has not been registered for context '{1}'.", type.Name, usageContext));
      return GetInstance(registeredObject, usageContext, args);
    }

    /// <summary>
    /// Возвращает экземпляр типа зарегистрированного объекта.
    /// </summary>
    /// <param name="registeredObject">Зарегистрированнный объект.</param>
    /// <param name="usageContext">Контекст использования.</param>
    /// <param name="args">Аргументы конструктора типа.</param>
    /// <returns>Экземпляр типа зарегистрированного объекта.</returns>
    private object GetInstance(RegisteredType registeredObject, string usageContext, params object[] args)
    {
      object instance = registeredObject.SingletonInstance;
      if (instance == null)
      {
        var parameters = args == null ? ResolveConstructorParameters(registeredObject, usageContext) : args;
        instance = registeredObject.CreateInstance(parameters.ToArray());
      }
      return instance;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="registeredObject"></param>
    /// <param name="usageContext">Контекст использования.</param>
    /// <returns></returns>
    private object[] ResolveConstructorParameters(RegisteredType registeredObject, string usageContext)
    {
      var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
      var parameters = constructorInfo.GetParameters().Select(parameter => ResolveObject(parameter.ParameterType, usageContext));
      return parameters.ToArray();
    }

    /// <summary>
    /// Возвращает ключ для зарегистрированного типа.
    /// </summary>
    /// <param name="type">Зарегистрированный тип.</param>
    /// <param name="usageContext">Контекст использования.</param>
    /// <returns>Ключ типа.</returns>
    private string GetKeyFromType(Type type, string usageContext)
    {
      return string.IsNullOrEmpty(usageContext) ? type.ToString() : type.ToString() + "@" + usageContext;
    }
  }
}
