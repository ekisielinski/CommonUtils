﻿using System;
using EK.CommonUtils.Time;
using Xunit;

namespace EK.CommonUtils.Tests;

public sealed class GuardTests
{
    #region null

    [Fact]
    public void NotNull_ArgIsNull_ThrowException()
    {
        object value = null!;

        Assert.Throws<ArgumentNullException>(() => Guard.NotNull(value));
    }

    [Fact]
    public void NullableNotNull_ArgIsNull_ThrowException()
    {
        int? value = null;

        Assert.Throws<ArgumentNullException>(() => Guard.NullableNotNull(value));
    }

    [Fact]
    public void NullableNotNull_ArgIsNotNull_Pass()
    {
        int? value = 10;

        int? result = Guard.NullableNotNull(value);

        Assert.Equal(10, result);
    }

    [Theory]
    [InlineData(null, "B")]
    [InlineData("A", null)]
    public void BothWithValueOrBothNull_OneArgIsNull_ThrowException(object? first, object? second)
    {
        Assert.Throws<ArgumentException>(() => Guard.BothWithValueOrBothNull(first, second));
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("A", "B")]
    public void BothWithValueOrBothNull_ValidInput(object? first, object? second)
    {
        Guard.BothWithValueOrBothNull(first, second);
    }

    #endregion

    [Fact]
    public void NotNull_ArgIsString_ReturnTheSameInstance()
    {
        string data = "test-data";

        string result = Guard.NotNull(data);

        Assert.Same(data, result);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    [InlineData(long.MinValue)]
    public void NotNegativeInt64_NegativeValues_ThrowException(long value)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Guard.NotNegative(value));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(int.MaxValue)]
    [InlineData(long.MaxValue)]
    public void NotNegativeInt64_ZeroAndPositiveValues_Pass(long value)
    {
        long result = Guard.NotNegative(value);

        Assert.Equal(result, value);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void NotNegativeInt32_NegativeValues_ThrowException(int value)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Guard.NotNegative(value));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(int.MaxValue)]
    public void NotNegativeInt32_ZeroAndPositiveValues_Pass(int value)
    {
        long result = Guard.NotNegative(value);

        Assert.Equal(result, value);
    }

    #region strings

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void NotNullOrEmpty_InvalidInput_ThrowException(string value)
    {
        Assert.Throws<ArgumentException>(() => Guard.NotNullOrEmpty(value));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\n")]
    [InlineData("\t")]
    public void NotNullOrWhitespace_InvalidInput_ThrowException(string value)
    {
        Assert.Throws<ArgumentException>(() => Guard.NotNullOrWhitespace(value));
    }

    #endregion

    #region time

    [Theory]
    [InlineData(DateTimeKind.Local)]
    [InlineData(DateTimeKind.Utc)]
    [InlineData(DateTimeKind.Unspecified)]
    public void InUtc_DateTime_ThrowExceptionIfNotUtc(DateTimeKind kind)
    {
        var sut = DateTime.SpecifyKind(DateTime.Now, kind);

        if (kind != DateTimeKind.Utc)
        {
            Assert.Throws<ArgumentException>(() => Guard.InUtc(sut));
        }
    }

    [Theory]
    [InlineData(DateTimeKind.Local)]
    [InlineData(DateTimeKind.Utc)]
    [InlineData(DateTimeKind.Unspecified)]
    public void InUtc_DateTimeRange_ThrowExceptionIfNotUtc(DateTimeKind kind)
    {
        var now = DateTime.SpecifyKind(DateTime.Now, kind);
        var sut = new DateTimeRange(now, now);

        if (kind != DateTimeKind.Utc)
        {
            Assert.Throws<ArgumentException>(() => Guard.InUtc(sut));
        }
    }
    
    [Fact]
    public void NotNegative_TimeSpan()
    {
        var negative = TimeSpan.FromTicks(-1);

        Assert.Throws<ArgumentOutOfRangeException>(() => Guard.NotNegative(negative));

        var zero = TimeSpan.Zero;
        var positive = TimeSpan.FromTicks(1);

        Guard.NotNegative(zero);
        Guard.NotNegative(positive);
    }
    
    #endregion
}
