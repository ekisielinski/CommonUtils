using EK.CommonUtils.Time;

namespace EK.CommonUtils;

public static class MiscUtils
{
    public static async Task<CancellableOperationResult> DelayAsync(Duration duration, CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(duration.Value, cancellationToken);

            return CancellableOperationResult.NotCancelled;
        }
        catch (TaskCanceledException)
        {
            return CancellableOperationResult.Cancelled;
        }
    }

    public static bool IsDigit09(char ch)
    {
        return ch >= '0' && ch <= '9';
    }
    
    public static int? TryParse(string? number)
    {
        return int.TryParse(number, out int result) ? result : null;
    }

    public static bool TryCatchAction(Action method, Action<Exception> exceptionHandler)
    {
        Guard.NotNull(method);
        Guard.NotNull(exceptionHandler);

        try
        {
            method.Invoke();
            
            return true;
        }
        catch (Exception ex)
        {
            try
            {
                exceptionHandler(ex);
            }
            catch
            {
                // nop
            }

            return false;
        }
    }

    public static bool SyncExecuteAction(object sync, Action method, TimeSpan enterLockTimeout)
    {
        Guard.NotNull(sync);
        Guard.NotNull(method);
        Guard.Timeout(enterLockTimeout);

        bool lockTaken = false;

        try
        {
            Monitor.TryEnter(sync, enterLockTimeout, ref lockTaken);

            if (lockTaken)
            {
                method.Invoke();
            }

            return lockTaken;
        }
        finally
        {
            if (lockTaken) Monitor.Exit(sync);
        }
    }
}
