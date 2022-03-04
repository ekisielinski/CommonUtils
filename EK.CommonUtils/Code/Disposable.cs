namespace EK.CommonUtils;

public sealed class Disposable : IDisposable
{
    readonly Action disposeBody;

    bool isDisposed = false;

    //====== ctors

    public Disposable(Action disposeBody)
    {
        this.disposeBody = Guard.NotNull(disposeBody);
    }

    //====== IDisposable

    public void Dispose()
    {
        if (isDisposed) return;

        isDisposed = true;
        disposeBody.Invoke();
    }

    //====== public static properties

    public static IDisposable Empty { get; } = new Disposable(Actions.Empty);
}
