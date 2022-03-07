using Xunit;

namespace EK.CommonUtils.Tests;

public sealed class DataSizeTests
{
    [Theory]
    [InlineData(0L, "0 B")]
    [InlineData(1L, "1 B")]
    [InlineData(DataSize.KiB, "1 KiB")]
    [InlineData(DataSize.MiB, "1 MiB")]
    [InlineData(DataSize.GiB, "1 GiB")]
    [InlineData(DataSize.TiB, "1 TiB")]
    [InlineData(DataSize.PiB, "1 PiB")]
    public void ToString_RoundValues(long bytes, string toString)
    {
        string result = new DataSize(bytes).ToString();

        Assert.Equal(toString, result);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1024 * 1024)]
    [InlineData(1024, DataSize.GiB)]
    public void FromMiB_ValidData(long megabytes, long bytesExpected)
    {
        var dataSize = DataSize.FromMiB(megabytes);

        Assert.Equal(bytesExpected, dataSize.Bytes);
    }
}
