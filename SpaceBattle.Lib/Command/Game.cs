using System.Diagnostics;
using SpaceBattle.Lib.Interfaces;

namespace SpaceBattle.Lib.Command;

public class Game : ICommand
{
    private readonly object _scope;
    private readonly Stopwatch _stopwatch = new();

    public Game(object scope) => _scope = scope;

    public void Execute()
    {
        _stopwatch.Reset();
        Ioc.Resolve<ICommand>("Scopes.Current.Set", _scope).Execute();

        var timeBudget = Ioc.Resolve<TimeSpan>("Command.Time");
        var hasPendingCommands = Ioc.Resolve<Func<int>>("Game.Queue.Count");

        while (hasPendingCommands() > 0 && _stopwatch.Elapsed <= timeBudget)
        {
            _stopwatch.Start();
            var current = Ioc.Resolve<ICommand>("Game.Queue.Take");

            try
            {
                current.Execute();
            }
            catch (Exception error)
            {
                Ioc.Resolve<ICommand>("ExceptionHandler", error, current).Execute();
            }

            _stopwatch.Stop();
        }
    }
}
