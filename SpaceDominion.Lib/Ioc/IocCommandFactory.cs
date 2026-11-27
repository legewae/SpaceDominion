using SpaceDominion.Lib.Commands;

namespace SpaceDominion.Lib.Ioc;

// Реализация фабрики команд поверх контейнера преподавателя.
// Контейнер знает только имена, поэтому доменный код работает с ICommandFactory,
// а не с App.Ioc напрямую.
public sealed class IocCommandFactory : ICommandFactory
{
    public ICommand Create(string commandName, params object[] args)
    {
        if (string.IsNullOrWhiteSpace(commandName))
        {
            throw new ArgumentException("Имя команды не может быть пустым.", nameof(commandName));
        }

        return App.Ioc.Resolve<ICommand>(commandName, args);
    }
}
