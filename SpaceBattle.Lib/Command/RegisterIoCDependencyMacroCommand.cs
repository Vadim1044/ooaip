namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMacroCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>("IoC.Register", "Commands.Macro",
            (Func<object[], object>)(args => new MacroCommand(args.Cast<ICommand>().ToArray()))
        ).Execute();
    }
}
