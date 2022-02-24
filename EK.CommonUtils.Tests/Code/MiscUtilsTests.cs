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
}
