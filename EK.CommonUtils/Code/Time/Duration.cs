namespace EK.CommonUtils.Tests.Time;

public readonly record struct Duration
{
    public Duration(TimeSpan value)
    {
        Guard.NotNegative(value.Ticks);

        Value = value;
    }

    //====== public properties

    public TimeSpan Value { get; }

    //====== public static properties

    public static Duration Zero { get; } = new(TimeSpan.Zero);

    //====== override: Object

    public override string ToString() => Value.ToString();
}
