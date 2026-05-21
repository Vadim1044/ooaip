namespace SpaceBattle;

public interface ICommand
{
    void Execute();
}
public interface IMovingObject
{
    Vector Position { get; set; }
    Vector Velocity { get; }
}
