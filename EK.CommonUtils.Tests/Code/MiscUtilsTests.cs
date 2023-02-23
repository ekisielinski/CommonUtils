using System;
using System.Threading;
using System.Threading.Tasks;
using EK.CommonUtils.Time;
using Xunit;

namespace EK.CommonUtils.Tests;

public sealed class MiscUtilsTests
{
    [Fact(Timeout = 500)]
    public async Task DelayAsync_ZeroSeconds_ReturnNotCancelled()
    {
        var result = await MiscUtils.DelayAsync(Duration.Zero, CancellationToken.None);

        Assert.Equal(CancellableOperationResult.NotCancelled, result);
    }

    [Fact(Timeout = 500)]
    public async Task DelayAsync_CancelledToken_ReturnCancelled()
    {
        var result = await MiscUtils.DelayAsync(new(TimeSpan.FromSeconds(10)), new CancellationToken(canceled: true));

        Assert.Equal(CancellableOperationResult.Cancelled, result);
    }

    [Fact(Timeout = 500)]
    public async Task DelayAsync_CancellationTokenSourceWithTimeout_ReturnCancelled()
    {
        var cts = new CancellationTokenSource(50);

        var result = await MiscUtils.DelayAsync(new(TimeSpan.FromSeconds(10)), cts.Token);

        Assert.Equal(CancellableOperationResult.Cancelled, result);
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

    [Fact]
    public void SyncExecuteAction_SyncObjectReleased_ExecuteMethod()
    {
        var sync = new object();
        var cts = new CancellationTokenSource();

        bool executed = MiscUtils.SyncExecuteAction(sync, () => cts.Cancel(), Timeout.InfiniteTimeSpan);

        Assert.True(cts.IsCancellationRequested);
        Assert.True(executed);
    }

    [Fact]
    public void SyncExecuteAction_WithTimeoutAndAcquiredSyncObject_SkipMethodExecution()
    {
        var sync = new object();
        var mre = new ManualResetEvent(false);
        var cts = new CancellationTokenSource();

        Task.Run(() =>
        {
            lock (sync)
            {
                mre.Set();
                Thread.Sleep(5000);
            }
        });

        mre.WaitOne();

        bool executed = MiscUtils.SyncExecuteAction(sync, () => cts.Cancel(), TimeSpan.FromMilliseconds(5));

        Assert.False(cts.IsCancellationRequested);
        Assert.False(executed);
    }
}
