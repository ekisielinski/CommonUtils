using System.Diagnostics;
using CAEAttribute = System.Runtime.CompilerServices.CallerArgumentExpressionAttribute;

namespace EK.CommonUtils.Code;

[DebuggerStepThrough]
public static class Guard
{
    public static T NotNull<T>(T value, [CAE("value")] string? expr = null) where T : class
    {
        return value ?? throw new ArgumentNullException(expr ?? nameof(value));
    }
}
