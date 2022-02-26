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
    public void ToString_(long bytes, string toString)
    {
        string result = new DataSize(bytes).ToString();

        Assert.Equal(toString, result);
    }
}
