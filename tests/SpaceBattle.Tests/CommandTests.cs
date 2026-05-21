using Moq;
using SpaceBattle;
using Xunit;
namespace SpaceBattle.Tests;

public sealed class CommandTests
{
    [Fact]
    public void MacroCommandExecutesAllCommands()
    {
        var first = new Mock<ICommand>();
        var second = new Mock<ICommand>();

        new MacroCommand(first.Object, second.Object).Execute();

        first.Verify(x => x.Execute(), Times.Once);
        second.Verify(x => x.Execute(), Times.Once);
    }

    [Fact]
    public void MacroCommandStopsAndThrowsWhenCommandThrows()
    {
        var first = new Mock<ICommand>();
        var second = new Mock<ICommand>();
        first.Setup(x => x.Execute()).Throws<InvalidOperationException>();

        Assert.Throws<InvalidOperationException>(() => new MacroCommand(first.Object, second.Object).Execute());
        second.Verify(x => x.Execute(), Times.Never);
    }

    [Fact]
    public void SendCommandPassesCommandToReceiver()
    {
        var longCommand = new Mock<ICommand>();
        var receiver = new Mock<ICommandReceiver>();

        new SendCommand(longCommand.Object, receiver.Object).Execute();

        receiver.Verify(x => x.Receive(longCommand.Object), Times.Once);
    }

    [Fact]
    public void SendCommandThrowsWhenReceiverCannotAcceptCommand()
    {
        var receiver = new Mock<ICommandReceiver>();
        receiver.Setup(x => x.Receive(It.IsAny<ICommand>())).Throws<InvalidOperationException>();

        Assert.Throws<InvalidOperationException>(() => new SendCommand(Mock.Of<ICommand>(), receiver.Object).Execute());
    }

    [Fact]
    public void CommandInjectableExecutesInjectedCommand()
    {
        var command = new Mock<ICommand>();
        var injectable = new CommandInjectableCommand();

        injectable.Inject(command.Object);
        injectable.Execute();

        command.Verify(x => x.Execute(), Times.Once);
    }

    [Fact]
    public void CommandInjectableThrowsWhenCommandWasNotInjected()
    {
        Assert.Throws<InvalidOperationException>(() => new CommandInjectableCommand().Execute());
    }
}
