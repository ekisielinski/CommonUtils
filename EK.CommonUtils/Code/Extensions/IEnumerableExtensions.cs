namespace EK.CommonUtils.Extensions;

public static class IEnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> me, Action<T> method)
    {
        Guard.NotNull(me);
        Guard.NotNull(method);

        foreach (T item in me)
        {
            method.Invoke(item);
        }
    }

    public static void ForEach<T>(this IEnumerable<T> me, Action<T, int> method)
    {
        Guard.NotNull(me);
        Guard.NotNull(method);

        int index = 0;

        foreach (T item in me)
        {
            method.Invoke(item, index++);
        }
    }

    public static void Consume<T>(this IEnumerable<T> me)
    {
        Guard.NotNull(me);

        foreach (T _ in me)
        {
            // nop
        }
    }

    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> me) where T : class
    {
        Guard.NotNull(me);

        foreach (T? item in me)
        {
            if (item is not null) yield return item;
        }
    }
    
    public static IEnumerable<T?> WhereNullableHasValue<T>(this IEnumerable<T?> me) where T : struct
    {
        Guard.NotNull(me);

        foreach (T? item in me)
        {
            if (item.HasValue) yield return item;
        }
    }

    public static List<T> EmptyIfNull<T>(this List<T>? me)
    {
        return me ?? new List<T>(0);
    }

    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? me)
    {
        return me ?? Enumerable.Empty<T>();
    }
}
