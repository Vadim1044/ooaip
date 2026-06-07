namespace SpaceBattle.Lib;

public class RegisterIoCDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>("IoC.Register", "Commands.Rotate", (object[] args) =>
        {
            var rotatable = Ioc.Resolve<IRotatingObject>("Adapters.IRotatingObject", args[0]);
            return new RotateCommand(rotatable);
        }).Execute();
    }
}
