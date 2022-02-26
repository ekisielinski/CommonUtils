using EK.CommonUtils.Extensions;
using Xunit;

namespace EK.CommonUtils.Tests.Extensions;

public sealed class StringExtensionsTests
{
    [Theory]
    [InlineData(null, 5, null)]
    [InlineData("", 5, "")]
    [InlineData("empty", 0, "")]
    [InlineData("cut_me", 3, "cut")]
    [InlineData("bigMaxLength", 50, "bigMaxLength")]
    public void Test(string current, int maxLength, string expected)
    {
        string? truncated = current.Truncate(maxLength);

        Assert.Equal(expected, truncated);
    }
}
