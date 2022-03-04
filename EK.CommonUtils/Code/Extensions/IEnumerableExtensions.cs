namespace EK.CommonUtils.Extensions;

public static class IEnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> me, Action<T> method)
    {
        Guard.NotNull(me);
        Guard.NotNull(method);

        foreach (var item in me)
        {
            method.Invoke(item);
        }
    }

    public static void ForEach<T>(this IEnumerable<T> me, Action<T, int> method)
    {
        Guard.NotNull(me);
        Guard.NotNull(method);

        int index = 0;

        foreach (var item in me)
        {
            method.Invoke(item, index++);
        }
    }
}
