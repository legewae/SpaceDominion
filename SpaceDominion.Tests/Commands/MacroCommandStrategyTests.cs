using Moq;
using SpaceDominion.Lib.Commands;

namespace SpaceDominion.Tests.Commands;

public class MacroCommandStrategyTests
{
    [Fact]
    public void Create_ResolvesEveryCommandThroughFactoryWithSameArguments()
    {
        var factory = new Mock<ICommandFactory>();
        var specification = new CommandSpecification(new[] { "Commands.First", "Commands.Second" });
        factory
            .Setup(f => f.Create("Commands.First", It.IsAny<object[]>()))
            .Returns(new Mock<ICommand>().Object);
        factory
            .Setup(f => f.Create("Commands.Second", It.IsAny<object[]>()))
            .Returns(new Mock<ICommand>().Object);
        var sut = new MacroCommandStrategy(factory.Object);

        sut.Create(specification, "match-1");

        factory.Verify(
            f => f.Create(
                "Commands.First",
                It.Is<object[]>(args => args.Length == 1 && (string)args[0] == "match-1")),
            Times.Once);
        factory.Verify(
            f => f.Create(
                "Commands.Second",
                It.Is<object[]>(args => args.Length == 1 && (string)args[0] == "match-1")),
            Times.Once);
    }

    [Fact]
    public void Create_ReturnsMacroCommandThatExecutesResolvedCommands()
    {
        var first = new Mock<ICommand>();
        var factory = new Mock<ICommandFactory>();
        factory
            .Setup(f => f.Create("Commands.First", It.IsAny<object[]>()))
            .Returns(first.Object);
        var sut = new MacroCommandStrategy(factory.Object);

        var macro = sut.Create(new CommandSpecification(new[] { "Commands.First" }));
        macro.Execute();

        first.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Create_WhenSpecificationIsNull_Throws()
    {
        var factory = new Mock<ICommandFactory>();
        var sut = new MacroCommandStrategy(factory.Object);

        var act = () => sut.Create(null!);

        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void Constructor_WhenFactoryIsNull_Throws()
    {
        var act = () => new MacroCommandStrategy(null!);

        Assert.Throws<ArgumentNullException>(act);
    }
}
