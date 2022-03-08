using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EK.CommonUtils.Tests;

public sealed class MiscUtilsTests
{
    [Fact(Timeout = 500)]
    public async Task DelayAsync_ZeroSeconds()
    {
        await MiscUtils.DelayAsync(TimeSpan.FromSeconds(0), CancellationToken.None);
    }

    [Fact(Timeout = 500)]
    public async Task DelayAsync_CancelledToken()
    {
        await MiscUtils.DelayAsync(TimeSpan.FromSeconds(10), new CancellationToken(canceled: true));
    }

    [Fact(Timeout = 500)]
    public async Task DelayAsync_CancellationTokenSourceWithTimeout()
    {
        var cts = new CancellationTokenSource(50);

        await MiscUtils.DelayAsync(TimeSpan.FromSeconds(10), cts.Token);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("test")]
    [InlineData("123x")]
    [InlineData("2147483648")] // int.MaxValue+1
    public void TryParse_NotParsableStrings(string number)
    {
        int? parsed = MiscUtils.TryParse(number);

        Assert.True(!parsed.HasValue);
    }

    [Theory]
    [InlineData("-1")]
    [InlineData("0")]
    [InlineData("256")]
    [InlineData("65536")]
    [InlineData("2147483646")] // int.MaxValue
    [InlineData(" 123 ")]
    [InlineData(" 007 ")]
    public void TryParse_ParsableStrings(string number)
    {
        int? parsed = MiscUtils.TryParse(number);

        Assert.True(parsed.HasValue);
    }

    [Fact]
    public void TryCatchAction_ThrowsException_HnadlerIsExecuted()
    {
        Exception? caughtException = null;

        MiscUtils.TryCatchAction(() => throw new Exception("test!"), ex =>
        {
            caughtException = ex;
        });

        Assert.NotNull(caughtException);
        Assert.IsType<Exception>(caughtException);
        Assert.Equal("test!", caughtException!.Message);
    }
}
