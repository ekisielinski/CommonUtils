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

    [Theory]
    [InlineData(DateTimeKind.Local)]
    [InlineData(DateTimeKind.Utc)]
    [InlineData(DateTimeKind.Unspecified)]
    public void Kind_DifferentKinds_KindIsValid(DateTimeKind kind)
    {
        var now = DateTime.SpecifyKind(DateTime.Now, kind);
        var sut = new DateTimeRange(now, now);
        
        Assert.Equal(kind, sut.Kind);
    }
}
