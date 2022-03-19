using System.Collections.Generic;
using EK.CommonUtils.Extensions;
using Xunit;

namespace EK.CommonUtils.Tests.Extensions;

public sealed class IEnumerableExtensionsTests
{
    [Fact]    
    public void ForEach_WithoutIndex()
    {
        var items = new int[] { 1, 2, 3, 4, 5 };

        var result = new List<int>();

        items.ForEach(result.Add);

        Assert.Equal(5, result.Count);
        Assert.Equal(1, result[0]);
        Assert.Equal(5, result[^1]);
    }

    [Fact]
    public void ForEach_WithIndex()
    {
        var items = new int[] { 1, 2, 3, 4, 5 };

        var result = new List<int>();

        items.ForEach((x, index) => result.Add(index));

        Assert.Equal(5, result.Count);
        Assert.Equal(0, result[0]);
        Assert.Equal(4, result[^1]);
    }

    [Fact]
    public void Consume_NonEmptyEnumerable_IteratesThroughAllElements()
    {
        int consumed = 0;

        CreateEnumerable().Consume();

        Assert.Equal(10, consumed);

        IEnumerable<object?> CreateEnumerable()
        {
            for (int i = 0; i < 10; i++)
            {
                consumed++;

                yield return null;
            }
        }
    }
}
