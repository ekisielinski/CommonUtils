namespace EK.CommonUtils;

public readonly record struct DataSize
{
    internal const long KiB = 1L << 10;
    internal const long MiB = 1L << 20;
    internal const long GiB = 1L << 30;
    internal const long TiB = 1L << 40;
    internal const long PiB = 1L << 50;

    //====== ctors

    public DataSize(long bytes)
    {
        Bytes = Guard.NotNegative(bytes);
    }

    //====== public properties

    public long Bytes { get; }

    //====== public static properties

    public static DataSize Zero   { get; } = new(0);
    public static DataSize OneKiB { get; } = new(KiB);
    public static DataSize OneMiB { get; } = new(MiB);
    public static DataSize OneGiB { get; } = new(GiB);
    public static DataSize OneTiB { get; } = new(TiB);
    public static DataSize OnePiB { get; } = new(PiB);

    //====== public static methods

    public static DataSize Sum(DataSize first, DataSize second)
    {
        return new(first.Bytes + second.Bytes);
    }

    //====== operators

    public static bool operator >(DataSize lhs, DataSize rhs) => lhs.Bytes > rhs.Bytes;
    public static bool operator <(DataSize lhs, DataSize rhs) => lhs.Bytes < rhs.Bytes;

    public static implicit operator long (DataSize dataSize) => dataSize.Bytes;

    //====== override: Object

    public override string ToString()
    {
        return Bytes switch
        {
            >= PiB => Bytes / PiB + " PiB",
            >= TiB => Bytes / TiB + " TiB",
            >= GiB => Bytes / GiB + " GiB",
            >= MiB => Bytes / MiB + " MiB",
            >= KiB => Bytes / KiB + " KiB",

            _ => Bytes + " B"
        };
    }
}
