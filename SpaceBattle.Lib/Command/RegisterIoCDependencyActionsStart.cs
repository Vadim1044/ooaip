namespace SpaceBattle.Lib.Command;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>("IoC.Register", "Actions.Start",
            (Func<object[], object>)(args =>
                new StartCommand((IDictionary<string, object>)args[0], (string)args[1]))
        );
    }
}
