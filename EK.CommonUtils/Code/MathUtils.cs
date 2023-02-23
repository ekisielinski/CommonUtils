namespace EK.CommonUtils;

public static class MathUtils
{
    public static int Clamp(int value, int min, int max)
    {
        if (min > max) throw new ArgumentException($"The '{min}' parameter cannot be greater than the '{max}' parameter.");

        return Math.Min(Math.Max(value, min), max);
    }
}
