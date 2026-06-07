namespace SpaceBattle.Lib;

public class CreateMacroCommandStrategy
{
    private readonly string _commandSpec;

    public CreateMacroCommandStrategy(string commandSpec) => _commandSpec = commandSpec;

    public ICommand Resolve(object[] args)
    {
        var commandNames = Ioc.Resolve<string[]>(_commandSpec);

        if (commandNames is not { Length: > 0 })
            throw new Exception("Spec not found or empty");

        var commands = new ICommand[commandNames.Length];
        for (var i = 0; i < commandNames.Length; i++)
            commands[i] = Ioc.Resolve<ICommand>(commandNames[i]);

        return new MacroCommand(commands);
    }
}
