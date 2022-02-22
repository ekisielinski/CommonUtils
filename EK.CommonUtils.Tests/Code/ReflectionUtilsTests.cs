using System.Linq;
using Xunit;

namespace EK.CommonUtils.Tests;

public sealed class ReflectionUtilsTests
{
    [Fact]
    public void PublicInstanceGettersToString_TestObject()
    {
        var result = ReflectionUtils.PublicInstanceGettersToString(new TestObject());

        Assert.Equal(2, result.Count);
        Assert.Equal("getter", result.Single(x => x.Property == nameof(TestObject.InstancePropertyR)).Value);
    }
}
