using SpaceDominion.Lib.Commands;
using SpaceDominion.Lib.Ioc;

namespace SpaceDominion.Tests.Ioc;

public class IocCommandFactoryTests
{
    public IocCommandFactoryTests()
    {
        TestScope.Reset();
    }

    [Fact]
    public void Create_ReturnsCommandRegisteredInContainer()
    {
        var expected = new EmptyCommand();
        App.Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Commands.Test",
            (object[] _) => expected).Execute();
        var sut = new IocCommandFactory();

        var actual = sut.Create("Commands.Test");

        Assert.Same(expected, actual);
    }

    [Fact]
    public void Create_WhenNameIsWhitespace_Throws()
    {
        var sut = new IocCommandFactory();

        var act = () => sut.Create("   ");

        Assert.Throws<ArgumentException>(act);
    }
}
