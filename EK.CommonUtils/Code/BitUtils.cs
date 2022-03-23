namespace EK.CommonUtils;

public static class BitUtils
{
    public static ushort ReverseBytes(ushort value)
    {
        return (ushort)((value & 0x00FFu) << 8 | (value & 0xFF00u) >> 8);
    }

    public static short ReverseBytes(short value)
    {
        unchecked
        {
            ushort v = (ushort)value;

            return (short)((v & 0x00FFu) << 8 | (v & 0xFF00u) >> 8);
        }
    }

    public static uint ReverseBytes(uint value)
    {
        value = value >> 16 | value << 16;
        
        return (value & 0xFF00FF00u) >> 8 | (value & 0x00FF00FF) << 8;
    }

    public static int ReverseBytes(int value)
    {
        unchecked
        {
            uint v = (uint)value;

            v = v >> 16 | v << 16;

            return (int)((v & 0xFF00FF00u) >> 8 | (v & 0x00FF00FF) << 8);
        }
    }
}
