using SpaceBattle.Lib.Interfaces;

namespace SpaceBattle.Lib.Command;

public class RegisterIoCDependencySendCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>("IoC.Register", "Commands.Send",
            (Func<object[], object>)(args =>
                new SendCommand((ICommand)args[0], (ICommandReceiver)args[1]))
        );
    }
}
