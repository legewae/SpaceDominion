namespace SpaceDominion.Lib.Commands;

// Команда, которая ничего не делает.
// Нужна как безопасная заглушка: при остановке повторяющейся операции в объект
// инжектируется пустая команда вместо настоящей.
public sealed class EmptyCommand : ICommand
{
    public void Execute()
    {
    }
}
