namespace SpaceDominion.Lib.Commands;

// Фабрика команд: единственное место, где команда получается по имени.
// Нужна, чтобы стратегии и правила не зависели от контейнера напрямую:
// в тестах подменяется заглушкой, в бою работает поверх IoC.
public interface ICommandFactory
{
    // Создаёт команду по имени зарегистрированной зависимости.
    ICommand Create(string commandName, params object[] args);
}
