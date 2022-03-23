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

    [Theory]
    [InlineData(0, 0)]
    [InlineData(uint.MaxValue, uint.MaxValue)]
    [InlineData(0xFF000000, 0x000000FF)]
    [InlineData(0xAABBCCDD, 0xDDCCBBAA)]
    [InlineData(0xA1B2C3D4, 0xD4C3B2A1)]
    public void ReverseBytes_UInt32(uint input, uint expected)
    {
        uint actual = BitUtils.ReverseBytes(input);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(int.MaxValue, -129)]
    [InlineData(-1, -1)]
    [InlineData(-256, 16777215)]
    [InlineData(-16711936, 16711935)]
    [InlineData(-1582119980, -725372255)]
    public void ReverseBytes_Int32(int input, int expected)
    {
        int actual = BitUtils.ReverseBytes(input);

        Assert.Equal(expected, actual);
    }
}
