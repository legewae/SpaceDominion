namespace SpaceDominion.Lib.Commands;

// Спецификация макрокоманды: упорядоченный список имён команд.
// Отдельный тип нужен, чтобы «список имён» нельзя было передать куда угодно
// и чтобы пустая спецификация отсекалась ещё до исполнения.
public sealed class CommandSpecification
{
    private readonly IReadOnlyList<string> _commandNames;

    public CommandSpecification(IEnumerable<string> commandNames)
    {
        ArgumentNullException.ThrowIfNull(commandNames);

        var names = commandNames.ToList();
        if (names.Count == 0)
        {
            throw new ArgumentException("Спецификация не может быть пустой.", nameof(commandNames));
        }

        if (names.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException("Имя команды в спецификации не может быть пустым.", nameof(commandNames));
        }

        _commandNames = names;
    }

    public IReadOnlyList<string> CommandNames => _commandNames;
}
