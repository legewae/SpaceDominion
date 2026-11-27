using SpaceDominion.Lib.Commands;

namespace SpaceDominion.Tests;

// Готовит чистый скоуп контейнера. Без сброса регистрации из разных тестов
// накладываются друг на друга, и тесты начинают зависеть от порядка запуска.
internal static class TestScope
{
    public static void Reset()
    {
        new App.Scopes.InitCommand().Execute();
        new App.Scopes.ClearCurrentScopeCommand().Execute();
        var scope = App.Ioc.Resolve<object>("IoC.Scope.Create");
        App.Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
    }
}

// Команда-шпион: запоминает факт исполнения, чтобы проверять порядок и побочные эффекты.
internal sealed class RecordingCommand : ICommand
{
    private readonly Action _action;

    public RecordingCommand(Action action)
    {
        _action = action;
    }

    public void Execute()
    {
        _action();
    }
}
