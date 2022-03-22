using Xunit;

namespace EK.CommonUtils.Tests;

public sealed class BitUtilsTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(ushort.MaxValue, ushort.MaxValue)]
    [InlineData(0xFF00, 0x00FF)]
    [InlineData(0x00FF, 0xFF00)]
    [InlineData(0xF0F0, 0xF0F0)]
    [InlineData(0xABCD, 0xCDAB)]
    [InlineData(0b11001100_10101010, 0b10101010_11001100)]
    public void ReverseBytes_UInt16(ushort input, ushort expected)
    {
        ushort actual = BitUtils.ReverseBytes(input);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-1, -1)]
    [InlineData(-256, 255)]
    [InlineData(255, -256)]
    [InlineData(-3856, -3856)]
    [InlineData(-21555, -12885)]
    [InlineData(-13098, -10548)]
    public void ReverseBytes_Int16(short input, short expected)
    {
        short actual = BitUtils.ReverseBytes(input);

        Assert.Equal(expected, actual);
    }
}
