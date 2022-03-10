using System.Diagnostics;

using CAEAttribute = System.Runtime.CompilerServices.CallerArgumentExpressionAttribute;

namespace EK.CommonUtils;

[DebuggerStepThrough]
public static class Guard
{
    #region null

    public static T NotNull<T>(T value, [CAE("value")] string? expr = null) where T : class
    {
        return value ?? throw new ArgumentNullException(expr ?? nameof(value));
    }

    public static void BothWithValueOrBothNull(object? first, object? second)
    {
        if (first is null && second is null) return;
        if (first is not null && second is not null) return;

        throw new ArgumentException("Both parameters must have value (not null) or both parameters must be null.");
    }

    #endregion

    #region range

    public static long NotNegative(long value, [CAE("value")] string? expr = null)
    {
        return value >= 0 ? value : throw new ArgumentOutOfRangeException(expr ?? nameof(value), value, "Negative values are not allowed.");
    }

    public static int NotNegative(int value, [CAE("value")] string? expr = null)
    {
        return value >= 0 ? value : throw new ArgumentOutOfRangeException(expr ?? nameof(value), value, "Negative values are not allowed.");
    }

    public static int InRange(int value, int fromInclusive, int toInclusive, [CAE("value")] string? expr = null)
    {
        if (fromInclusive > toInclusive)
        {
            throw new ArgumentException(
                $"The '{nameof(fromInclusive)}' parameter cannot be greater than the '{nameof(toInclusive)}' parameter.", nameof(fromInclusive));
        }

        if (value >= fromInclusive && value <= toInclusive) return value;

        throw new ArgumentOutOfRangeException(expr ?? nameof(value), value, $"Value is out of range [{fromInclusive}..{toInclusive}].");
    }

    public static long InRange(long value, long fromInclusive, long toInclusive, [CAE("value")] string? expr = null)
    {
        if (fromInclusive > toInclusive)
        {
            throw new ArgumentException(
                $"The '{nameof(fromInclusive)}' parameter cannot be greater than the '{nameof(toInclusive)}' parameter.", nameof(fromInclusive));
        }

        if (value >= fromInclusive && value <= toInclusive) return value;
        
        throw new ArgumentOutOfRangeException(expr ?? nameof(value), value, $"Value is out of range [{fromInclusive}..{toInclusive}].");
    }

    public static int? InRangeOrNull(int? value, int fromInclusive, int toInclusive, [CAE("value")] string? expr = null)
    {
        if (fromInclusive > toInclusive)
        {
            throw new ArgumentException(
                $"The '{nameof(fromInclusive)}' parameter cannot be greater than the '{nameof(toInclusive)}' parameter.", nameof(fromInclusive));
        }

        if (value is null) return null;

        return InRange(value.Value, fromInclusive, toInclusive, expr ?? $"{nameof(value)}.{nameof(value.Value)}");
    }

    #endregion

    #region strings

    public static string NotNullOrEmpty(string value, [CAE("value")] string? expr = null)
    {
        return !string.IsNullOrEmpty(value) ? value : throw new ArgumentException("Cannot be null or empty.", expr ?? nameof(value));
    }

    public static string NotNullOrWhitespace(string value, [CAE("value")] string? expr = null)
    {
        return !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Cannot be null or white space.", expr ?? nameof(value));
    }

    #endregion

    public static IReadOnlyList<T> ToSnapshot<T>(IEnumerable<T> items, [CAE("items")] string? expr = null) where T : class
    {
        NotNull(items, expr ?? nameof(items));

        var result = new List<T>();

        foreach (T item in items)
        {
            if (item is null) throw new ArgumentException("Given enumerable contains null element.", expr ?? nameof(items));

            result.Add(item);
        }

        return result;
    }
}
