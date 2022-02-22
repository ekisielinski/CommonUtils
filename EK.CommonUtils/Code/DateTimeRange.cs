namespace EK.CommonUtils;

public sealed record DateTimeRange
{
    public DateTimeRange(DateTime start, DateTime end)
    {
        if (start.Kind != end.Kind)
        {
            throw new ArgumentException($"The {nameof(DateTime.Kind)} property in both parameters must have the same value.");
        }

        Start = start;
        End = end;
    }

    //====== public properties

    public DateTime Start { get; }
    public DateTime End   { get; }
}
