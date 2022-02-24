using System.Diagnostics;

using CAEAttribute = System.Runtime.CompilerServices.CallerArgumentExpressionAttribute;

namespace EK.CommonUtils;

[DebuggerStepThrough]
public static class Guard
{
    public static T NotNull<T>(T value, [CAE("value")] string? expr = null) where T : class
    {
        return value ?? throw new ArgumentNullException(expr ?? nameof(value));
    }

    public static long NotNegative(long value, [CAE("value")] string? expr = null)
    {
        return value >= 0 ? value : throw new ArgumentOutOfRangeException(expr ?? nameof(value), value, "Negative values are not allowed.");
    }
}
