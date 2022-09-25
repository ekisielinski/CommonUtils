namespace EK.CommonUtils.Time;

public readonly record struct Duration
{
    public Duration(TimeSpan value)
    {
        Value = Guard.NotNegative(value);
    }

    //====== public properties

    public TimeSpan Value { get; }

    //====== public static properties

    public static Duration Zero { get; } = new(TimeSpan.Zero);

    //====== override: Object

    public override string ToString()
    {
        string format = Value.Days > 0 ? "d'd 'hh':'mm':'ss' .'fff" : "hh':'mm':'ss' .'fff";

        return Value.ToString(format);
    }
}
