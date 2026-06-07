namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMacroMoveRotate : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>("IoC.Register", "Macro.Move",
            (Func<object[], object>)(_ =>
                new MacroCommand(
                    Ioc.Resolve<string[]>("Specs.Move")
                        .Select(name => Ioc.Resolve<ICommand>(name))
                        .ToArray()))
        ).Execute();

        Ioc.Resolve<ICommand>("IoC.Register", "Macro.Rotate",
            (Func<object[], object>)(_ =>
                new MacroCommand(
                    Ioc.Resolve<string[]>("Specs.Rotate")
                        .Select(name => Ioc.Resolve<ICommand>(name))
                        .ToArray()))
        ).Execute();
    }
}
