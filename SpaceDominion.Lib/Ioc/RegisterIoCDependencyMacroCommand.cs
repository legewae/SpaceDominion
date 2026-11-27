using SpaceDominion.Lib.Commands;

namespace SpaceDominion.Lib.Ioc;

// Регистрирует команду сборки макрокоманды: Commands.Macro.
// Аргумент — набор команд, которые нужно исполнить последовательно.
public sealed class RegisterIoCDependencyMacroCommand : ICommand
{
    public void Execute()
    {
        App.Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Commands.Macro",
            (object[] args) => new MacroCommand((IEnumerable<ICommand>)args[0])).Execute();
    }
}
