namespace SpaceBattle.Lib;

public class MacroCommand : ICommand
{
    private readonly ICommand[] _commands;

    public MacroCommand(ICommand[] commands)
    {
        if (commands is not { Length: > 0 })
            throw new ArgumentException("Пустой или некорректный массив команд");

        _commands = commands.ToArray();
    }

    public void Execute()
    {
        foreach (var command in _commands)
            command.Execute();
    }
}
