namespace EK.CommonUtils;

public static class FileUtils
{
    public static void DeleteIfExists(string filePath)
    {
        Guard.NotNull(filePath);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
    
    public static DataSize GetSize(string filePath)
    {
        Guard.NotNull(filePath);

        long size = new FileInfo(filePath).Length;

        return new DataSize(size);
    }
}
