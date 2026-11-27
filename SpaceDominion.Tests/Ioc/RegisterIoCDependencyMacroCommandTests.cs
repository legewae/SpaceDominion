using SpaceDominion.Lib.Commands;
using SpaceDominion.Lib.Ioc;

namespace SpaceDominion.Tests.Ioc;

public class RegisterIoCDependencyMacroCommandTests
{
    public RegisterIoCDependencyMacroCommandTests()
    {
        TestScope.Reset();
    }

    [Fact]
    public void Execute_RegistersMacroCommandThatRunsGivenCommands()
    {
        new RegisterIoCDependencyMacroCommand().Execute();
        var called = false;

        var macro = App.Ioc.Resolve<ICommand>(
            "Commands.Macro",
            new List<ICommand> { new RecordingCommand(() => called = true) });
        macro.Execute();

        Assert.True(called);
    }
}
