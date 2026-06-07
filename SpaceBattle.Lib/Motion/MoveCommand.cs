namespace SpaceBattle.Lib;

public class MoveCommand : ICommand
{
    private readonly IMovingObject _movingObject;

    public MoveCommand(IMovingObject movingObject) => _movingObject = movingObject;

    public void Execute()
    {
        try
        {
            _movingObject.Position = _movingObject.Position + _movingObject.Velocity;
        }
        catch when (_movingObject.Position == null)
        {
            throw new InvalidOperationException("Cannot get position");
        }
        catch when (_movingObject.Velocity == null)
        {
            throw new InvalidOperationException("Cannot get velocity");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex}");
            throw;
        }
    }
}
