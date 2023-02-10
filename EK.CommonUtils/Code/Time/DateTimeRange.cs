namespace EK.CommonUtils.Time;

public readonly record struct DateTimeRange
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

    public DateTimeKind Kind => Start.Kind;

    //====== public methods

    public bool Contains(DateTime dateTime)
    {
        if (dateTime.Kind == Kind) return dateTime >= Start && dateTime <= End;

        throw new ArgumentException(
            $"The '{nameof(DateTime.Kind)}' property of the given parameter must have the same value as " +
            $"the '{nameof(DateTime.Kind)}' property of the current instance ({Kind}).",
            nameof(dateTime));
    }

    //====== override: Object

    public override string ToString()
    {
        const string Format = "yyyy.MM.dd HH:mm:ss";

        var strStart = Start.ToString(Format);
        var strEnd = End.ToString(Format);

        return $"{strStart} .. {strEnd} | {Kind}";
    }
}
