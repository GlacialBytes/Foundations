using System;

namespace GlacialBytes.Foundation.Diagnostics
{
  /// <summary>
  /// Проверка утверждения закончилась неудачно.
  /// </summary>
  public class AssertFailedException : Exception
  {
    public string Details { get; }

    public AssertFailedException()
    {
      Details = "";
    }

    public AssertFailedException(string details)
    {
      Details = details;
    }
  }

  /// <summary>
  /// Проверка утверждения IsTrue закончилась неудачно.
  /// </summary>
  public class IsTrueAssertException : AssertFailedException
  {
    public IsTrueAssertException()
      : base()
    {
    }

    public IsTrueAssertException(string details)
      : base(details)
    {
    }
  }

  /// <summary>
  /// Проверка утверждения IsFalse закончилась неудачно.
  /// </summary>
  public class IsFalseAssertException : AssertFailedException
  {
    public IsFalseAssertException()
      : base()
    {
    }

    public IsFalseAssertException(string details)
      : base(details)
    {
    }
  }

  /// <summary>
  /// Проверка утверждения IsNull закончилась неудачно.
  /// </summary>
  public class IsNullAssertException : AssertFailedException
  {
    public IsNullAssertException()
      : base()
    {
    }

    public IsNullAssertException(string details)
      : base(details)
    {
    }
  }

  /// <summary>
  /// Проверка утверждения IsNotNull закончилась неудачно.
  /// </summary>
  public class IsNotNullAssertException : AssertFailedException
  {
    public IsNotNullAssertException()
      : base()
    {
    }

    public IsNotNullAssertException(string details)
      : base(details)
    {
    }
  }

  /// <summary>
  /// Проверка утверждения IsNullOrEmpty закончилась неудачно.
  /// </summary>
  public class IsNullOrEmptyAssertException : AssertFailedException
  {
    public IsNullOrEmptyAssertException()
      : base()
    {
    }

    public IsNullOrEmptyAssertException(string details)
      : base(details)
    {
    }
  }

  /// <summary>
  /// Проверка утверждения IsNotNullOrEmpty закончилась неудачно.
  /// </summary>
  public class IsNotNullOrEmptyAssertException : AssertFailedException
  {
    public IsNotNullOrEmptyAssertException()
      : base()
    {
    }

    public IsNotNullOrEmptyAssertException(string details)
      : base(details)
    {
    }
  }

  /// <summary>
  /// Проверка утверждения AreEqual закончилась неудачно.
  /// </summary>
  public class AreEqualAssertException : AssertFailedException
  {
    public AreEqualAssertException()
      : base()
    {
    }

    public AreEqualAssertException(string details)
      : base(details)
    {
    }
  }

  /// <summary>
  /// Проверка утверждения AreNotEqual закончилась неудачно.
  /// </summary>
  public class AreNotEqualAssertException : AssertFailedException
  {
    public AreNotEqualAssertException()
      : base()
    {
    }

    public AreNotEqualAssertException(string details)
      : base(details)
    {
    }
  }

  /// <summary>
  /// Проверка утверждения AreSame закончилась неудачно.
  /// </summary>
  public class AreSameAssertException : AssertFailedException
  {
    public AreSameAssertException()
      : base()
    {
    }

    public AreSameAssertException(string details)
      : base(details)
    {
    }
  }

  /// <summary>
  /// Проверка утверждения AreNotSame закончилась неудачно.
  /// </summary>
  public class AreNotSameAssertException : AssertFailedException
  {
    public AreNotSameAssertException()
      : base()
    {
    }

    public AreNotSameAssertException(string details)
      : base(details)
    {
    }
  }
}
