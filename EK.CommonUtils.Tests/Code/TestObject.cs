namespace EK.CommonUtils.Tests;

public sealed class TestObject
{
    public string InstancePropertyR { get; } = "getter";

    public int InstancePropertyRW { get; set; } = -5;

    public int InstancePropertyW
    {
        set
        {
            // nop
        }
    }

    public static int StaticProperty { get; }
}
