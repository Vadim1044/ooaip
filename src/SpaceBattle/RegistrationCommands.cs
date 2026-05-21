namespace SpaceBattle;

public sealed class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.Move", args =>
        {
            var movingObject = Ioc.Resolve<IMovingObject>("Adapters.IMovingObject", args[0]);
            return new MoveCommand(movingObject);
        });
    }
}

public sealed class RegisterIoCDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.Rotate", args =>
        {
            var rotatingObject = Ioc.Resolve<IRotatingObject>("Adapters.IRotatingObject", args[0]);
            return new RotateCommand(rotatingObject);
        });
    }
}

public sealed class RegisterIoCDependencyMacroCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.Macro", args =>
        {
            if (args.Length == 1 && args[0] is ICommand[] array)
            {
                return new MacroCommand(array);
            }

            return new MacroCommand(args.Cast<ICommand>().ToArray());
        });
    }
}

public sealed class RegisterIoCDependencyMacroMoveRotate : ICommand
{
    public void Execute()
    {
        Ioc.Register("Macro.Move", args => new CreateMacroCommandStrategy("Move").Resolve(args));
        Ioc.Register("Macro.Rotate", args => new CreateMacroCommandStrategy("Rotate").Resolve(args));
    }
}

public sealed class RegisterIoCDependencySendCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.Send", args => new SendCommand((ICommand)args[0], (ICommandReceiver)args[1]));
    }
}

public sealed class RegisterDependencyCommandInjectableCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.CommandInjectable", _ => new CommandInjectableCommand());
        Ioc.Register("Commands.CommadInjectable", _ => new CommandInjectableCommand());
    }
}

public sealed class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        Ioc.Register("Actions.Start", args => new StartLongOperationCommand((IDictionary<string, object>)args[0]));
    }
}

public sealed class RegisterIoCDependencyActionsStop : ICommand
{
    public void Execute()
    {
        Ioc.Register("Actions.Stop", args => new StopLongOperationCommand((IDictionary<string, object>)args[0]));
    }
}
