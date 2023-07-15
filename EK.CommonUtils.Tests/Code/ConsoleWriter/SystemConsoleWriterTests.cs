using System;
using System.IO;
using EK.CommonUtils.ConsoleWriter;
using Xunit;

namespace EK.CommonUtils.Tests.ConsoleWriter;

public sealed class SystemConsoleWriterTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("test")]
    public void Write__AddValueToOutput(string? value)
    {
        // arrange
        using var writer = new StringWriter();
        Console.SetOut(writer);

        // act
        var sut = new SystemConsoleWriter();
        sut.Write(value);

        // assert
        var expected = value ?? string.Empty;
        Assert.Equal(expected, writer.ToString());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("test")]
    public void WriteLine__AddValueWithNewLineToOutput(string? value)
    {
        // arrange
        using var writer = new StringWriter();
        Console.SetOut(writer);

        // act
        var sut = new SystemConsoleWriter();
        sut.WriteLine(value);

        // assert
        var expected = (value ?? string.Empty) + Environment.NewLine;
        Assert.Equal(expected, writer.ToString());
    }
}
