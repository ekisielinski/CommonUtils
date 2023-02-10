using System.Diagnostics.CodeAnalysis;

namespace EK.CommonUtils;

[ExcludeFromCodeCoverage]
public static class Actions
{
    public static void Empty() { }

    public static void Empty<T>(T _) { }

    public static void Empty<T1, T2>(T1 _1, T2 _2) { }

    public static void Empty<T1, T2, T3>(T1 _1, T2 _2, T3 _3) { }
}
