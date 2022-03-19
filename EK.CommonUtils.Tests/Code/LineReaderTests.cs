using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EK.CommonUtils.Extensions;
using Xunit;

namespace EK.CommonUtils.Tests;

public sealed class LineReaderTests
{
    [Theory]
    [InlineData(3, new[] { "line 1", "line 2", "line 3" }, "line 1\nline 2\nline 3")]
    [InlineData(3, new[] { "line 1", "line 2", "line 3" }, "line 1\r\nline 2\r\nline 3")]
    [InlineData(3, new[] { "a", "b", "c" },                "a\nb\nc\n")]
    [InlineData(4, new[] { "a", "b", "c", "" },            "a\nb\nc\n\n")]
    [InlineData(1, new[] { "" },                           "\n")]
    [InlineData(0, new string[] { },                       "")]
    public void FromStream_ZeroOrMoreLines(int expectedLineCount, string[] expectedLines, string inputText)
    {
        Stream stream = StreamUtils.ToStream(inputText, Encoding.UTF8);

        List<string> lines = LineReader.FromStream(() => stream).ToList();

        Assert.Equal(expectedLineCount, lines.Count);
        Assert.Equal(expectedLines, lines);
    }

    [Fact]
    public void FromStream_IterateThroughAllElements_UnderlyingStreamGetsDisposed()
    {
        Stream stream = StreamUtils.ToStream("abc\ndef", Encoding.UTF8);

        LineReader.FromStream(() => stream).Consume();
        
        Assert.Throws<ObjectDisposedException>(() => _ = stream.Length);
    }

    [Fact]
    public void FromStream_IterateManyTimes_StreamFactoryExecutesEachIteration()
    {
        int streamCount = 0;

        var streamFactory = () =>
        {
            streamCount++;

            return StreamUtils.ToStream("abc\ndef", Encoding.UTF8);
        };

        var lineReader = LineReader.FromStream(streamFactory);

        lineReader.Consume();
        lineReader.Consume();
        lineReader.Consume();

        Assert.Equal(3, streamCount);
    }
}
