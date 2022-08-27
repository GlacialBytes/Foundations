using System.Diagnostics;

namespace GlacialBytes.Foundation.Diagnostics
{
  /// <summary>
  /// Отладочный класс утверждений.
  /// </summary>
  public class DebugAssert
  {
    [Conditional("DEBUG")]
    public static void IsTrue(bool condition, string details = null)
    {
      if (!condition) throw new IsTrueAssertException(details);
    }

    [Conditional("DEBUG")]
    public static void IsFalse(bool condition, string details = null)
    {
      if (condition) throw new IsFalseAssertException(details);
    }

    [Conditional("DEBUG")]
    public static void IsNull(object value, string details = null)
    {
      if (value != null) throw new IsNullAssertException(details);
    }

    [Conditional("DEBUG")]
    public static void IsNotNull(object value, string details = null)
    {
      if (value == null) throw new IsNotNullAssertException(details);
    }

    [Conditional("DEBUG")]
    public static void IsNullOrEmpty(string value, string details = null)
    {
      if (!string.IsNullOrEmpty(value)) throw new IsNotNullOrEmptyAssertException(details);
    }

    [Conditional("DEBUG")]
    public static void IsNotNullOrEmpty(string value, string details = null)
    {
      if (string.IsNullOrEmpty(value)) throw new IsNotNullOrEmptyAssertException(details);
    }

    [Conditional("DEBUG")]
    public static void AreEqual(object expected, object actual, string details = null)
    {
      if (!actual.Equals(expected)) throw new AreEqualAssertException(details);
    }

    [Conditional("DEBUG")]
    public static void AreNotEqual(object notExpected, object actual, string details = null)
    {
      if (actual.Equals(notExpected)) throw new AreNotEqualAssertException(details);
    }

    [Conditional("DEBUG")]
    public static void AreSame(object first, object second, string details = null)
    {
      if (!ReferenceEquals(first, second)) throw new AreSameAssertException(details);
    }

    [Conditional("DEBUG")]
    public static void AreNotSame(object first, object second, string details = null)
    {
      if (ReferenceEquals(first, second)) throw new AreNotSameAssertException(details);
    }
  }
}
