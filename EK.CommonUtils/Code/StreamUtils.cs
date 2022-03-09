using System.Text;

namespace EK.CommonUtils;

public static class StreamUtils
{
    public static string ToString(Stream stream, Encoding encoding)
    {
        Guard.NotNull(stream);
        Guard.NotNull(encoding);

        using var reader = new StreamReader(stream, encoding);

        string content = reader.ReadToEnd();

        return content;
    }

    public static Stream ToStream(string content, Encoding encoding)
    {
        Guard.NotNull(content);
        Guard.NotNull(encoding);

        byte[] data = encoding.GetBytes(content);

        return new MemoryStream(data);
    }
}
