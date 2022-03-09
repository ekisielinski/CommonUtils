using System;
using System.IO;
using System.Text;
using Xunit;

namespace EK.CommonUtils.Tests;

public sealed class StreamUtilsTests
{
    const string TestMessage = "Hello!123";

    [Fact]
    public void ToStream_AsciiString()
    {
        Stream stream = StreamUtils.ToStream(TestMessage, Encoding.ASCII);

        Assert.NotNull(stream);
        Assert.Equal(TestMessage.Length, stream.Length);

        const int Position = 5;

        stream.Position = Position;
        int data = stream.ReadByte();

        Assert.Equal(TestMessage[Position], data);
    }

    [Fact]
    public void ToString_AsciiString_StreamGetsDisposed()
    {
        byte[] bytes = Encoding.ASCII.GetBytes(TestMessage);
        var ms = new MemoryStream(bytes);
        
        string actualMessage = StreamUtils.ToString(ms, encoding: Encoding.ASCII);

        Assert.Equal(TestMessage, actualMessage);
        Assert.Throws<ObjectDisposedException>(() => ms.Position = 0);
    }
}
