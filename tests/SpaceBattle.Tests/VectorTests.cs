using SpaceBattle;
using Xunit;
namespace SpaceBattle.Tests;

public sealed class VectorTests
{
    [Fact]
    public void SumOfOppositeVectorsReturnsZeroVector()
    {
        Assert.Equal(new Vector(0, 0, 0), new Vector(1, -1, 2) + new Vector(-1, 1, -2));
    }

    [Fact]
    public void SumThrowsWhenLeftVectorHasMoreDimensions()
    {
        Assert.Throws<ArgumentException>(() => new Vector(1, 2, 3) + new Vector(1, 2));
    }

    [Fact]
    public void SumThrowsWhenRightVectorHasMoreDimensions()
    {
        Assert.Throws<ArgumentException>(() => new Vector(1, 2) + new Vector(1, 2, 3));
    }

    [Fact]
    public void EqualCoordinateVectorsAreEqualByEquals()
    {
        Assert.True(new Vector(1, 2).Equals(new Vector(1, 2)));
    }

    [Fact]
    public void EqualCoordinateVectorsAreEqualByOperator()
    {
        Assert.True(new Vector(1, 2) == new Vector(1, 2));
    }

    [Fact]
    public void DifferentCoordinateVectorsAreNotEqualByEquals()
    {
        Assert.False(new Vector(1, 2).Equals(new Vector(2, 1)));
    }

    [Fact]
    public void DifferentCoordinateVectorsAreNotEqualByOperator()
    {
        Assert.True(new Vector(1, 2) != new Vector(2, 1));
    }

    [Fact]
    public void VectorHasHashCode()
    {
        Assert.IsType<int>(new Vector(1, 2).GetHashCode());
    }
}
