using Moq;
using SpaceBattle;
using Xunit;
namespace SpaceBattle.Tests;

public sealed class MoveRotateTests
{
    [Fact]
    public void MoveChangesPositionByVelocity()
    {
        var movingObject = new Mock<IMovingObject>();
        var position = new Vector(12, 5);
        movingObject.SetupProperty(x => x.Position, position);
        movingObject.SetupGet(x => x.Velocity).Returns(new Vector(-4, 1));

        new MoveCommand(movingObject.Object).Execute();

        Assert.Equal(new Vector(8, 6), movingObject.Object.Position);
    }

    [Fact]
    public void MoveThrowsWhenPositionCannotBeRead()
    {
        var movingObject = new Mock<IMovingObject>();
        movingObject.SetupGet(x => x.Position).Throws<InvalidOperationException>();

        Assert.Throws<InvalidOperationException>(() => new MoveCommand(movingObject.Object).Execute());
    }

    [Fact]
    public void MoveThrowsWhenVelocityCannotBeRead()
    {
        var movingObject = new Mock<IMovingObject>();
        movingObject.SetupGet(x => x.Position).Returns(new Vector(1, 2));
        movingObject.SetupGet(x => x.Velocity).Throws<InvalidOperationException>();

        Assert.Throws<InvalidOperationException>(() => new MoveCommand(movingObject.Object).Execute());
    }

    [Fact]
    public void MoveThrowsWhenPositionCannotBeChanged()
    {
        var movingObject = new Mock<IMovingObject>();
        movingObject.SetupGet(x => x.Position).Returns(new Vector(1, 2));
        movingObject.SetupGet(x => x.Velocity).Returns(new Vector(1, 1));
        movingObject.SetupSet(x => x.Position = It.IsAny<Vector>()).Throws<InvalidOperationException>();

        Assert.Throws<InvalidOperationException>(() => new MoveCommand(movingObject.Object).Execute());
    }

    [Fact]
    public void RotateChangesAngleByAngularVelocity()
    {
        var rotatingObject = new Mock<IRotatingObject>();
        rotatingObject.SetupProperty(x => x.Angle, new Angle(1, 8));
        rotatingObject.SetupGet(x => x.AngularVelocity).Returns(new Angle(1, 8));

        new RotateCommand(rotatingObject.Object).Execute();

        Assert.Equal(new Angle(2, 8), rotatingObject.Object.Angle);
    }

    [Fact]
    public void RotateThrowsWhenAngleCannotBeRead()
    {
        var rotatingObject = new Mock<IRotatingObject>();
        rotatingObject.SetupGet(x => x.Angle).Throws<InvalidOperationException>();

        Assert.Throws<InvalidOperationException>(() => new RotateCommand(rotatingObject.Object).Execute());
    }

    [Fact]
    public void RotateThrowsWhenAngularVelocityCannotBeRead()
    {
        var rotatingObject = new Mock<IRotatingObject>();
        rotatingObject.SetupGet(x => x.Angle).Returns(new Angle(1, 8));
        rotatingObject.SetupGet(x => x.AngularVelocity).Throws<InvalidOperationException>();

        Assert.Throws<InvalidOperationException>(() => new RotateCommand(rotatingObject.Object).Execute());
    }

    [Fact]
    public void RotateThrowsWhenAngleCannotBeChanged()
    {
        var rotatingObject = new Mock<IRotatingObject>();
        rotatingObject.SetupGet(x => x.Angle).Returns(new Angle(1, 8));
        rotatingObject.SetupGet(x => x.AngularVelocity).Returns(new Angle(1, 8));
        rotatingObject.SetupSet(x => x.Angle = It.IsAny<Angle>()).Throws<InvalidOperationException>();

        Assert.Throws<InvalidOperationException>(() => new RotateCommand(rotatingObject.Object).Execute());
    }
}
