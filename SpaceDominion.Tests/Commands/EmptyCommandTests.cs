using SpaceDominion.Lib.Commands;

namespace SpaceDominion.Tests.Commands;

public class EmptyCommandTests
{
    [Fact]
    public void Execute_DoesNothing()
    {
        var sut = new EmptyCommand();

        var exception = Record.Exception(sut.Execute);

        Assert.Null(exception);
    }
}
