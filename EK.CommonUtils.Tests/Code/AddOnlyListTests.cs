using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EK.CommonUtils.Tests;

public sealed class AddOnlyListTests
{
    [Fact]
    public void Add_NullValue()
    {
        var sut = new AddOnlyList<string?>();

        sut.Add("1");
        sut.Add(null);
        sut.Add("3");

        Assert.Equal(3, sut.Count);
        Assert.Null(sut.EnumerateCurrentState().ElementAt(1));
    }

    [Fact]
    public void Count_UsingAdd()
    {
        var sut = new AddOnlyList<int>();
        
        sut.Add(1);
        sut.Add(2);
        sut.Add(3);

        Assert.Equal(3, sut.Count);
    }

    [Theory]
    [InlineData(0, new int[] { })]
    [InlineData(3, new int[] { 1, 2, 3 })]
    [InlineData(5, new int[] { 1, 2, 3, 4, 5 })]
    public void Count_UsingAddRange(int count, int[] collection)
    {
        var sut = new AddOnlyList<int>();

        sut.AddRange(collection);

        Assert.Equal(count, sut.Count);
    }

    [Fact]
    public void EnumerateCurrentState()
    {
        var sut = new AddOnlyList<int>();

        sut.Add(1);
        sut.Add(2);
        sut.Add(3);

        IEnumerable<int> state = sut.EnumerateCurrentState();

        sut.Add(4); // not included in 'state'
        sut.Add(5);

        Assert.Equal(3, state.Count());
        Assert.Equal(1, state.ElementAt(0));
        Assert.Equal(2, state.ElementAt(1));
        Assert.Equal(3, state.ElementAt(2));

        Assert.Throws<ArgumentOutOfRangeException>(() => state.ElementAt(3));
    }
}
