namespace EK.CommonUtils;

public static class FileUtils
{
    public static void DeleteIfExists(string path)
    {
        Guard.NotNullOrEmpty(path);

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
    
    public static DataSize GetSize(string path)
    {
        Guard.NotNullOrEmpty(path);

        long size = new FileInfo(path).Length;

        return new DataSize(size);
    }
}
