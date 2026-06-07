namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>("IoC.Register", "Commands.Move", (object[] args) =>
        {
            var movable = Ioc.Resolve<IMovingObject>("Adapters.IMovingObject", args[0]);
            return new MoveCommand(movable);
        }).Execute();
    }
}
