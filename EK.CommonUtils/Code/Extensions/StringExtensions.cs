namespace EK.CommonUtils.Extensions;

public static class StringExtensions
{
    public static string? Truncate(this string? me, int maxLength)
    {
        Guard.NotNegative(maxLength);

        if (me is null) return null;
        if (me.Length <= maxLength) return me;

        return me[..maxLength];
    }

    public static string? ToNullIfEmpty(this string? me)
    {
        return me?.Length == 0 ? null : me;
    }

    public static string ToEmptyIfNull(this string? me)
    {
        return me ?? string.Empty;
    }
}
