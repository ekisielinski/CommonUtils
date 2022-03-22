namespace EK.CommonUtils;

public static class BitUtils
{
    public static ushort ReverseBytes(ushort value)
    {
        return (ushort)((value & 0x00FFu) << 8 | (value & 0xFF00u) >> 8);
    }

    public static short ReverseBytes(short value)
    {
        ushort v = (ushort)value;

        return (short)((v & 0x00FFu) << 8 | (v & 0xFF00u) >> 8);
    }
}
