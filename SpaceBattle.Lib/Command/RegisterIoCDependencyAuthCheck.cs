using SpaceBattle.Lib.Interfaces;

namespace SpaceBattle.Lib.Command;

public class RegisterIoCDependencyAuthCheck : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>("IoC.Register", "Authorization.Check", (object[] args) =>
        {
            var subjectId = (string)args[0];
            var action = (string)args[1];
            var objectId = (string)args[2];
            var permissions = Ioc.Resolve<IDictionary<string, IEnumerable<string>>>(
                "Authorization.GetPermissions", subjectId);

            if (permissions.ContainsKey("*"))
                return (object)true;

            if (!permissions.ContainsKey(objectId))
                return (object)false;

            var allowedActions = permissions[objectId];
            return (object)(allowedActions.Contains("*") || allowedActions.Contains(action));
        }).Execute();
    }
}
