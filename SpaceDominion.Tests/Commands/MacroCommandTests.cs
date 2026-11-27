using SpaceDominion.Lib.Commands;

namespace SpaceDominion.Tests.Commands;

public class MacroCommandTests
{
    [Fact]
    public void Execute_RunsAllCommandsInOrder()
    {
        var calls = new List<string>();
        var sut = new MacroCommand(new ICommand[]
        {
            new RecordingCommand(() => calls.Add("first")),
            new RecordingCommand(() => calls.Add("second")),
        });

        sut.Execute();

        Assert.Equal("first,second", string.Join(",", calls));
    }

    [Fact]
    public void Execute_WhenNoCommands_DoesNothing()
    {
        var sut = new MacroCommand(Array.Empty<ICommand>());

        var exception = Record.Exception(sut.Execute);

        Assert.Null(exception);
    }

    [Fact]
    public void Execute_WhenCommandThrows_StopsAndPropagates()
    {
        var calls = new List<string>();
        var sut = new MacroCommand(new ICommand[]
        {
            new RecordingCommand(() => throw new InvalidOperationException("boom")),
            new RecordingCommand(() => calls.Add("second")),
        });

        var act = () => sut.Execute();

        Assert.Throws<InvalidOperationException>(act);
        Assert.Empty(calls);
    }

    [Fact]
    public void Constructor_WhenCommandsAreNull_Throws()
    {
        var act = () => new MacroCommand(null!);

        Assert.Throws<ArgumentNullException>(act);
    }
}
