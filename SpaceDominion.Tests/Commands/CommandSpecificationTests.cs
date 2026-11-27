using SpaceDominion.Lib.Commands;

namespace SpaceDominion.Tests.Commands;

public class CommandSpecificationTests
{
    [Fact]
    public void Constructor_WhenNamesAreNull_Throws()
    {
        var act = () => new CommandSpecification(null!);

        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void Constructor_WhenNamesAreEmpty_Throws()
    {
        var act = () => new CommandSpecification(Array.Empty<string>());

        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Constructor_WhenNameIsWhitespace_Throws()
    {
        var act = () => new CommandSpecification(new[] { "Commands.First", " " });

        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Constructor_KeepsNamesAndOrder()
    {
        var sut = new CommandSpecification(new[] { "Commands.Second", "Commands.First" });

        Assert.Equal("Commands.Second,Commands.First", string.Join(",", sut.CommandNames));
    }
}
