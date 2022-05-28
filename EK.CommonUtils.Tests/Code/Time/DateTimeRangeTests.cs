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

    [Theory]
    [InlineData(DateTimeKind.Utc,  0, true )]
    [InlineData(DateTimeKind.Utc,  1, true )]
    [InlineData(DateTimeKind.Utc,  5, true )]
    [InlineData(DateTimeKind.Utc, -1, false)]
    [InlineData(DateTimeKind.Utc,  6, false)]
    public void Contains_ValidKind_ReturnExpectedValue(DateTimeKind kind, int daysFromNow, bool expectedValue)
    {
        var now = DateTime.UtcNow;
        var sut = new DateTimeRange(now, now.AddDays(5));

        var pointInTime = DateTime.SpecifyKind(now.AddDays(daysFromNow), kind);

        Assert.Equal(expectedValue, sut.Contains(pointInTime));
    }

    [Theory]
    [InlineData(DateTimeKind.Local,        0)]
    [InlineData(DateTimeKind.Local,       -1)]
    [InlineData(DateTimeKind.Unspecified,  0)]
    [InlineData(DateTimeKind.Unspecified,  6)]
    public void Contains_InvalidKind_ThrowException(DateTimeKind kind, int daysFromNow)
    {
        var now = DateTime.UtcNow;
        var sut = new DateTimeRange(now, now.AddDays(5));

        var pointInTime = DateTime.SpecifyKind(now.AddDays(daysFromNow), kind);

        Assert.Throws<ArgumentException>(() => sut.Contains(pointInTime));
    }
}
