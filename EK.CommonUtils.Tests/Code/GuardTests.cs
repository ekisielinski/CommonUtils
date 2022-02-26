using System;
using Xunit;

namespace EK.CommonUtils.Tests;

public sealed class GuardTests
{
    [Fact]
    public void NotNull_ArgIsNull_ThrowException()
    {
        object value = null!;

        Assert.Throws<ArgumentNullException>(() => Guard.NotNull(value));
    }

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
    public void NotNegativeInt64_NegativeValues(long value)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Guard.NotNegative(value));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(int.MaxValue)]
    [InlineData(long.MaxValue)]
    public void NotNegativeInt64_ZeroAndPositiveValues(long value)
    {
        long result = Guard.NotNegative(value);

        Assert.Equal(result, value);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void NotNegativeInt32_NegativeValues(int value)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Guard.NotNegative(value));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(int.MaxValue)]
    public void NotNegativeInt32_ZeroAndPositiveValues(int value)
    {
        long result = Guard.NotNegative(value);

        Assert.Equal(result, value);
    }
}
