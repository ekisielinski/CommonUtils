using EK.CommonUtils.Code;
using System;
using Xunit;

namespace EK.CommonUtils.Tests;

public sealed class GuardTests
{
    [Fact]
    public void NotNull_ArgIsNull_ThrowsException()
    {
        object? value = null;

        Assert.Throws<ArgumentNullException>(() => Guard.NotNull(value));
    }

    [Fact]
    public void NotNull_ArgIsString_ReturnsTheSameObject()
    {
        string data = "test-data";

        string result = Guard.NotNull(data);

        Assert.Same(data, result);
    }
}
