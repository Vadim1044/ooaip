namespace SpaceBattle.Lib.Command;

public class RegisterIoCDependencyFireCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>("IoC.Register", "Commands.Fire",
            (Func<object[], object>)(args => new FireCommand((IDictionary<string, object>)args[0]))
        ).Execute();
    }
}
