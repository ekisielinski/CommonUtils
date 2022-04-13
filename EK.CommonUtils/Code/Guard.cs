using System.Diagnostics;
using EK.CommonUtils.Time;

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

    public static T? NullableNotNull<T>(T? value, [CAE("value")] string? expr = null) where T : struct
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

    public static int MinValue(int value, int min, [CAE("value")] string? expr = null)
    {
        if (value >= min) return value;

        throw new ArgumentOutOfRangeException(expr ?? nameof(value), value, $"Value cannot be less than {min}.");
    }

    public static long MinValue(long value, long min, [CAE("value")] string? expr = null)
    {
        if (value >= min) return value;

        throw new ArgumentOutOfRangeException(expr ?? nameof(value), value, $"Value cannot be less than {min}.");
    }

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
    
    public static string StringValidChars(string value, Func<char, bool> charValidator, [CAE("value")] string? expr = null)
    {
        NotNull(value, expr ?? nameof(value));
        NotNull(charValidator);

        for (int i = 0; i < value.Length; i++)
        {
            if (!charValidator(value[i]))
            {
                throw new ArgumentException($"The given value contains invalid character: '{value[i]}'.");
            }
        }

        return value;
    }

    public static string StringLength(string value, int? min, int? max, [CAE("value")] string? expr = null)
    {
        NotNull(value, expr ?? nameof(value));

        if (min > max)
        {
            throw new ArgumentException($"The '{nameof(min)}' parameter cannot be greater than the '{nameof(max)}' parameter.", nameof(min));
        }

        if (min.HasValue && value.Length < min) throw new ArgumentException($"Length cannot be less than {min}.", expr ?? nameof(value));
        if (max.HasValue && value.Length > max) throw new ArgumentException($"Length cannot be greater than {max}.", expr ?? nameof(value));

        return value;
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

    #region time
    
    public static DateTime InUtc(DateTime value, [CAE("value")] string? expr = null)
    {
        if (value.Kind == DateTimeKind.Utc) return value;

        const string Message = $"The kind of the given 'DateTime' instance is invalid. Only UTC is allowed.";

        throw new ArgumentException(Message, expr ?? nameof(value));
    }
    
    public static DateTimeRange InUtc(DateTimeRange value, [CAE("value")] string? expr = null)
    {
        if (value.Kind == DateTimeKind.Utc) return value;

        const string Message = $"The kind of the given '{nameof(DateTimeRange)}' instance is invalid. Only UTC is allowed.";

        throw new ArgumentException(Message, expr ?? nameof(value));
    }

    public static TimeSpan NotNegative(TimeSpan value, [CAE("value")] string? expr = null)
    {
        if (value >= TimeSpan.Zero) return value;

        throw new ArgumentOutOfRangeException(expr ?? nameof(value), value, "Negative time spans are not allowed.");
    }

    #endregion
}
