namespace EK.CommonUtils;

public static class DirectoryUtils
{
    public static void DeleteIfExists(string path)
    {
        Guard.NotNullOrEmpty(path);

        if (Directory.Exists(path))
        {
            Directory.Delete(path);
        }
    }

    public static void ThrowIfNotExists(string path, string? message = null)
    {
        Guard.NotNullOrEmpty(path);

        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException(message);
        }
    }
}
