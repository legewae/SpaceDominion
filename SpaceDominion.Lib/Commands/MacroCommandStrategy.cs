namespace SpaceDominion.Lib.Commands;

// Стратегия сборки макрокоманды: имена из спецификации превращаются в команды
// через фабрику, порядок сохраняется. Одна и та же спецификация может дать
// разные команды при разных аргументах — поэтому аргументы передаются в фабрику.
public sealed class MacroCommandStrategy
{
    private readonly ICommandFactory _factory;

    public MacroCommandStrategy(ICommandFactory factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public ICommand Create(CommandSpecification specification, params object[] args)
    {
        ArgumentNullException.ThrowIfNull(specification);

        var commands = specification.CommandNames
            .Select(name => _factory.Create(name, args))
            .ToList();

        return new MacroCommand(commands);
    }
}
