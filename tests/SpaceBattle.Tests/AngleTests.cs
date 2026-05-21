using SpaceBattle;
using Xunit;
namespace SpaceBattle.Tests;

public sealed class AngleTests
{
    [Fact]
    public void SumIsNormalizedByDenominator()
    {
        Assert.Equal(new Angle(4, 8), new Angle(5, 8) + new Angle(7, 8));
    }

    [Fact]
    public void EqualAnglesAreEqualByEquals()
    {
        Assert.True(new Angle(15, 8).Equals(new Angle(23, 8)));
    }

    [Fact]
    public void EqualAnglesAreEqualByOperator()
    {
        Assert.True(new Angle(15, 8) == new Angle(23, 8));
    }

    [Fact]
    public void DifferentAnglesAreNotEqualByEquals()
    {
        Assert.False(new Angle(1, 8).Equals(new Angle(2, 8)));
    }

    [Fact]
    public void DifferentAnglesAreNotEqualByOperator()
    {
        Assert.True(new Angle(1, 8) != new Angle(2, 8));
    }

    [Fact]
    public void AngleHasHashCode()
    {
        Assert.IsType<int>(new Angle(1, 8).GetHashCode());
    }

    [Fact]
    public void AngleCanBePassedToMathCosWithoutExplicitCast()
    {
        Assert.Equal(Math.Sqrt(2) / 2, Math.Cos(new Angle(1, 8)), 12);
    }
}
