using SpaceBattle.Lib.Interfaces;

namespace SpaceBattle.Lib.Command;

public class FireCommand : ICommand
{
    private readonly IDictionary<string, object> _ship;

    public FireCommand(IDictionary<string, object> ship) => _ship = ship;

    public void Execute()
    {
        var position = Ioc.Resolve<IMovingObject>("Adapters.IMovingObject", _ship).Position;
        var heading = Ioc.Resolve<IRotatingObject>("Adapters.IRotatingObject", _ship).Angle
            ?? throw new InvalidOperationException("Ship angle is null");

        var radians = (double)heading;
        var direction = new Vector(
            (int)Math.Round(Math.Cos(radians)),
            (int)Math.Round(Math.Sin(radians)));

        var torpedoId = Guid.NewGuid();
        var torpedo = new Dictionary<string, object>
        {
            ["Position"] = position,
            ["Velocity"] = direction,
            ["Receiver"] = new CommandReceiver()
        };

        Ioc.Resolve<ICommand>("Game.Registry.Add", torpedoId, torpedo).Execute();
        Ioc.Resolve<ICommand>("Actions.Start", torpedo, "Move").Execute();
    }
}
