using System;
using System.Collections.Generic;
using System.Linq;
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

    [Theory]
    [InlineData(new object[]  {                  }, new object[] {          })]
    [InlineData(new string[]  { "x"              }, new string[] { "x"      })]
    [InlineData(new string?[] { "a",  null, "b"  }, new string[] { "a", "b" })]
    [InlineData(new string?[] { null, null, null }, new string[] {          })]
    [InlineData(new string?[] { null, "x",  null }, new string[] { "x"      })]
    public void WhereNotNull(IEnumerable<object?> input, IEnumerable<object?> expected)
    {
        var actual = input.WhereNotNull().ToList();

        Assert.True(expected.SequenceEqual(actual));
    }

    [Theory]
    [MemberData(nameof(WhereNullableHasValueData))]
    public void WhereNullableHasValue(IEnumerable<int?> input, IEnumerable<int?> expected)
    {
        var actual = input.WhereNullableHasValue().ToList();

        Assert.True(expected.SequenceEqual(actual));
    }

    public static IEnumerable<object[]> WhereNullableHasValueData => new List<object[]>
    {
        new object[] { new int?[] {            }, new int?[] {      } },
        new object[] { new int?[] { null       }, new int?[] {      } },
        new object[] { new int?[] { null, null }, new int?[] {      } },
        new object[] { new int?[] { 1, null    }, new int?[] { 1    } },
        new object[] { new int?[] { 1, null, 3 }, new int?[] { 1, 3 } },
    };
}
