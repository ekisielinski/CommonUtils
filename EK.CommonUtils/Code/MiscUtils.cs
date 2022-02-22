namespace EK.CommonUtils.Code;

public static class MiscUtils
{
    public static async Task DelayAsync(TimeSpan timeout, CancellationToken ct)
    {
        try
        {
            await Task.Delay(timeout, ct);
        }
        catch (TaskCanceledException)
        {
            // continue execution
        }
    }

    public static bool IsDigit09(char ch)
    {
        return ch >= '0' && ch <= '9';
    }
}
