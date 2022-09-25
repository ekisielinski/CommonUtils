using System;
using EK.CommonUtils.Time;
using Xunit;

namespace EK.CommonUtils.Tests.Time;

public sealed class DurationTests
{
    [Fact]
    public void Ctor_NegativeTimeSpan_ThrowException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Duration(TimeSpan.FromTicks(-1)));
    }

    [Fact]
    public void Ctor_PositiveTimeSpan_Pass()
    {
        _ = new Duration(TimeSpan.FromTicks(1));
    }

    [Fact]
    public void Ctor_TimeSpanZero_Pass()
    {
        _ = new Duration(TimeSpan.Zero);
    }
}
