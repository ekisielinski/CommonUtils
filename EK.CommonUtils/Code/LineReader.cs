using System.Collections;
using System.Text;

namespace EK.CommonUtils;

public sealed class LineReader : IEnumerable<string>
{
    readonly Func<TextReader> dataSourceFactory;

    //====== ctors

    private LineReader(Func<TextReader> dataSourceFactory)
    {
        this.dataSourceFactory = dataSourceFactory;
    }

    //====== public static methods

    public static LineReader FromStream(Func<Stream> streamFactory, Encoding? encoding = null)
    {
        Guard.NotNull(streamFactory);

        var dataSourceFactory = () => new StreamReader(streamFactory(), encoding ?? Encoding.UTF8);

        return new LineReader(dataSourceFactory);
    }

    public static LineReader FromFile(string filePath, Encoding? encoding = null)
    {
        Guard.NotNullOrEmpty(filePath);

        var dataSourceFactory = () => new StreamReader(filePath, encoding ?? Encoding.UTF8);

        return new LineReader(dataSourceFactory);
    }

    //====== IEnumerable<T>

    public IEnumerator<string> GetEnumerator()
    {
        using TextReader reader = dataSourceFactory();

        string? line;

        while ((line = reader.ReadLine()) is not null)
        {
            yield return line;
        }
    }

    //====== IEnumerable

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
