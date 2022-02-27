namespace EK.CommonUtils;

public class AddOnlyList<T>
{
    readonly List<T> items = new();

    protected readonly object sync = new();

    //====== public properties

    public int Count
    {
        get { lock (sync) { return items.Count; } }
    }

    //====== public methods

    public virtual void Add(T item)
    {
        lock (sync)
        {
            items.Add(item);
        }
    }

    public virtual void AddRange(IEnumerable<T> collection)
    {
        Guard.NotNull(collection);

        lock (sync)
        {
            items.AddRange(collection);
        }
    }

    public IEnumerable<T> EnumerateCurrentState()
    {
        int currentItemCount;

        lock (sync)
        {
            currentItemCount = items.Count;
        }

        return Enumerate(currentItemCount);

        IEnumerable<T> Enumerate(int itemCount)
        {
            for (int i = 0; i < itemCount; i++)
            {
                yield return items[i];
            }
        }
    }
}
