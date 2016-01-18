using System;

namespace WordCounter.Core
{
  public static class ParameterChecker
  {
    public static void ThrowIfNull<T>(this T val, string parameterName)
    {
      if (val == null)
      { throw new ArgumentNullException(parameterName);}
    }
  }
}
