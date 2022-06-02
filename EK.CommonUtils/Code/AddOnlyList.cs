namespace EK.CommonUtils;

public sealed class AddOnlyList<T>
{
    const int DefaultCapacity = 4;

    T[] items;
    int itemCount = 0;

    readonly object sync = new();

    //====== ctors

    public AddOnlyList(int capacity = DefaultCapacity)
    {
        Guard.InRange(capacity, 0, Array.MaxLength);

        items = capacity == 0 ? Array.Empty<T>() : CreateArray(capacity);
    }

    //====== public properties

    public int Count
    {
        get { lock (sync) return itemCount; }
    }

    //====== public methods

    public void Add(T item)
    {
        lock (sync)
        {
            AddImpl(item);
        }
    }

    public void AddRange(IEnumerable<T> collection)
    {
        Guard.NotNull(collection);

        lock (sync)
        {
            foreach (T item in collection)
            {
                AddImpl(item);
            }
        }
    }

    public IEnumerable<T> EnumerateCurrentState()
    {
        lock (sync)
        {
            return Enumerate(items, Count);
        }
        
        static IEnumerable<T> Enumerate(T[] items, int itemCount)
        {
            for (int i = 0; i < itemCount; i++)
            {
                yield return items[i];
            }
        }
    }

    //====== private methods

    private void AddImpl(T item)
    {
        EnsureCapacity();

        items[itemCount] = item;

        itemCount++;
    }

    private void EnsureCapacity()
    {
        if (HasEnoughSpace()) return;

        if (items.Length == 0)
        {
            items = CreateArray(DefaultCapacity);
        }
        else
        {
            var newItems = CreateArray(CalculateNewLength());

            Array.Copy(items, 0, newItems, 0, items.Length);

            items = newItems;
        }

        bool HasEnoughSpace() => itemCount < items.Length;

        int CalculateNewLength() => (int) Math.Min(Array.MaxLength, items.Length * 2L);
    }

    //====== private static methods

    private static T[] CreateArray(int length)
    {
        return GC.AllocateUninitializedArray<T>(length, pinned: false);
    }
}
