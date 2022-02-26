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
}
