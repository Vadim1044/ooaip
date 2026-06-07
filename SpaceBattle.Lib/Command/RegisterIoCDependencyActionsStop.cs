namespace SpaceBattle.Lib.Command;

public class RegisterIoCDependencyActionsStop : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>("IoC.Register", "Actions.Stop",
            (Func<object[], object>)(args =>
                new StopCommand((IDictionary<string, object>)args[0], (string)args[1]))
        );
    }
}
