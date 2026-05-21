namespace SpaceBattle;

public sealed class EmptyCommand : ICommand
{
    public void Execute()
    {
    }
}

public sealed class MoveCommand : ICommand
{
    private readonly IMovingObject _movingObject;

    public MoveCommand(IMovingObject movingObject)
    {
        _movingObject = movingObject ?? throw new ArgumentNullException(nameof(movingObject));
    }

    public void Execute()
    {
        var position = _movingObject.Position ?? throw new InvalidOperationException("Position is not defined.");
        var velocity = _movingObject.Velocity ?? throw new InvalidOperationException("Velocity is not defined.");
        _movingObject.Position = position + velocity;
    }
}

public sealed class RotateCommand : ICommand
{
    private readonly IRotatingObject _rotatingObject;

    public RotateCommand(IRotatingObject rotatingObject)
    {
        _rotatingObject = rotatingObject ?? throw new ArgumentNullException(nameof(rotatingObject));
    }

    public void Execute()
    {
        var angle = _rotatingObject.Angle ?? throw new InvalidOperationException("Angle is not defined.");
        var angularVelocity = _rotatingObject.AngularVelocity ?? throw new InvalidOperationException("Angular velocity is not defined.");
        _rotatingObject.Angle = angle + angularVelocity;
    }
}

public sealed class MacroCommand : ICommand
{
    private readonly ICommand[] _commands;

    public MacroCommand(params ICommand[] commands)
    {
        ArgumentNullException.ThrowIfNull(commands);
        _commands = commands.ToArray();
    }

    public void Execute() => Array.ForEach(_commands, command => command.Execute());
}

public sealed class SendCommand : ICommand
{
    private readonly ICommand _command;
    private readonly ICommandReceiver _receiver;

    public SendCommand(ICommand command, ICommandReceiver receiver)
    {
        _command = command ?? throw new ArgumentNullException(nameof(command));
        _receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
    }

    public void Execute() => _receiver.Receive(_command);
}

public sealed class CommandInjectableCommand : ICommand, ICommandInjectable
{
    private ICommand? _command;

    public void Inject(ICommand command)
    {
        _command = command ?? throw new ArgumentNullException(nameof(command));
    }

    public void Execute()
    {
        if (_command is null)
        {
            throw new InvalidOperationException("Command was not injected.");
        }

        _command.Execute();
    }
}

public sealed class StartLongOperationCommand : ICommand
{
    private readonly IDictionary<string, object> _order;

    public StartLongOperationCommand(IDictionary<string, object> order)
    {
        _order = order ?? throw new ArgumentNullException(nameof(order));
    }

    public void Execute()
    {
        var operation = (string)_order["operation"];
        var gameObject = _order["object"];
        var receiver = (ICommandReceiver)_order["receiver"];

        var longCommand = Ioc.Resolve<ICommand>($"Macro.{operation}", gameObject);
        var injectableCommand = Ioc.Resolve<CommandInjectableCommand>("Commands.CommandInjectable");
        injectableCommand.Inject(longCommand);

        _order["command"] = injectableCommand;
        Ioc.Resolve<ICommand>("Commands.Send", injectableCommand, receiver).Execute();
    }
}

public sealed class StopLongOperationCommand : ICommand
{
    private readonly IDictionary<string, object> _order;

    public StopLongOperationCommand(IDictionary<string, object> order)
    {
        _order = order ?? throw new ArgumentNullException(nameof(order));
    }

    public void Execute()
    {
        var injectableCommand = (ICommandInjectable)_order["command"];
        injectableCommand.Inject(new EmptyCommand());
    }
}
