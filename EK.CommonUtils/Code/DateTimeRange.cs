namespace EK.CommonUtils;

public sealed record DateTimeRange
{
    public DateTimeRange(DateTime start, DateTime end)
    {
        if (start.Kind != end.Kind)
        {
            throw new ArgumentException($"The '{nameof(DateTime.Kind)}' property in both parameters must have the same value.");
        }

        if (start > end)
        {
            throw new ArgumentException($"The '{nameof(start)}' parameter cannot be greater than the '{nameof(end)}' parameter.", nameof(start));
        }

        Start = start;
        End = end;
    }

    //====== public properties

    public DateTime Start { get; }
    public DateTime End   { get; }
}
