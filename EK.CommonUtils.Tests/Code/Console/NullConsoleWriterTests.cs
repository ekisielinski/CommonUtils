using EK.CommonUtils.Console;
using Xunit;

namespace EK.CommonUtils.Tests.Console;

public sealed class NullConsoleWriterTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("test")]
    public void Write__DoNothing(string? value)
    {
        // arrange
        var sut = NullConsoleWriter.Instance;

        // act
        sut.Write(value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("test")]
    public void WriteLine__DoNothing(string? value)
    {
        // arrange
        var sut = NullConsoleWriter.Instance;

        // act
        sut.WriteLine(value);
    }

    [Fact]
    public void Instance__CallTwice__ReturnSameReference()
    {
        // act
        var obj1 = NullConsoleWriter.Instance;
        var obj2 = NullConsoleWriter.Instance;

        // assert
        Assert.Same(obj1, obj2);
    }
}
