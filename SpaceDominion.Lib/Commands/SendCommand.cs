namespace SpaceDominion.Lib.Commands;

// Команда-обёртка: передаёт другую команду получателю.
// Основа очередей и перенаправления запросов: решение «исполнить здесь или отправить
// дальше» принимает получатель, а не отправитель.
public sealed class SendCommand : ICommand
{
    private readonly ICommand _command;
    private readonly ICommandReceiver _receiver;

    // Оба аргумента обязательны: без команды или получателя отправлять нечего и некуда.
    public SendCommand(ICommand command, ICommandReceiver receiver)
    {
        _command = command ?? throw new ArgumentNullException(nameof(command));
        _receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
    }

    public void Execute()
    {
        _receiver.Receive(_command);
    }
}
