using System;
using EK.CommonUtils.Time;
using Xunit;

namespace EK.CommonUtils.Tests.Time;

public sealed class DateTimeRangeTests
{
    [Fact]
    public void Ctor_DifferentKinds_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var now = DateTime.Now;
            var utcNow = now.ToUniversalTime();

            new DateTimeRange(now, utcNow);
        });
    }

    [Fact]
    public void Ctor_EndTimeBeforeStartTime_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var now = DateTime.Now;
            var past = now - TimeSpan.FromSeconds(1);

            new DateTimeRange(now, past);
        });
    }
}
