namespace SpaceDominion.Lib.Commands;

// Макрокоманда: исполняет набор команд последовательно, как одну.
// Нужна там, где действие состоит из нескольких шагов (подготовка матча,
// старт операции), но снаружи должно выглядеть одной командой.
// Ошибка останавливает исполнение: часть шагов уже применена, продолжать нельзя.
public sealed class MacroCommand : ICommand
{
    private readonly IReadOnlyList<ICommand> _commands;

    public MacroCommand(IEnumerable<ICommand> commands)
    {
        ArgumentNullException.ThrowIfNull(commands);
        _commands = commands.ToList();
    }

    public void Execute()
    {
        foreach (var command in _commands)
        {
            command.Execute();
        }
    }
}
