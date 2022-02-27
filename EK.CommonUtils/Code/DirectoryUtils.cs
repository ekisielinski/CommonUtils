namespace EK.CommonUtils;

public static class DirectoryUtils
{
    public static void DeleteIfExists(string dirPath)
    {
        Guard.NotNull(dirPath);

        if (Directory.Exists(dirPath))
        {
            Directory.Delete(dirPath);
        }
    }
}
